// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using System;
using Thirtwo.Boids.Movement;
using Thirtwo.DI;
using UnityEngine;
namespace Thirtwo.Boids.Data
{
    public class SimulationData : MonoBehaviour, IRegister
    {
        #region Variables
        public const int BOID_COUNT = 50;
        public const float BOID_DENSITY = 0.08f;
        private readonly float seperationMaxTime = 1.2f;
        public float SeperationMaxTime => seperationMaxTime;
        [field: Header("Seperation")]
        [field: SerializeField] public float MinNeighborRadius { get; private set; } = 0.1f;
        [field: SerializeField] public float MaxNeighborRadius { get; private set; } = 1f;
        [field: SerializeField] public float NeighborRadius { get; set; }
        public float SquareAvoidanceRadius => NeighborRadius * NeighborRadius;
        [field: Header("Cohesion")]
        [field: SerializeField] public float MinCohesionRadius { get; private set; } = 0f;
        [field: SerializeField] public float MaxCohesionRadius { get; private set; } = 1f;
        [field: SerializeField] public float CohesionRadius { get; set; }
        [field: Header("Alignment")]
        [field: SerializeField] public float MinAlignmentEffect { get; private set; } = 0f;
        [field: SerializeField] public float MaxAlignmentEffect { get; private set; } = 1f;
        [field: SerializeField] public float AlignmentEffect { get; set; }

        [field: Space]
        [field: Header("Active Movements")]
        [field:SerializeField] public BoidMovement ActiveMovement { get; private set; }
        [field: SerializeField] public Boid BoidPrefab { get; private set; }
        [field: Range(1f, 100f)][field: SerializeField] public float  DriveFactor { get; private set; } = 10f;
        [field: Range(1f, 100f)][field: SerializeField] public float MaxSpeed { get; private set; } = 5f;

        public float SquareMaxSpeed => MaxSpeed * MaxSpeed;

        public void Register(DependencyContainer container)
        {
            container.Register(this);
        }
        #endregion
    }
}
