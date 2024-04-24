// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine;
using System.Collections.Generic;
using Thirtwo.Boids.Data;
using System;
namespace Thirtwo.Boids.Movement
{
    
    public abstract class BoidMovement : ScriptableObject
    {
        public abstract Vector2 CalculateMove(Boid boid, List<Transform> boidTransforms, SimulationData simulationData);
    }
}
