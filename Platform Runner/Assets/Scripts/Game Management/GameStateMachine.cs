using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformRunner.Core.StateMachine
{
    public class GameStateMachine
    {
        private IGameState _currentState;
        private Dictionary<Type, IGameState> _states = new Dictionary<Type, IGameState>();

        public void AddState<T>(T state) where T : IGameState
        {
            _states.Add(typeof(T), state);
        }

        public void ChangeState<T>() where T : IGameState
        {
            if (_states.TryGetValue(typeof(T), out IGameState newState))
            {
                _currentState?.Exit();
                _currentState = newState;
                _currentState.Enter();
            }
            else
            {
                Debug.LogError($"State {typeof(T).Name} is not added to game state machine but called.");
            }
        }

        public void UpdateState()
        {
            _currentState?.Update();
        }
    }
}