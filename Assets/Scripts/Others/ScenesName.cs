using UnityEditor;

#if UNITY_EDITOR
public static  class ScenesName
{
    [MenuItem("Catacosmic/Update Scene Name")]
    public static void UpdateSceneName()
    {
        // Get the list of scenes in the current build settings
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

        // Start building the enum
        string enumContent = "public enum ScenesNameEnum\n{\n";

        foreach (var scene in scenes)
        {
            if (scene.enabled) // Only include enabled scenes
            {
                string scenePath = scene.path;
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                enumContent += $"    {sceneName.Replace(" ", "_")},\n";
            }
        }

        enumContent += "}";

        // Save the enum content to a C# file
        string filePath = "Assets/Scripts/ScenesNameEnum.cs";
        System.IO.File.WriteAllText(filePath, enumContent);

        // Refresh the AssetDatabase to include the new file
        AssetDatabase.Refresh();
    }
}
#endif