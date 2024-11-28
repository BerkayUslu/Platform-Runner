using System.Collections;
using System.Collections.Generic;
using PlatformRunner.Player;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace PlatformRunner.Core.StateMachine
{
    public class CelebrateState : IGameState
    {
        private PlayerController _playerController;
        private float _celebrateTime;
        private float _elapsedTime = 0;

        public CelebrateState(PlayerController playerController, float celebrateTime)
        {
            _playerController = playerController;
            _celebrateTime = celebrateTime;
        }

        public void Enter()
        {
            _playerController.PlayCelebrateAnimation();
        }

        public void Exit()
        {
        }

        public void Update()
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime > _celebrateTime)
                GameManager.Instance.RestartCurrentScene();
        }
    }
}