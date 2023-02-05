using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public float gravity = -9.8f;
    public float stickForce = 10f;

    private Vector3 velocity = Vector3.zero;
    private bool isSticking = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSticking)
        {
            velocity.y = jumpSpeed;
        }

        velocity.y += gravity * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        if (transform.position.y <= 0f)
        {
            velocity.y = 0f;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            isSticking = true;
        }

        if (isSticking)
        {
            velocity.y = Mathf.Lerp(velocity.y, 0f, stickForce * Time.deltaTime);

            if (Mathf.Abs(velocity.y) < 0.1f)
            {
                velocity.y = 0f;
                isSticking = false;
            }
        }
    }
}