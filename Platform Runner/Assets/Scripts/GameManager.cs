using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        [SerializeField] private StartCountdownAnimation _startCountdown;
        [SerializeField] private RaceEndPositionAnimation _raceEndPositionAnimation;
        [Header("State Settings")]
        [SerializeField] private Transform _paintingPosition;
        [SerializeField] private float _celebrationTime;


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
            StartCoroutine(StartGameWithDelay(0.1f));
        }

        private void Update()
        {
            _stateMachine.UpdateState();
        }

        private void OnDestroy()
        {
            DOTween.KillAll();
        }

        private IEnumerator StartGameWithDelay(float time)
        {
            yield return new WaitForSeconds(time);
            ChangeState<MenuState>();
        }

        private void AddStatesToStateMachine()
        {
            _stateMachine.AddState(new MenuState(_uiManager));
            _stateMachine.AddState(new RunningState(_uiManager, _enemyUnits, _player.GetComponent<IMovementController>(), _startCountdown));
            _stateMachine.AddState(new RaceEndState(_player.GetComponent<ITweenMovement>(), _paintingPosition, RunningRaceManager.Instance, UiManager.Instance, _raceEndPositionAnimation));
            _stateMachine.AddState(new PaintingState(_uiManager, _cameraManager, PaintingManager.Instance));
            _stateMachine.AddState(new CelebrateState(_player, _celebrationTime));
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