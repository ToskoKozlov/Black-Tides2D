using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitWindow : MonoBehaviour {

    public GameObject currentWindow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void HideWindow()
    {
        if (currentWindow.activeInHierarchy)
        {
            currentWindow.SetActive(false);
        }
    }
}
