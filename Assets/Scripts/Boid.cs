// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using System.Collections.Generic;
using Thirtwo.Boids.Data;
using UnityEngine;
namespace Thirtwo.Boids
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Boid : MonoBehaviour
    {
        #region Variables
        [SerializeField] private CircleCollider2D circleCollider;
        private SimulationData _simulationData;
        private float _lerpTime = 0.1f;
        private List<Transform> _context = new List<Transform>();
        private Vector2 _direction;
        Collider2D[] _boidBuffer;
        #endregion
        public void Init(SimulationData simulationData)
        {
            _simulationData = simulationData;
            _boidBuffer = new Collider2D[SimulationData.BOID_COUNT];
        }
        private void Move(Vector2 velocity)
        {
            transform.up = velocity;
            transform.position += (Vector3)(velocity * Time.deltaTime);
        }


        #region Movement Logic
        public void MoveWithTouch(Vector2 touchPosition)
        {
            _direction = touchPosition - (Vector2)transform.position;
            var desiredVelocity = _direction.normalized * _simulationData.MaxSpeed;
            Move(desiredVelocity);
        }
        public void SetMovementStrategy()
        {
            List<Transform> context = GetNearbyObjects();

            Vector2 move = _simulationData.ActiveMovement.CalculateMove(this, context, _simulationData);
            move *= _simulationData.DriveFactor;
            if (move.sqrMagnitude > _simulationData.SquareMaxSpeed)
            {
                move = move.normalized * _simulationData.MaxSpeed;
            }
            Move(move);

        }
        private List<Transform> GetNearbyObjects()
        {
            _context.Clear();
            int nearbyBoidsCount = Physics2D.OverlapCircleNonAlloc(transform.position, _simulationData.NeighborRadius, _boidBuffer);
            for (int i = 0; i < nearbyBoidsCount; i++)
            {
                if (_boidBuffer[i] == circleCollider) continue;

                _context.Add(_boidBuffer[i].transform);

            }
            return _context;
        }
        #endregion
        #region Editor Methods
        [ContextMenu("Set Refs")]
        private void SetRefs()
        {
            circleCollider = GetComponent<CircleCollider2D>();
        }
        #endregion
    }
}
