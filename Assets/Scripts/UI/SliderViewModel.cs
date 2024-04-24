// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine;
using System;
namespace Thirtwo.UI
{
    public class SliderViewModel
    {
        public event Action<float> SliderChanged;

        private SliderModel _sliderModel;

        public SliderViewModel(SliderModel sliderModel)
        {
            _sliderModel = sliderModel;
        }

        public void SetValue()
        {
            _sliderModel.Value = Mathf.Clamp(_sliderModel.Value, _sliderModel.MinValue, _sliderModel.MaxValue);
            SliderChanged?.Invoke(_sliderModel.Value);
        }

        public void OnSliderValueChanged(float value)
        {
            _sliderModel.Value = value;
            SliderChanged?.Invoke(_sliderModel.Value);
        }
    }
}
