using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] GameEvents events;
    [SerializeField] GameState state;
    [SerializeField] SceneData sceneData;

    void Awake()
    {
        sceneData.activeScenes = new List<string>();
    }

    public void LoadScene(string name)
    {
        state.isInCutscene = true;
        if (sceneData.activeScenes.Contains(name))
        {
            StartCoroutine(ReloadScene(name));
            return;
        }

        sceneData.activeScenes.Add(name);
        StartCoroutine(LoadScenes(name));
    }

    IEnumerator LoadScenes(string actual)
    {
        var tasks = sceneData.activeScenes
                        .Where(item => item != actual)
                        .Select(item =>
                        {
                            sceneData.activeScenes.Remove(item);
                            return SceneManager.UnloadSceneAsync(item);
                        });


        while (tasks.Any(item => !item.isDone))
            yield return null;

        var scene = SceneManager.LoadSceneAsync(actual, LoadSceneMode.Additive);

        while (!scene.isDone)
            yield return null;

        Debug.LogWarning($"{actual} Loaded");
        state.isInCutscene = false;

    }

    IEnumerator ReloadScene(string actual)
    {
        var sceneUnload = SceneManager.UnloadSceneAsync(actual);

        while (!sceneUnload.isDone)
            yield return null;

        var scene = SceneManager.LoadSceneAsync(actual, LoadSceneMode.Additive);

        while (!scene.isDone)
            yield return null;

        state.isInCutscene = false;
    }


    void OnEnable()
    {
        events.LoadSceneEvent.AddListener(LoadScene);
    }

    void OnDisable()
    {
        events.LoadSceneEvent.RemoveListener(LoadScene);
    }
}