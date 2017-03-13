using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Kallbert;

public class TestMenu : MonoBehaviour {

    public GameObject trigger;
    public GameObject menu;
    public Camera camera;

    private List<Image> targets;
    private bool isActive = false;
    private Vector3 originPos;
    private Vector3 cameraOriginDir;
    private Vector3 eyeOriginPos;
    private Vector3 menuOriginPos;
    private BoxCollider triggerCollider;
    private Image triggerImage;

    // Use this for initialization
    void Start () {
        /*
        Button triggerButton = trigger.GetComponent<Button>();
        triggerButton.onClick.AddListener(() =>
        {
            Debug.Log("Clicked trigger");
            isActive = !isActive;
            menu.SetActive(isActive);

            if (isActive)
            {
                cameraOriginDir = camera.transform.forward;
                eyeOriginPos = camera.WorldToScreenPoint(trigger.transform.position);
                menuOriginPos = camera.WorldToScreenPoint(menu.transform.position);

                Debug.Log("eye pos " + eyeOriginPos);
            }
        });
        */

        menu.SetActive(false);

        targets = menu.FindComponentsInChildrenWithTag<Image>("Target");
        triggerCollider = trigger.GetComponent<BoxCollider>();
        triggerImage = trigger.GetComponent<Image>();

        Debug.Log("Target count = " + targets.Count);
	}

    public void ClickTrigger()
    {
        Debug.Log("Clicked trigger");
        isActive = !isActive;
        menu.SetActive(isActive);

        if (isActive)
        {
            cameraOriginDir = camera.transform.forward;
            eyeOriginPos = camera.WorldToScreenPoint(trigger.transform.position);
            menuOriginPos = camera.WorldToScreenPoint(menu.transform.position);

            Debug.Log("eye pos " + eyeOriginPos);
        }
    }

    public void Test()
    {
        Debug.Log("Clicked!");
    }
	
	// Update is called once per frame
	void Update () {
        
        if (isActive)
        {
            Vector3 eyeGazePos = camera.WorldToScreenPoint(trigger.transform.position);

            //Debug.Log(eyeGazePos - eyeOriginPos);

            Vector3 targetPos = menuOriginPos + (eyeOriginPos - eyeGazePos);

            menu.transform.position = camera.ScreenToWorldPoint(Vector3.Lerp(menuOriginPos, targetPos, 0.5f * Time.deltaTime));
            
            triggerImage.color = Color.white;

            foreach (Image target in targets)
            {
                BoxCollider collider = target.GetComponent<BoxCollider>();

                if (collider != null)
                {
                    if (collider.bounds.Intersects(triggerCollider.bounds))
                    {
                        Debug.Log("Overlap!");
                        triggerImage.color = target.color;
                        break;
                    }
                }
            }
        }
	}
}


/*
Vector3 cameraDir = cameraOriginDir - camera.transform.forward;
Vector3 menuDir = new Vector3(cameraDir.x, cameraDir.y, 0);

Debug.Log(menuDir);
Vector3 menuPos = menu.transform.position;
menuPos = Vector3.Lerp(menuPos, menuPos + menuDir, 0.5f * Time.deltaTime);
menu.transform.position = menuPos;
*/
