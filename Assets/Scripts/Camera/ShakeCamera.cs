using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Collections;

public class ShakeCamera : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    [SerializeField] float time;
    [SerializeField] float intensity;
    [SerializeField] PlayerState state;
    [SerializeField] GameState gameState;
    [SerializeField] InputEvents events;

    void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    void OnFire(InputAction.CallbackContext context)
    {
        if (!gameState.canControl)
            return;

        if (context.started && state.canShoot)
        {

            StartCoroutine(WaitShake());
        }
    }

    IEnumerator WaitShake()
    {
        var perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = intensity;

        yield return new WaitForSeconds(time);
        perlin.m_AmplitudeGain = 0;
    }

    void OnEnable()
    {
        events.FireInputEvent.AddListener(OnFire);
    }

    void OnDisable()
    {
        events.FireInputEvent.RemoveListener(OnFire);
    }


}