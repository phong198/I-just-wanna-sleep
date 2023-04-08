using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerTutorial : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
    private int minute = 0;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;

    private int lives = 3;
    private int score = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        gameObject.transform.DOMoveX(-6, 3);
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump"))
            {
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
                Tutorial.Instance.livesImages[lives].SetActive(false);
                break;
            case "nightmare":
                int timeAdd = 15;
                minute += timeAdd;
                Tutorial.Instance.clockText.SetText("Time: 00 : " + minute.ToString());
                break;
            case "star":
                ++score;
                Tutorial.Instance.scoreText.SetText("Score: " + score.ToString());
                break;
        }
        Destroy(other.gameObject);
    }
}
