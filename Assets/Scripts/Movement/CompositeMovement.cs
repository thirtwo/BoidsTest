// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine;
using System.Collections.Generic;
using Thirtwo.Boids.Data;
namespace Thirtwo.Boids.Movement
{
    [CreateAssetMenu(fileName = "CompositeMovement", menuName = "Boid/Movement/Composite")]
    public class CompositeMovement : BoidMovement
    {
        [SerializeField] private float[] weights;
        [SerializeField] private BoidMovement[] BoidMovements;

        public float[] Weight
        {
            get { return weights; }
            set { weights = value; }
        }
        public override Vector2 CalculateMove(Boid boid, List<Transform> boidTransforms, SimulationData simulationData)
        {
            var compositeMove = Vector2.zero;
            for (int i = 0; i < BoidMovements.Length; i++)
            {
                Vector2 partialMove = BoidMovements[i].CalculateMove(boid, boidTransforms, simulationData) * weights[i];

                if (partialMove != Vector2.zero)
                {
                    if (partialMove.sqrMagnitude > weights[i] * weights[i])
                    {
                        partialMove.Normalize();
                        partialMove *= weights[i];
                    }

                    compositeMove += partialMove;

                }
            }
            compositeMove /= BoidMovements.Length;
            return compositeMove;
        }
    }
}
