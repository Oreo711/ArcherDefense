using UnityEngine;


namespace _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Progress
{
    public sealed class PlayerProgressService
    {
        public int HighestUnlockedLevel { get; private set; } = 1;
        public int SelectedLevel { get; private set; } = 1;

        public void SelectLevel (int level)
        {
            SelectedLevel = level;
        }

        public void CompleteLevel(int completedLevel)
        {
            int nextLevel = completedLevel + 1;

            if (nextLevel > HighestUnlockedLevel)
                HighestUnlockedLevel = nextLevel;
        }
    }
}
