using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    public float rotationSpeed = 120f;
    public AudioClip moveSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = moveSound;
        audioSource.loop = true;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //    animator.SetTrigger("WalkLeft");

        //if (Input.GetKeyDown(KeyCode.D))
        //    animator.SetTrigger("WalkRight");
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isWalking", true);
          
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("Jump");

        if (Input.GetKeyDown(KeyCode.S))
            animator.SetTrigger("Slide");

        bool isMoving =
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.Q) ||
            Input.GetKey(KeyCode.E);

        if (isMoving)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

        if (!Input.anyKey)
            animator.SetTrigger("Idle");
    }
}