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
        #endregion
        public void Init(SimulationData simulationData)
        {
            _simulationData = simulationData;
        }
        private void Move(Vector2 velocity)
        {
            Vector3 normalizedDirection = velocity.normalized;
            transform.up = Vector3.Lerp(transform.up, normalizedDirection, _lerpTime);
            transform.position += (Vector3)velocity * Time.deltaTime;
        }


        #region Movement Logic
        public void MoveWithTouch(Vector2 touchPosition)
        {
            var direction = touchPosition - (Vector2)transform.position;
            var desiredVelocity = direction.normalized * _simulationData.MaxSpeed;
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
            var context = new List<Transform>();
            Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, _simulationData.NeighborRadius);
            foreach (Collider2D collider in contextColliders)
            {
                if (collider != circleCollider)
                {
                    context.Add(collider.transform);
                }
            }
            return context;
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
