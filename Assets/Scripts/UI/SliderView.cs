// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine.UI;
namespace Thirtwo.UI
{
    public class SliderView : Slider
    {
        private SliderModel _model;
        private SliderViewModel _viewModel;
        public SliderViewModel SetSlider(float min, float max, float value)
        {
            _model = new SliderModel(min, max, value);
            _viewModel = new SliderViewModel(_model);

            minValue = min;
            maxValue = max;
            this.value = value;

            this.onValueChanged.AddListener(_viewModel.OnSliderValueChanged);
            return _viewModel;
        }
    }
}
