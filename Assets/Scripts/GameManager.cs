// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine;
namespace Thirtwo.Settings
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 30;
        }
    }
}
