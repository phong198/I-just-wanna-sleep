using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;

    private int lives = 3;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        int isBoughtLive = PlayerPrefs.GetInt("isBoughtLive", 0);
        if (isBoughtLive == 1)
        {
            lives = 4;
        }
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        Debug.Log("lives: " + lives);
        direction += Vector3.down * gravity * Time.deltaTime;

        if (!GameManager.Instance.isPaused && character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump")) {
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Demon":
                --lives;
                if (lives == 0)
                {
                    GameManager.Instance.GameOver();
                }
                break;
            case "nightmare":
                GameManager.Instance.AddTime();
                break;
            case "star":
                GameManager.Instance.AddScore();
                break;
        }
        Destroy(other.gameObject);
    }

}
