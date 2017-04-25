using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TestHover : MonoBehaviour, IFocusable {

    private MeshRenderer meshRenderer;
    private float hideTimer;
    public float hideDelay;
    private bool hasExited;

    public void OnFocusEnter()
    {
        meshRenderer.enabled = true;
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
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasExited)
        {
            hideTimer += Time.deltaTime;
            if (hideTimer > hideDelay)
            {
                meshRenderer.enabled = false;
                hideTimer = 0;
                hasExited = false;
            }
        }
    }
}
