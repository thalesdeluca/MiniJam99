using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameEvents events;
    bool hasStarted;

    IDisposable eventListener;


    void OnButtonPressed(InputControl button)
    {
        if (hasStarted)
            return;

        hasStarted = true;
        events.LoadSceneEvent?.Invoke("LevelScene");
    }

    void OnEnable()
    {
        eventListener = InputSystem.onAnyButtonPress.CallOnce(OnButtonPressed);
    }

    void OnDisable()
    {
        eventListener.Dispose();
    }
}