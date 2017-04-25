using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TestUIHover : MonoBehaviour, IFocusable {

    private CanvasGroup canvasGroup;
    private float hideTimer;
    public float hideDelay;
    private bool hasExited;

    public void OnFocusEnter()
    {
        //meshRenderer.enabled = true;
        canvasGroup.alpha = 1;
        if (hasExited)
        {
            hasExited = false;
            hideTimer = 0;
        }
        Debug.Log("enter");
    }

    public void OnFocusExit()
    {
        hasExited = true;
        //meshRenderer.enabled = false;
        Debug.Log("exit");
    }

    // Use this for initialization
    void Start () {
        canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasExited)
        {
            hideTimer += Time.deltaTime;
            if (hideTimer > hideDelay)
            {
                //meshRenderer.enabled = false;
                canvasGroup.alpha = 0;
                hideTimer = 0;
                hasExited = false;
            }
        }
    }
}
