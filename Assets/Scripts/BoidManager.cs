// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine;
using Thirtwo.Boids.Data;
using Thirtwo.Boids.Movement;
using System.Collections.Generic;
using System.Collections;
using Thirtwo.DI;
namespace Thirtwo.Boids
{
    public class BoidManager : MonoBehaviour, IResolve
    {
        #region Variables
        public static event System.Action NotifyOnLoad; 

        private readonly List<Boid> boids = new List<Boid>();
        private SimulationData _simulationData;

        private bool _isBusy = true;
        private bool _isTouchActive = false;
        private float _onTouchStartedTime;
        private float _onTouchEndedTime;

        #endregion
        #region Standart Unity Callbacks
        private void Start()
        {
            StartCoroutine(SpawnBoidsRoutine());
        }
        private void Update()
        {
            if (_isBusy) return;
            if (_isTouchActive) return;
            MoveBoidsWithStrategy();
        }

        private void OnEnable()
        {
            InputManager.OnTouchStarted += InputManager_OnTouchStarted;
            InputManager.OnTouchEnded += InputManager_OnTouchEnded;
            InputManager.OnTouch += MoveBoidsWithFingerTouch;
        }
        private void OnDisable()
        {
            InputManager.OnTouchStarted -= InputManager_OnTouchStarted;
            InputManager.OnTouchEnded -= InputManager_OnTouchEnded;
            InputManager.OnTouch -= MoveBoidsWithFingerTouch;
        }
        #endregion
        private void InputManager_OnTouchEnded()
        {
            _isTouchActive = false;
            _onTouchEndedTime = Time.time;
            StartCoroutine(SeperationRoutine());
        }

        private void InputManager_OnTouchStarted()
        {
            _isTouchActive = true;
            _onTouchStartedTime = Time.time;
        }

        private void MoveBoidsWithStrategy()
        {
            foreach (Boid boid in boids)
            {
                boid.SetMovementStrategy();
            }
        }

        private void MoveBoidsWithFingerTouch(Vector2 touchPosition)
        {
            for (int i = 0; i < SimulationData.BOID_COUNT; i++)
            {
                boids[i].MoveWithTouch(touchPosition);
            }
        }

        public IEnumerator SpawnBoidsRoutine()
        {
            for (int i = 0; i < SimulationData.BOID_COUNT; i++)
            {
                var boid = Instantiate(
                    _simulationData.BoidPrefab,
                    SimulationData.BOID_COUNT * SimulationData.BOID_DENSITY * Random.insideUnitCircle,
                    Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                    transform
                    );
                boid.Init(_simulationData);
                boids.Add(boid);
                if (i % 5 == 0)
                    yield return null;
            }
            NotifyOnLoad?.Invoke();
            _isBusy = false;
        }

        private IEnumerator SeperationRoutine()
        {
            var movement = (CompositeMovement)_simulationData.ActiveMovement;
            movement.Weight[0] += 10;
            var seperationTime = (_onTouchEndedTime - _onTouchStartedTime);
            if (seperationTime > _simulationData.SeperationMaxTime)
                seperationTime = _simulationData.SeperationMaxTime;

            var time = 0f;
            while (time < seperationTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            movement.Weight[0] -= 10;
        }

        public void Inject(DependencyContainer container)
        {
            _simulationData = container.Resolve<SimulationData>();
        }
    }
}
