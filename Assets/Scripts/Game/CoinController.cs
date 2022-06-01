using UnityEngine;

public class CoinController : MonoBehaviour
{
    float TurnSpeed = 90;
    GameController GameController;

    private void Awake() {
        GameController = GameObject.FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name != "Player") return;

        GameController.Score();

        Destroy(gameObject);
    }

    private void Update() {
        transform.Rotate(0, 0, TurnSpeed * Time.deltaTime);
    }
}
