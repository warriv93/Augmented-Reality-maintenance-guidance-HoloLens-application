using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class HaythamClient : MonoBehaviour {

    private class Commands
    {
        public const int StartCalibration = 1000;
        public const int ShowCalibrationPoint = 1001;
    }

    string clientName;
#if !UNITY_EDITOR
    KallbertLibrary.SocketClient client;
#endif

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    public GameObject calibrationPoint;
    public Camera sceneCamera;

    private void Start()
    {
        keywords.Add("connect", () =>
        {
#if !UNITY_EDITOR
            Debug.Log("connecting");
            this.Connect();
#endif
        });

        keywords.Add("calibrate", () =>
        {
#if !UNITY_EDITOR
            Debug.Log("starting calibration");
            this.StartCalibration();
#endif
        });

        calibrationPoint.SetActive(false);

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

#if !UNITY_EDITOR
    private void StartCalibration()
    {
        if (client.IsConnected)
        {
            calibrationPoint.SetActive(true);
            client.Send(Commands.StartCalibration);
        }
    }
#endif

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Recognized command!");
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

#if !UNITY_EDITOR
    private async void Connect()
    {
        client = new KallbertLibrary.SocketClient();

        bool success = await client.Connect("192.168.254.146", "60000");

        if (success) {
            
            await client.Send(Screen.width);
            await client.Send(Screen.height);

            Debug.Log("Success!");

            int message = await client.ReadInt();

            while (true)
            {
                switch (message)
                {
                    case Commands.ShowCalibrationPoint:
                        Debug.Log("Got calibration point");
                        int x = await client.ReadInt();
                        int y = await client.ReadInt();

                        UnityMainThreadDispatcher.Instance().Enqueue(() => {
                            Vector3 vec = new Vector3(x, y, 1.5f);
                            Vector3 worldPos = this.sceneCamera.ScreenToWorldPoint(vec);
                            Debug.Log(worldPos);
                            this.calibrationPoint.transform.position = worldPos;
                        }); 

                        break;
                    default:
                        break;
                }

                message = await client.ReadInt();
            }

        } else
        {
            Debug.Log("Failed to connect to server!");
        }
    }
#endif
}
