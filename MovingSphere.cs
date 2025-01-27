using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written in Unity 2022.3.7f1
//Following this tutorial https://catlikecoding.com/unity/tutorials/movement/sliding-a-sphere/
public class MovingSphere : MonoBehaviour
{
    public float maxSpeed = 10;

    [SerializeField, Range(0f, 100f)]
    public float maxAcceleration = 5;

    [SerializeField]
    Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);

    [SerializeField, Range(0f, 1f)]
    float bounciness = 0.5f;

    private Vector3 velocity;


    // Update is called once per frame
    void Update()
    {
        // Get player input
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        // Calculate desired velocity
        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;

        // Adjust velocity based on acceleration
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        // Calculate new position
        Vector3 displacement = velocity * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + displacement;

        // Constrain movement within the allowed area
        if (newPosition.x < allowedArea.xMin)
        {
            newPosition.x = allowedArea.xMin;
            velocity.x = -velocity.x * bounciness;
        }
        else if (newPosition.x > allowedArea.xMax)
        {
            newPosition.x = allowedArea.xMax;
            velocity.x = -velocity.x * bounciness;
        }
        if (newPosition.z < allowedArea.yMin)
        {
            newPosition.z = allowedArea.yMin;
            velocity.z = -velocity.z * bounciness;
        }
        else if (newPosition.z > allowedArea.yMax)
        {
            newPosition.z = allowedArea.yMax;
            velocity.z = -velocity.z * bounciness;
        }

        // Update position
        transform.localPosition = newPosition;
    }
}
