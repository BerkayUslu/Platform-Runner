using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using PlatformRunner.Core;
using PlatformRunner.Core.StateMachine;

namespace PlatformRunner
{
    public class RunningRaceManager : SingletonMonobehaviour<RunningRaceManager>
    {
        public event Action<int> PlayerPositionChanged;
        [SerializeField] private RaceParticipant[] _raceParticipants;
        [SerializeField] private int _poisitionCheckFrameInterval = 5;

        private List<ParticipantPositionInfo> _participantPositionInfos;
        private int _frameCounter = 0;
        private int _playerPosition = -1;
        private int _playerFinalPosition = -1;
        public int PlayerFinalPosition { get { return _playerFinalPosition; } }


        private class ParticipantPositionInfo
        {
            public RaceParticipant Participant;
            public float PositionZ;
        }

        private void Start()
        {
            _participantPositionInfos = new List<ParticipantPositionInfo>();

            foreach (RaceParticipant participant in _raceParticipants)
            {
                _participantPositionInfos.Add(new ParticipantPositionInfo
                {
                    Participant = participant,
                });
            }
        }

        private void Update()
        {
            _frameCounter++;

            if (_frameCounter >= _poisitionCheckFrameInterval)
            {
                _frameCounter = 0;

                CheckPlayerPositioning();
            }

        }

        public void PlayerPassedFinishLine()
        {
            _playerFinalPosition = _playerPosition;
            GameManager.Instance.ChangeState<RaceEndState>();
        }

        private void CheckPlayerPositioning()
        {
            for (int i = 0; i < _participantPositionInfos.Count; i++)
            {
                _participantPositionInfos[i].PositionZ = _participantPositionInfos[i].Participant.GetPositionZ();
            }

            _participantPositionInfos.Sort((a, b) => b.PositionZ.CompareTo(a.PositionZ));

            for (int i = 0; i < _participantPositionInfos.Count; i++)
            {
                if (_participantPositionInfos[i].Participant.CompareTag(Tags.Player))
                {
                    int newPlayerPosition = i + 1;
                    if (newPlayerPosition != _playerPosition)
                    {
                        _playerPosition = newPlayerPosition;
                        PlayerPositionChanged?.Invoke(newPlayerPosition);
                    }
                }
            }
        }

        private void OnValidate()
        {
            _raceParticipants = FindObjectsOfType<RaceParticipant>();
        }
    }
}