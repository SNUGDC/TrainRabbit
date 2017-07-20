using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    Text nextName;
    Text nextText;

    // Use this for initialization
    void Start()
    {
        nextName = GameObject.Find("Name").GetComponent<Text>();
        nextText = GameObject.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        nextName.text = "LOL";
        nextText.text = "WTFF";
    }


}
