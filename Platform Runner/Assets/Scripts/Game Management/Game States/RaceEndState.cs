using PlatformRunner.Player;
using UnityEngine;

namespace PlatformRunner.Core.StateMachine
{
    public class RaceEndState : IGameState
    {
        private readonly PlayerController _player;
        private readonly Transform _paintingPosition;

        public RaceEndState(PlayerController player, Transform paintingPosition)
        {
            _player = player;
            _paintingPosition = paintingPosition;
        }

        public void Enter()
        {
            _player.MoveToPaintingPosition(_paintingPosition.position);
        }

        public void Exit() { }

        public void Update() { }
    }
}