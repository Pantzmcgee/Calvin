using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity_Zero : MonoBehaviour {
    public Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (rb2d.velocity.x != 0)
        {
            if (rb2d.velocity.x < 1 && rb2d.velocity.x > -1)
            {
                rb2d.velocity = new Vector2(0, 0);
            }
            else
            {
                rb2d.velocity = new Vector2((rb2d.velocity.x) * .5f, rb2d.velocity.y);
            }
        }
		
	}
}
