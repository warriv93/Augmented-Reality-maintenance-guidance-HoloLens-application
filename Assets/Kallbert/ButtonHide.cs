using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
using UnityEngine.UI;

public class ButtonHide : MonoBehaviour {

    private Button btn;
    private bool hidden;
    private GameObject[] objects;

    // Use this for initialization
    void Start () {
        btn = this.gameObject.GetComponent<Button>();
        objects = GameObject.FindGameObjectsWithTag("ButtonHide");
        hidden = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
        for (var i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(!hidden);
        }
        
        hidden = !hidden;
    }
}
