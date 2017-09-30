using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce;
    public float groundTolerance = 0.5f;

    private int floorLayer;

    private float lastDistance;
    private Rigidbody2D body;
    private new BoxCollider2D collider;
    private float footPos;

    // Use this for initialization
    void Start ()
    {
        body = this.GetComponent<Rigidbody2D>();
        collider = this.GetComponent<BoxCollider2D>();
        footPos = collider.size.y / 2.0f;
        floorLayer = LayerMask.GetMask("Ground");
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    void FixedUpdate()
    {
        HandleInput();
    }

    bool IsOnGround()
    {
        var feet = body.position + collider.offset + Vector2.down * footPos;
        var hit = Physics2D.Raycast(feet, Vector2.down, groundTolerance, floorLayer);
        // Debug.DrawLine(feet, feet + Vector2.down * groundTolerance, Color.cyan);
        if (hit)
        {
            // Check if the distance of the object hit is less than the last distance checked
            if (hit.distance < lastDistance)
            {
                // Update the last distance if the object below is less than the last known distance
                lastDistance = hit.distance;
            }
            else
            {
                // If the hit distance is not less than the lass distance, then you're probably not falling anymore.
                lastDistance = 100f;
                return true;
            }
        }

        return false;
    }

    void HandleInput()
    {
        if (IsOnGround())
        {
            var currVelocity = body.velocity;

            if (Input.GetAxis("Horizontal") > 0)
            {
                currVelocity.x = speed;
                body.velocity = currVelocity;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                currVelocity.x = -speed;
                body.velocity = currVelocity;
            }
            else
            {
                currVelocity.x = 0;
                body.velocity = currVelocity;
            }

            if (Input.GetAxis("Jump") > 0)
            {
                body.AddForce(Vector2.up * jumpForce);
            }
        }
    }

}
