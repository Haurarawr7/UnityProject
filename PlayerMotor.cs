using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    [Header("Movement System")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;

    [SerializeField] private ParticleSystem walkingVFX;

    //Interaction components
    PlayerInteraction playerInteraction;
    InputManager inputManager;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        
        //Get interaction component
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        //Runs the function that handles all interaction
        Interact();
    }

    public void Interact()
    {
        //Tool interaction
        if (inputManager != null && inputManager.OnFoot.Interact.triggered)
        {
            //Interact
            if (playerInteraction != null)
            {
                playerInteraction.Interact();
            }
        }

        //TODO: Set up item interaction
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);

        // Handle walking VFX
        if (input != Vector2.zero && walkingVFX != null)
        {
            if (!walkingVFX.isPlaying)
                walkingVFX.Play();
        }
        else if (walkingVFX != null && walkingVFX.isPlaying)
        {
            walkingVFX.Stop();
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}