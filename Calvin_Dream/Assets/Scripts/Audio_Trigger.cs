using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Trigger : MonoBehaviour {

    public AudioSource source;
    private bool check = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (check)
        {
            Debug.Log("Bacon");
            source.Play();
            check = false;
        }
    }
}
