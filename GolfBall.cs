using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ButaGolf.Helpers.PlayerUtils;

public class GolfBall : MonoBehaviour
{
    [SerializeField] float groundCheckRadius = 0.5f; // Radius for ground check
    [SerializeField] float groundCheckDistance = 0.2f; // Distance to check for ground
    [SerializeField] LayerMask groundLayer;

    Rigidbody body;
    bool onGround;
    bool isMoving;
    bool isShooting = false;

    LineRenderer lineRenderer;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    void Update()
    {
        isMoving = body.velocity.magnitude > 0.1f;
        onGround = CheckIfOnGround(transform.position, groundCheckRadius, groundCheckDistance, groundLayer);

        if (Input.GetButtonDown("Fire1"))
        {
            isShooting = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isShooting = false;
            lineRenderer.enabled = false;
        }

        if (isShooting)
        {
            DrawLineToMouse();
        }
    }

    void DrawLineToMouse()
    {
        lineRenderer.enabled = true;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, worldMousePosition);
    }

    bool CheckIfOnGround(Vector3 position, float radius, float distance, LayerMask layer)
    {
        return Physics.OverlapSphere(position, radius, layer).Length > 0 ||
               Physics.Raycast(
                   position,
                   Vector3.down,
                   out RaycastHit hit,
                   distance + radius,
                   layer
               );
    }
}
