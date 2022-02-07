using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider2D))]
public class Elevator : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform player;
    [SerializeField] GameState state;
    bool hasMoved;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !hasMoved)
        {
            hasMoved = true;
            state.isInCutscene = true;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(player.transform.DOMoveX(transform.position.x, 0.5f));
            sequence.Append(transform.DOMove(target.position, 1.2f));
            sequence.OnComplete(() =>
            {
                state.isInCutscene = false;
            });
            sequence.Play();
        }
    }
}