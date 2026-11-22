using UnityEngine.SceneManagement;

namespace _Project.Develop.Runtime.Infrastructure.SceneManagement
{
    public sealed class BuiltInSceneLoader : ISceneLoader
    {
        public void Load(string sceneKey)
        {
            SceneManager.LoadScene(sceneKey);
        }
    }
}
