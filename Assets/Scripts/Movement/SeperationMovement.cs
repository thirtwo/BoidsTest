// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine;
using System.Collections.Generic;
using Thirtwo.Boids.Data;
namespace Thirtwo.Boids.Movement
{
    [CreateAssetMenu(fileName = "SeperationMovement", menuName = "Boid/Movement/Seperation")]
    public class SeperationMovement : BoidMovement
    {
        public override Vector2 CalculateMove(Boid boid, List<Transform> boidTransforms, SimulationData simulationData)
        {
            if (boidTransforms.Count == 0)
                return Vector2.zero;

            Vector2 seperationMove = Vector2.zero;
            int avoidanceCount = 0;
            for (int i = 0; i < boidTransforms.Count; i++)
            {
                if (Vector2.SqrMagnitude(boidTransforms[i].position - boid.transform.position) < simulationData.SquareAvoidanceRadius)
                {
                    avoidanceCount++;
                    seperationMove += (Vector2)(boid.transform.position - boidTransforms[i].position);
                }
            }
            if (avoidanceCount > 0)
                seperationMove /= avoidanceCount;

            return seperationMove;
        }
    }
}
