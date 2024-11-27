using System;
using System.Collections;
using System.Collections.Generic;
using PlatformRunner.Core.StateMachine;
using PlatformRunner.Player;
using PlatformRunner.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformRunner.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private GameStateMachine _stateMachine;

        [Header("Script References")]
        [SerializeField] private PlayerController _player;
        [SerializeField] private UiManager _uiManager;
        [SerializeField] private CameraManager _cameraManager;
        [SerializeField] private EnemyUnitsManager _enemyUnits;
        [Header("State Settings")]
        [SerializeField] private Transform _paintingPosition;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            _stateMachine = new GameStateMachine();
            AddStatesToStateMachine();
            ChangeState<MenuState>();
        }

        private void AddStatesToStateMachine()
        {
            _stateMachine.AddState(new MenuState(_uiManager));
            _stateMachine.AddState(new RunningState(_uiManager, _player, _enemyUnits));
            _stateMachine.AddState(new RaceEndState(_player, _paintingPosition));
            _stateMachine.AddState(new PaintingState(_uiManager, _cameraManager));
        }

        public void ChangeState<T>() where T : IGameState
        {
            _stateMachine.ChangeState<T>();
        }

        public void StartGame()
        {
            _stateMachine.ChangeState<RunningState>();
        }

        public void RestartCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}