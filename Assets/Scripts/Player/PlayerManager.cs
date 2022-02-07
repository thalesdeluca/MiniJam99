using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerState state;
    [SerializeField] GameEvents gameEvents;


    void Start()
    {

    }

    void Update()
    {

    }

    public void Hit(float damage)
    {
        state.health -= damage;
        state.health = state.health <= 0 ? 0 : state.health;
    }

    IEnumerator WaitDeath()
    {
        yield return new WaitForSeconds(2f);
        gameEvents.LoadSceneEvent?.Invoke("LevelScene");
    }
}
