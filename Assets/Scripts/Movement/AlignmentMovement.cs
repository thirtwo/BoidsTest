// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine;
using System.Collections.Generic;
using Thirtwo.Boids.Data;
namespace Thirtwo.Boids.Movement
{
    [CreateAssetMenu(fileName = "AlignmentMovement",menuName = "Boid/Movement/Alignment")]
    public class AlignmentMovement : BoidMovement
    {
        public override Vector2 CalculateMove(Boid boid, List<Transform> boidTransforms, SimulationData simulationData)
        {

            if (boidTransforms.Count == 0)
                return boid.transform.up;

            Vector2 alignmentMove = Vector2.zero;
            foreach (Transform otherBoid in boidTransforms)
            {
                alignmentMove += (Vector2)otherBoid.transform.up;
            }

            return alignmentMove * simulationData.AlignmentEffect;
        }
    }
}
