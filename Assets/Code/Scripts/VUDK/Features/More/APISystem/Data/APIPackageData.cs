namespace VUDK.Features.More.APISystem.Data
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "APIPackageData", menuName = "VUDK/API Package", order = 1)]
    public class APIPackageData : ScriptableObject
    {
        [TextArea(1, 10)]
        public string[] APIS;
    }
}
