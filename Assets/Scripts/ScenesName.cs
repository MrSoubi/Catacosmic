using UnityEditor;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
public static  class ScenesName
{
    [MenuItem("Catacosmic/Update Scene Name")]
    public static void UpdateSceneName()
    {
        // Get the list of scenes in build settings
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        // Build a string for the enum values
        string enumContent = "public enum ScenesNameEnum\n{\n";

        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            enumContent += $"    {sceneName.Replace(" ", "_")},\n";
        }

        enumContent += "}";

        // Save the enum content to a C# file
        System.IO.File.WriteAllText("Assets/Scripts/ScenesNameEnum.cs", enumContent);
        AssetDatabase.Refresh();
    }
}
#endif