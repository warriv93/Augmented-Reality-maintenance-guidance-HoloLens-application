using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnManager : MonoBehaviour {

	public void testFirstBtn()
    {
        Debug.Log("Btn was pressed my man!");
    }

    public void ExitGameBtn()
    {
        Debug.Log("Exit btn was pressed!");
        // kills application
        Application.Quit();
    }
}
