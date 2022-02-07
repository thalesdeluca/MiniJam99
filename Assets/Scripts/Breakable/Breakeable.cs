using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(Animator), typeof(Collider2D))]
public class Breakeable : MonoBehaviour, IDamageable
{
    [SerializeField] float health;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Hit(float damage)
    {
        if (health <= 0)
            return;

        health -= damage;

        DOTween.KillAll();
        transform.DOPunchRotation(new Vector3(0, 0, 10), 0.25f);

        if (health > 0)
            return;

        animator.Play("Break");
        StartCoroutine(WaitDestroy());
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}