using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private Animator animator;

    public float rotationSpeed = 120f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("WalkLeft");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("WalkRight");
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("Slide");
        }

        if (!Input.anyKey)
        {
            animator.SetTrigger("Idle");
        }
    }
}