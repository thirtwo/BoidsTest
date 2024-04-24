// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine;
using Thirtwo.DI;
using Thirtwo.UI;
using Thirtwo.Boids.Data;
using Thirtwo.Boids;
namespace Thirtwo
{
    public class BoidHud : MonoBehaviour, IResolve
    {
        private SimulationData _simulationData;

        [Header("Sliders")]
        [SerializeField] private SliderView _cohesionSlider;
        [SerializeField] private SliderView _seperationSlider;
        [SerializeField] private SliderView _alignmentSlider;

        [Header("Loading")]
        [SerializeField] private GameObject _loadingIcon;

        #region Unity Standart Callbacks
        private void OnEnable()
        {
            BoidManager.NotifyOnLoad += DisableLoading;
        }
        private void OnDisable()
        {
            BoidManager.NotifyOnLoad -= DisableLoading;
        }
        private void Start()
        {
            SetCohesionSlider();
            SetSeperationSlider();
            SetAlignmentSlider();
        }
        #endregion
        #region Slider Init
        private void SetCohesionSlider()
        {
            var sliderVM = _cohesionSlider.SetSlider(_simulationData.MinCohesionRadius, _simulationData.MaxCohesionRadius, _simulationData.CohesionRadius);
            sliderVM.SliderChanged += CohesionSliderChanged;
        }
        private void CohesionSliderChanged(float val)
        {
            _simulationData.CohesionRadius = val;
        }
        private void SetSeperationSlider()
        {
            var sliderVM = _seperationSlider.SetSlider(_simulationData.MinNeighborRadius, _simulationData.MaxNeighborRadius, _simulationData.NeighborRadius);
            sliderVM.SliderChanged += SeperationSliderChanged;
        }
        private void SeperationSliderChanged(float val)
        {
            _simulationData.NeighborRadius = val;
        }
        private void SetAlignmentSlider()
        {
            var sliderVM = _alignmentSlider.SetSlider(_simulationData.MinAlignmentEffect, _simulationData.MaxAlignmentEffect, _simulationData.AlignmentEffect);
            sliderVM.SliderChanged += AlignmentSliderChanged;
        }
        private void AlignmentSliderChanged(float val)
        {
            _simulationData.AlignmentEffect = val;
        }
        #endregion
        private void DisableLoading()
        {
            _loadingIcon.SetActive(false);
        }
        public void Inject(DependencyContainer container)
        {
            _simulationData = container.Resolve<SimulationData>();
        }
    }
}
