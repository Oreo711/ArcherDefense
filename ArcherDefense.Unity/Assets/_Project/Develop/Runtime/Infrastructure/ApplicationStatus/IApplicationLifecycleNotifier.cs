using System;

namespace _Project.Develop.Runtime.Infrastructure.ApplicationStatus
{
    public interface IApplicationLifecycleNotifier
    {
        event Action<bool> ApplicationPaused;
        event Action<bool> ApplicationFocusChanged;
        event Action ApplicationQuit;
    }
}
