using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu_ToggleControl : MonoBehaviour {

    public GameObject Calvin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //changes position of Calvin
        if (Calvin.transform.position.y > 0)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Calvin.transform.position = new Vector3((float)-1.3, (float)-.6,(float)-1);
            }
        }
        else if(Calvin.transform.position.y < 0 && Calvin.transform.position.y > -1)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Calvin.transform.position = new Vector3((float)-1.3, (float)-1.3, (float)-1);
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Calvin.transform.position = new Vector3((float)-1.3, (float).1, (float)-1);
            }
        }
        else if (Calvin.transform.position.y < -1)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Calvin.transform.position = new Vector3((float)-1.3, (float)-.6, (float)-1);
            }
        }

        //Pressing return key will load correct scene
        if (Input.GetKey(KeyCode.Return))
        {
            if (Calvin.transform.position.y > 0)
            {
                SceneManager.LoadScene("LevelOne");
            }
            else if (Calvin.transform.position.y < 0 && Calvin.transform.position.y > -1)
            {
                SceneManager.LoadScene("Credits");
            }
            else if (Calvin.transform.position.y < -1)
            {
                Application.Quit();
            }
        }

    }
}
