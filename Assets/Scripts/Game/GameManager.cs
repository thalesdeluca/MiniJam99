using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameEvents events;
    [SerializeField] GameState state;

    void Start()
    {
        Screen.SetResolution(480, 270, true, 60);
        Application.targetFrameRate = 60;
        state.hasStarted = true;
        events.LoadSceneEvent?.Invoke("MainMenuScene");
    }
}