#if UNITY_EDITOR
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

[ExecuteInEditMode]
public class EditorPlayStartScene : EditorWindow
{
#if UNITY_EDITOR
    [SerializeField] int startupScene = 0;
    [SerializeField] int targetScene = 0;

    [SerializeField] string lastScene = "";
    [SerializeField] bool hasPlayed = false;
    static EditorBuildSettingsScene[] scenes;


    void OnEnable()
    {
        scenes = EditorBuildSettings.scenes;
    }

    [MenuItem("Edit/Play From Scene %0")]
    public static void Run()
        => GetWindow<EditorPlayStartScene>();


    void OnGUI()
    {
        startupScene = EditorGUILayout.IntField("Startup Scene", startupScene);

        var actual = EditorApplication.currentScene;
        var startup = scenes[startupScene].path;

        EditorGUILayout.LabelField("Start Scene Path", startup);
        EditorGUILayout.LabelField("Previous Scene Path", lastScene);

        targetScene = EditorGUILayout.IntField("Target Scene", targetScene);
        var target = scenes[targetScene].path;

        EditorGUILayout.LabelField("Target Scene Path", target);

        if (GUILayout.Button("Switch to Scene"))
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(target);
        }


        if (EditorApplication.isPlaying)
            return;

        if (GUILayout.Button("Play"))
        {
            lastScene = actual;
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(startup);
            hasPlayed = true;
        }
    }

    void Update()
    {
        if (EditorApplication.currentScene == scenes[startupScene].path && hasPlayed)
        {
            EditorApplication.isPlaying = true;
            hasPlayed = false;
            return;
        }

        if (!EditorApplication.isPlaying)
        {
            if (!string.IsNullOrEmpty(lastScene))
            {
                EditorSceneManager.OpenScene(lastScene);
                lastScene = null;
            }
        }
    }
#endif
}
#endif
