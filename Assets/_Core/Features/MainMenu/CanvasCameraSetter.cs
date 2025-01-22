using UnityEngine;

namespace _Core.Features.MainMenu
{
    public class CanvasCameraSetter : MonoBehaviour
    {
        void Awake()
        {
            var canvas = GetComponent<Canvas>();
            canvas.worldCamera = GlobalCamera.Camera;
            canvas.planeDistance = 10;
        }
    }
}