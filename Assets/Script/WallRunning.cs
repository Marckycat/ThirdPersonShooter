using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wall Running Settings")]
    public float wallRunSpeed = 8f;
    public float wallRunDuration = 2f;
    public float wallGravity = 2f;
    public float jumpOffWallForce = 5f;

    [Header("Detection")]
    public float wallCheckDistance = 1f;
    public LayerMask wallLayer;

    private Rigidbody rb;
    private bool isWallRunning = false;
    private Vector3 lastWallNormal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(CheckWall(out Vector3 wallNormal))
            {
                StartWallRun(wallNormal);
            }
        }else if (isWallRunning)
        {
            StopWallRun();
        }
    }

    void FixedUpdate()
    {
        if (isWallRunning)
        {
            PerformWallRun();
        }
    }

    private bool CheckWall(out Vector3 wallNormal)
    {
        //Detecta paredes a ambos lados del jugador
        RaycastHit hit;

        // Detecta paredes a la derecha
        Vector3 rightDirection = transform.right;
        Debug.DrawRay(transform.position, rightDirection * wallCheckDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.right, out hit, wallCheckDistance, wallLayer))
        {
            wallNormal = hit.normal; // Acceso correcto a la normal de la superficie
            return true;
        }

        // Detecta paredes a la izquierda
        Vector3 leftDirection = -transform.right;
        Debug.DrawRay(transform.position, leftDirection * wallCheckDistance, Color.blue);
        if (Physics.Raycast(transform.position, -transform.right, out hit, wallCheckDistance, wallLayer))
        {
            wallNormal = hit.normal; // Acceso correcto a la normal de la superficie
            return true;
        }

        wallNormal = Vector3.zero; // Si no hay pared detectada, devuelve un vector nulo
        return false;
    }

    private void StartWallRun(Vector3 wallNormal)
    {
        isWallRunning = true;
        lastWallNormal = wallNormal;

        // Reduce la gravedad para mantener al jugador en la pared
        rb.useGravity = false;
    }

    private void PerformWallRun()
    {
        // Obtén el input del jugador para avanzar o retroceder en la pared
        float forwardInput = Input.GetAxis("Vertical");

        // Calcula la dirección del movimiento a lo largo de la pared
        Vector3 wallForward = Vector3.Cross(lastWallNormal, Vector3.up).normalized;
        Vector3 moveDirection = wallForward * forwardInput;

        // Aplica la velocidad de movimiento en la pared
        rb.velocity = moveDirection * wallRunSpeed + Vector3.up * rb.velocity.y;

        // Gravedad personalizada
        rb.AddForce(Vector3.down * wallGravity, ForceMode.Force);
    }

    private void StopWallRun()
    {
        isWallRunning = false;
        rb.useGravity = true;

        // Permite al jugador saltar fuera de la pared
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(lastWallNormal * jumpOffWallForce, ForceMode.Impulse);
        }
    }

    public bool IsWallRunning()
    {
        return isWallRunning;
    }

}
