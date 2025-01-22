using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace _Core.Features.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public void Awake()
        {
            Screen.SetResolution(1920,1080, true);
        }

        public async void LoadBattle()
        {
            var handle = Addressables.LoadSceneAsync("Battle", LoadSceneMode.Additive);
            await handle.Task;
            gameObject.SetActive(false);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}