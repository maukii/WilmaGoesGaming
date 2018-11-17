using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool interacting = false;

    [SerializeField] bool canMoveDiagonally = false;
    [SerializeField] float movementSpeed = 3f;

    public float inputV { get; private set; }
    public float inputH { get; private set; }

    public bool movingV = false, movingH = false;

    Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        Movement();
        RotatePlayer();
        UpdateAnimations();
    }

    private void Movement()
    {
        if (!interacting)
        {
            inputH = Input.GetAxisRaw("Horizontal");
            inputV = Input.GetAxisRaw("Vertical");

            if (canMoveDiagonally)
            {
                Vector3 direction = new Vector3(inputH, inputV, 0);

                if (!interacting)
                {
                    transform.Translate(direction * movementSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (inputH != 0 && !movingV)
                {
                    movingH = true;

                    Vector3 direction = new Vector3(inputH, 0, 0);
                    transform.Translate(direction * movementSpeed * Time.deltaTime);
                }
                else
                {
                    movingH = false;
                }

                if (inputV != 0 && !movingH)
                {
                    movingV = true;

                    Vector3 direction = new Vector3(0, inputV, 0);
                    transform.Translate(direction * movementSpeed * Time.deltaTime);
                }
                else
                {
                    movingV = false;
                }
            }
        }
        else
        {
            inputH = 0f;
            inputV = 0f;

            anim.SetFloat("Hor", 0);
            anim.SetFloat("Ver", 0);
        }
    }

    private void RotatePlayer()
    {
        if (inputH < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (inputH > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void UpdateAnimations()
    {
        anim.SetFloat("Hor", inputH);
        anim.SetFloat("Ver", inputV);
    }
}
