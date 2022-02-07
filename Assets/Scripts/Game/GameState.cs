using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "Game/GameState", order = 0)]
public class GameState : ScriptableObject
{
    public bool hasStarted;
    public bool isPaused;
    public Checkpoint lastCheckpoint;
    public bool isInCutscene;
    public bool isFinished;

    public bool canControl
        => !isInCutscene && !isPaused && !isFinished && hasStarted;


    public void Reset()
    {
        hasStarted = false;
        isPaused = false;
        lastCheckpoint = null;
        isInCutscene = false;
        isFinished = false;
    }
}