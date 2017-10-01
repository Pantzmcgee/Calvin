using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool facingRight;
    public float groundTolerance = 0.5f;
    public Transform heldObject;
    public float jumpForce;
    public float speed;
    private Rigidbody2D body;
    private new BoxCollider2D collider;
    private List<Collider2D> collidingTriggers;
    private int floorLayer;

    private float footOffset;
    private bool grounded;
    private float lastDistance;
    private bool liftPressed = false;

    //Animator variable
    public Animator Calvin_Animate;
    public SpriteRenderer Calvin_Sprite;

    private void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        collider = this.GetComponent<BoxCollider2D>();
        footOffset = collider.size.y / 2.0f;
        floorLayer = LayerMask.GetMask("Ground");
        facingRight = true;
        collidingTriggers = new List<Collider2D>();
    }

    private void Update()
    {
        GetKeysDown();
    }


    private void FixedUpdate()
    {
        grounded = IsOnGround();
        HandleInput();
    }

    private void GetKeysDown()
    {
        liftPressed = liftPressed || Input.GetKeyDown(KeyCode.E);

        if (liftPressed)
        {
            Debug.Log("Lift Pressed in Update");
        }
    }

    private void HandleInput()
    {
        var currVelocity = body.velocity;

        if (Input.GetKey(KeyCode.D))
        {
            Calvin_Animate.SetInteger("State", 1);
            Calvin_Sprite.flipX = false;
            currVelocity.x = speed;
            body.velocity = currVelocity;
            facingRight = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Calvin_Animate.SetInteger("State", 1);
            Calvin_Sprite.flipX = true;
            currVelocity.x = -speed;
            body.velocity = currVelocity;
            facingRight = false;
        }
        else
        {
            Calvin_Animate.SetInteger("State", 0);
            currVelocity.x = 0;
            body.velocity = currVelocity;
        }

        if (Input.GetKey(KeyCode.Space) && body.velocity.y <= 0)
        {
            if (grounded)
            {
                Calvin_Animate.SetInteger("State", 2);
                lastDistance = 100f;
                body.AddForce(Vector2.up * jumpForce);
            }
        }

        if (liftPressed)
        {
            Debug.Log("Lift Pressed in FixedUpdate");

            var liftBody = heldObject.GetComponentInChildren<Rigidbody2D>();
            if (heldObject.childCount > 0 && liftBody != null)
            {
                //Debug.Log("detach");

                heldObject.DetachChildren();
                liftBody.bodyType = RigidbodyType2D.Dynamic;
                liftBody.velocity = body.velocity;
            }
            else
            {
                foreach (var trigger in collidingTriggers)
                {
                    if (trigger.CompareTag("Liftable"))
                    {
                        Lift(trigger);
                        break;
                    }
                }
            }

            liftPressed = false;
        }
    }

    private bool IsOnGround()
    {
        var leftfoot = body.position + (Vector2.left * collider.size.x / 4.0f) + collider.offset + Vector2.down * footOffset;
        var rightfoot = body.position + (Vector2.right * collider.size.x / 4.0f) + collider.offset + Vector2.down * footOffset;

        var leftHit = Physics2D.Raycast(leftfoot, Vector2.down, groundTolerance, floorLayer);
        var rightHit = Physics2D.Raycast(rightfoot, Vector2.down, groundTolerance, floorLayer);

        Debug.DrawLine(leftfoot, leftfoot + Vector2.down * groundTolerance, Color.blue);
        Debug.DrawLine(rightfoot, rightfoot + Vector2.down * groundTolerance, Color.blue);

        if (leftHit || rightHit)
        {
            // Check if the distance of the object hit is less than the last distance checked
            if (leftHit.distance < lastDistance)
            {
                // Update the last distance if the object below is less than the last known distance
                lastDistance = leftHit.distance;
            }
            else if (rightHit.distance < lastDistance)
            {
                lastDistance = rightHit.distance;
            }
            else
            {
                // If the hit distance is not less than the last distance, then you're probably not
                // falling anymore.
                //Debug.Log("On ground");
                return true;
            }
        }

        //Debug.Log("Not on ground");
        return false;
    }

    private void Lift(Collider2D trigger)
    {
        var liftTransform = trigger.GetComponentInParent<Transform>();
        var liftBody = trigger.GetComponentInParent<Rigidbody2D>();

        //Debug.Log("attach");

        liftBody.bodyType = RigidbodyType2D.Kinematic;
        liftTransform.SetParent(heldObject);
        liftTransform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidingTriggers.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collidingTriggers.Remove(collision);
    }
}