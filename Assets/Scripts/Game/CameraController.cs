using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform Player;
    Vector3 Offset;

    private void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        Offset = transform.position - Player.position;
    }

    private void Update()
    {
        Vector3 targetPosition = Player.position + Offset;

        targetPosition.x = 0;

        transform.position = targetPosition;
    }
}
