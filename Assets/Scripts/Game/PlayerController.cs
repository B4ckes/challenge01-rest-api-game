using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;
    public float IncreaseSpeedAmount = 0.1f;
    public bool IsPlayerAlive = true;

    float HorizontalMultiplier = 1.7f;

    Rigidbody RigidBody;
    GameController GameController;
    float HorizontalInput;

    private void Awake()
    {
        RigidBody = this.gameObject.GetComponent<Rigidbody>();
        GameController = GameObject.FindObjectOfType<GameController>();
    }

    private void FixedUpdate()
    {
        if (!IsPlayerAlive) return;

        Vector3 forwardMove = transform.forward * Speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * HorizontalInput * Speed * Time.fixedDeltaTime * HorizontalMultiplier;
        
        RigidBody.MovePosition(RigidBody.position + forwardMove + horizontalMove);
    }

    public void Die() {
        IsPlayerAlive = false;

        GameController.EndGame();

        Time.timeScale = 0;
    }

    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");

        if (IsPlayerAlive && transform.position.y < -2) {
            Die();
        }
    }
}
