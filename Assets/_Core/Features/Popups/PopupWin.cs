﻿using UnityEngine;

namespace _Core.Features.Popups
{
    public class PopupWin : MonoBehaviour, IPopup
    {
        [SerializeField] private GameObject _container;

        public void Open()
        {
            _container.SetActive(true);
        }

        public void Close()
        {
            _container.SetActive(false);
        }
    }
}