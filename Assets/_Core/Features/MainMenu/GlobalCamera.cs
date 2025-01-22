using UnityEngine;

namespace _Core.Features.MainMenu
{
    [DefaultExecutionOrder(-1)]
    public class GlobalCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private static GlobalCamera _instance;
        public static GlobalCamera Instance => _instance;
        public static Camera Camera { get; private set; }
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            
            Camera = _camera;
        }
    }
}