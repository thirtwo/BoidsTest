// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine;
using System.Collections.Generic;
using Thirtwo.Boids.Data;
namespace Thirtwo.Boids.Movement
{
    [CreateAssetMenu(fileName = "KeepRadiusMovement", menuName = "Boid/Movement/KeepRadius")]
    public class KeepRadiusMovement : BoidMovement
    {
        private readonly float _threshold = 0.9f;
        [SerializeField] private Vector2 center;
        [SerializeField] private float radius = 15f;
        public override Vector2 CalculateMove(Boid boid, List<Transform> boidTransforms, SimulationData simulationData)
        {
            Vector2 centerOffset = center - (Vector2)boid.transform.position;
            float t = centerOffset.magnitude / radius;
            if (t < _threshold)
            {
                return Vector2.zero;
            }

            return centerOffset;
        }
    }
}
