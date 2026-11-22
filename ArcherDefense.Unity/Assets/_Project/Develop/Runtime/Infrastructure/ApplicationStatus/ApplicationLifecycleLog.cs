using System;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Develop.Runtime.Infrastructure.ApplicationStatus
{
    public sealed class ApplicationLifecycleLog : IInitializable, IDisposable
    {
        private readonly IApplicationLifecycleNotifier _notifier;

        public ApplicationLifecycleLog(IApplicationLifecycleNotifier notifier)
        {
            _notifier = notifier;
        }

        public void Initialize()
        {
            _notifier.ApplicationPaused += OnApplicationPaused;
            _notifier.ApplicationFocusChanged += OnApplicationFocusChanged;
            _notifier.ApplicationQuit += OnApplicationQuit;
        }

        public void Dispose()
        {
            _notifier.ApplicationPaused -= OnApplicationPaused;
            _notifier.ApplicationFocusChanged -= OnApplicationFocusChanged;
            _notifier.ApplicationQuit -= OnApplicationQuit;
        }

        private void OnApplicationPaused(bool isPaused) => Debug.Log($"Пауза: {isPaused}");

        private void OnApplicationFocusChanged(bool hasFocus) => Debug.Log($"Фокус: {hasFocus}");

        private void OnApplicationQuit() => Debug.Log("Приложение закрывается");
    }
}
