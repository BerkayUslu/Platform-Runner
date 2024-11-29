using System.Collections;
using System.Collections.Generic;
using PlatformRunner.Player;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace PlatformRunner.Core.StateMachine
{
    public class CelebrateState : IGameState
    {
        private IPlayerAnimate _playerAnimate;
        private float _celebrateTime;
        private float _elapsedTime = 0;

        public CelebrateState(IPlayerAnimate playerAnimate, float celebrateTime)
        {
            _playerAnimate = playerAnimate;
            _celebrateTime = celebrateTime;
        }

        public void Enter()
        {
            _playerAnimate.PlayCelebrateAnimation();
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