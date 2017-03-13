using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class KallbertSceneManager : MonoBehaviour
{

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        keywords.Add("load one", () =>
        {
            Debug.Log("Load One");
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        });

        keywords.Add("load two", () =>
        {
        });

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
    void Update()
    {

    }
}
