using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    //bool prevFacingRight = true;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        var player = GetComponentInParent<PlayerController>();
        if (player != null)
        {
            var playerCollision = player.GetComponent<BoxCollider2D>();
            var liftBody = GetComponentInChildren<Rigidbody2D>();
            if (liftBody != null)
            {
                var liftCollisions = liftBody.GetComponentsInParent<BoxCollider2D>();
                BoxCollider2D liftCollision = null;
                foreach (var check in liftCollisions)
                {
                    if (!check.isTrigger)
                    {
                        liftCollision = check;
                        break;
                    }
                }
                
                if (player.facingRight)
                {
                    this.transform.localPosition = new Vector2(playerCollision.offset.x + playerCollision.size.x + liftCollision.offset.x + liftCollision.size.x, 0f);
                }
                else
                {
                    this.transform.localPosition = new Vector2(playerCollision.offset.x - playerCollision.size.x - liftCollision.offset.x - liftCollision.size.x, 0f);
                }
            }
        }
	}
}
