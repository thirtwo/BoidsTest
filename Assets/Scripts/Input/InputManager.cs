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
        private PointerEventData _pointerEventData;
        private List<RaycastResult> _raycastResults = new List<RaycastResult>();
        private void Awake()
        {
            _mainCam = Camera.main;
            _eventSystem = EventSystem.current;
            _pointerEventData = new PointerEventData(EventSystem.current);
        }
        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                if (IsPointerOverUIObject(Input.mousePosition)) return;
                OnTouchStarted?.Invoke();
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (IsPointerOverUIObject(Input.mousePosition)) return;
                OnTouchEnded?.Invoke();
            }
            if (Input.GetMouseButton(0))
            {
                if (IsPointerOverUIObject(Input.mousePosition)) return;
                var position = _mainCam.ScreenToWorldPoint(Input.mousePosition);
                OnTouch?.Invoke(position);
            }

#else
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

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

            _pointerEventData.position = new Vector2(inputPos.x, inputPos.y);
            _raycastResults.Clear();
            _eventSystem.RaycastAll(_pointerEventData, _raycastResults);
            return _raycastResults.Count > 0;
        }

    }
}
