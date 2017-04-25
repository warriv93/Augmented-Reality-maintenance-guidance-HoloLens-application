using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUISub : MonoBehaviour {

    public Button btnBack;
    public Button btnFive;

    public GameObject btnBackTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickBtnBack()
    {
        btnBackTarget.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
