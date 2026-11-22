using UnityEditor;
using UnityEditor.SceneManagement;

namespace _Project.Develop.Editor
{
    [InitializeOnLoad]
    public static class EntryPointSceneAutoLoader
    {
        private const string MENU_PATH = "Tools/ArcherDefense/Play from Bootstrap";
        private const string ENABLED_KEY = "ArcherDefense.PlayFromBootstrap";

        static EntryPointSceneAutoLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static bool IsEnabled => EditorPrefs.GetBool(ENABLED_KEY, true);

        [MenuItem(MENU_PATH)]
        private static void Toggle()
        {
            EditorPrefs.SetBool(ENABLED_KEY, IsEnabled == false);
        }

        [MenuItem(MENU_PATH, true)]
        private static bool ToggleValidate()
        {
            Menu.SetChecked(MENU_PATH, IsEnabled);

            return true;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.ExitingEditMode)
                return;

            if (IsEnabled == false)
            {
                EditorSceneManager.playModeStartScene = null;
                return;
            }

            EditorSceneManager.playModeStartScene = FindStartScene();
        }

        private static SceneAsset FindStartScene()
        {
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                    return AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
            }

            return null;
        }
    }
}
