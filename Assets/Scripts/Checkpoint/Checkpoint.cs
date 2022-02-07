using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameState state;
    public int checkpointIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
            return;

        if (state.lastCheckpoint.checkpointIndex <= checkpointIndex)
            state.lastCheckpoint = this;
    }
}