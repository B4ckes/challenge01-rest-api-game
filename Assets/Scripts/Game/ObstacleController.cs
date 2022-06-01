using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    PlayerController playerMovement;

    private void Start() {
        playerMovement = GameObject.FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.name == "Player") {
            playerMovement.Die();
        }
    }
}
