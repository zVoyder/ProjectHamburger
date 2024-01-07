namespace VUDK.Features.Main.PointsSystem.UI
{
    using TMPro;
    using UnityEngine;
    using VUDK.Features.Main.PointsSystem.Events;

    public class UIPointsManager : MonoBehaviour
    {
        [Header("UI Points Panel")]
        [SerializeField]
        private RectTransform _pointsBox;
        [SerializeField]
        private bool _enableOnAwake;

        [Header("Texts")]
        [SerializeField]
        private TMP_Text _pointsText;

        private void Awake()
        {
            if (_enableOnAwake)
                Enable();
            else
                Disable();
        }

        private void OnEnable()
        {
            PointsEvents.OnPointsInit += SetPointsText;
            PointsEvents.OnPointsChanged += OnPointsChanged;
        }

        private void OnDisable()
        {
            PointsEvents.OnPointsInit -= SetPointsText;
            PointsEvents.OnPointsChanged -= OnPointsChanged;
        }

        public void Enable()
        {
            _pointsBox.gameObject.SetActive(true);
        }

        public void Disable()
        {
            _pointsBox.gameObject.SetActive(false);
        }

        private void OnPointsChanged(object sender, int pointsChange)
        {
            SetPointsText((sender as PointsManager).Points);
        }

        private void SetPointsText(int points)
        {
            _pointsText.text = points.ToString();
        }
    }
}