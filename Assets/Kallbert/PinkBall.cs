using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PinkBall : MonoBehaviour {

    GameObject ball;
    public Vector3 ballPosition;

    int width;
    int height;
    Vector2 gazePos;
    Vector2 centerPos;

    Camera camera;

    public LayerMask[] RaycastLayerMasks = new LayerMask[] { Physics.DefaultRaycastLayers };

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    float increment = 0.025f;

    // Use this for initialization
    void Start () {
        ball = this.gameObject;
        width = Screen.width;
        height = Screen.height;
        centerPos = new Vector2(width / 2f, height / 2f);
        gazePos = new Vector2(width / 2f, height / 2f);
        camera = this.GetComponentInParent<Camera>();
        
        Debug.Log("Width = " + width);
        Debug.Log("Height = " + height);

        keywords.Add("left", () => { move("left", increment); });
        keywords.Add("right", () => { move("right", increment); });
        keywords.Add("up", () => { move("up", increment); });
        keywords.Add("down", () => { move("down", increment); });
        keywords.Add("shoot", () => { shoot(); });

        // Tell the KeywordRecognizer about our keywords.
        string[] keyArray = new string[keywords.Keys.Count];
        keywords.Keys.CopyTo(keyArray, 0);
        for (var i = 0; i < keyArray.Length; i++)
        {
            Debug.Log(keyArray[i]);
        }

        keywordRecognizer = new KeywordRecognizer(keyArray);

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void move(string direction, float increment)
    {
        Vector3 pos = this.transform.position;

        switch (direction)
        {
            case "left":
                pos.x -= increment;
                break;
            case "right":
                pos.x += increment;
                break;
            case "up":
                pos.y += increment;
                break;
            case "down":
                pos.y -= increment;
                break;
        }

        this.transform.position = pos;
    }

    private void shoot()
    {
        Debug.Log("shooting");
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;
        RaycastHit hitInfo;

        Vector3 screenPos = camera.WorldToScreenPoint(this.transform.position);

        float x = (screenPos.x / centerPos.x) - 1;
        float y = (screenPos.y / centerPos.y) - 1;

        float maxAngleY = 8f;
        float maxAngleX = 14.3f;

        Vector3 offsetDirection = Quaternion.AngleAxis(maxAngleY * y, Vector3.left) * gazeDirection;
        offsetDirection = Quaternion.AngleAxis(maxAngleX * x, Vector3.up) * offsetDirection;

        if (Physics.Raycast(headPosition, offsetDirection, out hitInfo, 100f, RaycastLayerMasks[0]))
        {
            DrawLine(headPosition, hitInfo.point, Color.green, 10);
            //this.transform.position = hitInfo.point;
            //this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Recognized command!");
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    // Update is called once per frame
    void Update () {
        Vector3 pos = this.transform.position;
        
		if (Input.GetKey(KeyCode.I))
        {
            pos.y += 0.001f;
            Debug.Log("going up");
        } else if (Input.GetKey(KeyCode.J))
        {
            pos.x -= 0.001f;
        } else if (Input.GetKey(KeyCode.K))
        {
            pos.y -= 0.001f;
        } else if (Input.GetKey(KeyCode.L))
        {
            pos.x += 0.001f;
        } else if (Input.GetKeyUp(KeyCode.O))
        {
            Debug.Log("shooting");
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;
            RaycastHit hitInfo;

            Vector3 screenPos = camera.WorldToScreenPoint(this.transform.position);

            float x = (screenPos.x / centerPos.x) - 1;
            float y = (screenPos.y / centerPos.y) - 1;

            float maxAngleY = 8f;
            float maxAngleX = 14.3f;

            Vector3 offsetDirection = Quaternion.AngleAxis(maxAngleY * y, Vector3.left) * gazeDirection;
            offsetDirection = Quaternion.AngleAxis(maxAngleX * x, Vector3.up) * offsetDirection;

            if (Physics.Raycast(headPosition, offsetDirection, out hitInfo))
            {
                DrawLine(headPosition, hitInfo.point, Color.green, 10);
                this.transform.position = hitInfo.point;
                this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
        }

        this.transform.position = pos;
        ballPosition = pos;
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.01f, 0.01f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
}
