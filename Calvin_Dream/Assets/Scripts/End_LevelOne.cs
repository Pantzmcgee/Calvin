using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class End_LevelOne : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene("LevelTwo");

    }

}
