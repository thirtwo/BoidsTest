// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;
namespace Thirtwo
{
    public class InputManager : MonoBehaviour
    {
        public static event Action OnTouchStarted;
        public static event Action<Vector2> OnTouch;
        public static event Action OnTouchEnded;


        private Camera _mainCam;
        private EventSystem _eventSystem;
        private void Awake()
        {
            _mainCam = Camera.main;
            _eventSystem = EventSystem.current;
        }
        private void Update()
        {
            if (_eventSystem.IsPointerOverGameObject()) return;
#if UNITY_EDITOR
            if (IsPointerOverUIObject(Input.mousePosition)) return;

            if (Input.GetMouseButtonDown(0))
            {
                OnTouchStarted?.Invoke();
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnTouchEnded?.Invoke();
            }
            if (Input.GetMouseButton(0))
            {
                var position = _mainCam.ScreenToWorldPoint(Input.mousePosition);
                OnTouch?.Invoke(position);
            }

#else
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (_eventSystem.IsPointerOverGameObject(touch.fingerId)) return;

                if (IsPointerOverUIObject(touch.position)) return;

                if (touch.phase == TouchPhase.Began)
                {
                    OnTouchStarted?.Invoke();
                }
                else if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    var position = _mainCam.ScreenToWorldPoint(touch.position);
                    OnTouch?.Invoke(position);
                }
                else
                {
                    OnTouchEnded?.Invoke();
                }
            }
#endif
        }

        private bool IsPointerOverUIObject(Vector2 inputPos)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(inputPos.x, inputPos.y);
            List<RaycastResult> results = new List<RaycastResult>();
            _eventSystem.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

    }
}
