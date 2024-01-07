namespace VUDK.Generic.Utility.Graphics
{
    using UnityEngine;

    public class ParralaxEffect : MonoBehaviour
    {
        [SerializeField]
        private GameObject _anchor;
        [SerializeField]
        public float _parallaxValue;

        [SerializeField, Header("Length")]
        private float _length;

        private float _zOrignalPosition;
        //private float _yOriginalPosition;

        private void Start()
        {
            _zOrignalPosition = transform.position.z;
            //_yOriginalPosition = transform.position.y;
        }

        private void Update()
        {
            float temp = (_anchor.transform.position.z * (1 - _parallaxValue));
            float dist = (_anchor.transform.position.z * _parallaxValue);
            //float ydist = (_anchor.transform.position.y * _parallaxValue);

            transform.position = new Vector3(transform.position.x, transform.position.y, _zOrignalPosition + dist);

            if (temp > _zOrignalPosition + _length)
                _zOrignalPosition += _length;
            else if (temp < _zOrignalPosition - _length)
                _zOrignalPosition -= _length;
        }
    }
}