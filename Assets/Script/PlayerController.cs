using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] 
    private float speed = 15f;
    [SerializeField] 
    private Transform cam;
    public float turnSmoothTime = 0.1f;
    //float turnSmoothVelocity;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] 
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;

    // Referencia al script de WallRunning
    [SerializeField] private WallRunning wallRunning;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Permitir movimiento incluso cuando estás corriendo en la pared
        if (wallRunning != null && wallRunning.IsWallRunning())
        {
            HandleWallMovement();
            return;
        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -1f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = cam.forward * move.z + cam.transform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * speed);

        if(move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void HandleWallMovement()
    {
        // Movimiento solo hacia adelante o atrás (eje Z en el espacio local)
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * verticalInput;
        controller.Move(move * Time.deltaTime * speed);
    }

}
