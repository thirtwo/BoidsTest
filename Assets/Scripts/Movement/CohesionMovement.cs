// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine;
using System.Collections.Generic;
using Thirtwo.Boids.Data;
namespace Thirtwo.Boids.Movement
{
    [CreateAssetMenu(fileName = "CohesionMovement", menuName = "Boid/Movement/Cohesion")]
    public class CohesionMovement : BoidMovement
    {
        public override Vector2 CalculateMove(Boid boid, List<Transform> boidTransforms, SimulationData simulationData)
        {
            if (boidTransforms.Count == 0)
                return Vector2.zero;

            Vector2 cohesionMove = Vector2.zero;
            int boidsInRange = 0;
            foreach (Transform otherBoid in boidTransforms)
            {
                float distance = Vector2.Distance(boid.transform.position, otherBoid.position);
                if (distance < simulationData.CohesionRadius)
                {
                    cohesionMove += (Vector2)otherBoid.position;
                    boidsInRange++;
                }
            }
            if (boidsInRange > 0)
            {
                cohesionMove /= boidsInRange;
                cohesionMove -= (Vector2)boid.transform.position;
            }

            return cohesionMove;
        }
    }
}
