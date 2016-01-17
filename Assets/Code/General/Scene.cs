using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {

    public string nextScene = "";
	// Use this for initialization
	
	// Update is called once per frame
	void Update ()
    {
	 if (Input.GetKeyDown(KeyCode.Return))
        {
            Application.LoadLevel(nextScene);
        }
	}
}
