// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
namespace Thirtwo.UI
{
    public class SliderModel
    {
        public float Value;
        public float MinValue;
        public float MaxValue;

        public SliderModel(float minValue, float maxValue, float value)        {
            Value = value;
            MinValue = minValue;
            MaxValue = maxValue;
        }

    }
}
