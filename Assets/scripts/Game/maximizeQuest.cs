using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maximizeQuest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject instance = Instantiate(Resources.Load("quest_info", typeof(GameObject))) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
