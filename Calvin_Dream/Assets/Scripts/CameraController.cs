using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float dampTime = 0.15f;
    public Transform target;

    private new Camera camera;
    private Vector3 velocity;

	// Use this for initialization
	void Start ()
    {
        camera = this.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    private void FixedUpdate()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = new Vector3(target.position.x, target.position.y, 0) - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}
