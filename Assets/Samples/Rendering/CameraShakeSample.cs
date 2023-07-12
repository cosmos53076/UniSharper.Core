using UniSharper.Rendering.PostProcessing;
using UnityEngine;

namespace UniSharper.Samples
{
    internal class CameraShakeSample : MonoBehaviour
    {
        private void Start()
        {
            CameraShake.Instance.Shake(5f, () =>
            {
                Debug.Log("Random shaking completed!");
            });
        }
    }
}