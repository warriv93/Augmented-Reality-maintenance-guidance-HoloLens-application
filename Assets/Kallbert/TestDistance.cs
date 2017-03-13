using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TestDistance : MonoBehaviour {

    private MeshRenderer meshRenderer;
    public float minDistance = 3f;

    // Use this for initialization
    void Start () {
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 camPos = Camera.main.transform.position;
        Vector3 objPos = this.transform.position;
        float dist = Vector3.Distance(camPos, objPos);
        //Debug.Log("distance: " + dist);

        meshRenderer.enabled = dist <= minDistance;
	}
}
