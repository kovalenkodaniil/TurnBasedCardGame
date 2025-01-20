using System;
using _Core.Features.Popups;
using R3;
using UnityEngine;

namespace _Core.Features.Combat
{
    public class CombatEndHandler : MonoBehaviour
    {
        public event Action OnCombatEnded;

        [SerializeField] private PopupWin _popupWin;
        [SerializeField] private PopupLose _popupLose;

        public void WinPLayer()
        {
            _popupWin.Open();
        }

        public void LosePlayer()
        {
            _popupLose.Open();
        }
    }
}