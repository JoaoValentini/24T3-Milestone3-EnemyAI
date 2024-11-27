using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // speed when player is walking
    public float mouseSensitivity = 2f;
    float verticalRotation = 0;
    Rigidbody rb;

    Transform camTransform; // Transform component of the camera
    public Transform playerHead; // object for the camera to follow
                                 // public float cameraOffset; // camera distance from player

    GameManager gameManager;
    void Start()
    {   
        gameManager = FindObjectOfType<GameManager>();
        // get component references
        rb = GetComponent<Rigidbody>();

        camTransform = Camera.main.transform; // getting reference to the camera transform
    }

    void Update()
    {
        if(gameManager.IsPaused)
            return;

        Rotation();
        Movement();
    }

    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X"); // horizontal mouse movement
        float mouseY = Input.GetAxis("Mouse Y");  // vertical mouse movement

        float yRotation = mouseX * mouseSensitivity * Time.timeScale;
        float xRotation = mouseY * mouseSensitivity * Time.timeScale;
        transform.Rotate(0, yRotation, 0); // rotate the player in the Y axis

        verticalRotation -= xRotation;
        verticalRotation = Mathf.Clamp(verticalRotation, -10, 89); // clamp the rotation

        playerHead.localRotation = Quaternion.Euler(verticalRotation, 0, 0); // rotate the player head for the camera 
    }

    void Movement()
    {
        float sideInput = Input.GetAxis("Horizontal"); // A and D keys input
        float forwardInput = Input.GetAxis("Vertical");  // W and S keys input

        Vector3 movement = new Vector3(sideInput,0,forwardInput) * moveSpeed;
        movement = transform.TransformDirection(movement);

        rb.velocity = new Vector3(movement.x, rb.velocity.y,movement.z);
    }



}
