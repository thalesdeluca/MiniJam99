using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvents", menuName = "Game/GameEvents", order = 0)]
public class GameEvents : ScriptableObject
{
    public UnityEvent<string> LoadSceneEvent;
}