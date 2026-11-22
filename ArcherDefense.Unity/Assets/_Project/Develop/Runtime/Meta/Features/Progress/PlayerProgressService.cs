namespace _Project.Develop.Runtime._Project.Develop.Runtime.Meta.Features.Progress
{
    public sealed class PlayerProgressService
    {
        public int CurrentLevel { get; private set; } = 1;

        public void CompleteLevel(int completedLevel)
        {
            int nextLevel = completedLevel + 1;

            if (nextLevel > CurrentLevel)
                CurrentLevel = nextLevel;
        }
    }
}
