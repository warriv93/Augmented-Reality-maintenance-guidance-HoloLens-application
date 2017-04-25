using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUIRoot : MonoBehaviour {

    public Button btnOne;
    public Button btnTwo;
    public Button btnThree;
    public Button btnFour;
    public GameObject btnTwoTarget;

    GameObject[] objects;
    private bool visible;

    // Use this for initialization
    void Start () {
        visible = true;
        objects = GameObject.FindGameObjectsWithTag("ButtonHide");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickBtnOne()
    {
        for (var i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(!visible);
        }

        visible = !visible;
    }

    public void ClickBtnTwo()
    {
        btnTwoTarget.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
