using System;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.ApplicationStatus
{
    public sealed class ApplicationLifecycleNotifier : MonoBehaviour, IApplicationLifecycleNotifier
    {
        public event Action<bool> ApplicationPaused;
        public event Action<bool> ApplicationFocusChanged;
        public event Action ApplicationQuit;

        private void OnApplicationPause(bool pauseStatus) => ApplicationPaused?.Invoke(pauseStatus);

        private void OnApplicationFocus(bool hasFocus) => ApplicationFocusChanged?.Invoke(hasFocus);

        private void OnApplicationQuit() => ApplicationQuit?.Invoke();
    }
}
