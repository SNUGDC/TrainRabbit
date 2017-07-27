using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public TextAsset ttext;


    Text nextName;
    Text nextText;

    // Use this for initialization
    void Start()
    {
        Debug.Log(ttext.text);
        DialougeReader reader = new DialougeReader();
        reader.Load(ttext);
        string[] strArray = { };

        foreach (var row in reader.GetRowList())
        {

            Debug.Log(row.name + ": " + row.dialogue);
        }
        nextName = GameObject.Find("Name").GetComponent<Text>();
        nextText = GameObject.Find("Text").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
        nextName.text = "LOL";
        nextText.text = "WTFF";
        }
    }


}
