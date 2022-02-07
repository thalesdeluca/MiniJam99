using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SceneData", menuName = "Game/SceneData", order = 0)]
public class SceneData : ScriptableObject
{
    public List<string> activeScenes;
}