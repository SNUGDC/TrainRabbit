using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public TextAsset ttext;
    List<string> dialogueList = new List<string>();
    List<string> nameList = new List<string>();
    int i = 0;

    public GameObject DialogueEnd;
    Text nextName;
    Text nextText;

    // Use this for initialization
    void Start()
    {
        Debug.Log(ttext.text);
        DialougeReader reader = new DialougeReader();
        reader.Load(ttext);


        foreach (var row in reader.GetRowList())
        {
            nameList.Add(row.name);
            dialogueList.Add(row.dialogue);
            Debug.Log(row.name + ": " + row.dialogue);
        }
        nextName = GameObject.Find("Name").GetComponent<Text>();
        nextText = GameObject.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        nextName.text = nameList[i];
        nextText.text = dialogueList[i];
        if (Input.GetMouseButtonDown(0)) {
            if (i == dialogueList.Count -2)
            {
                i++;
                nextName.text = nameList[i];
                nextText.text = dialogueList[i];
                DialogueEnd.SetActive(true);

                i = 0;

            }
            else
            {
                i++;
                nextName.text = nameList[i];
                nextText.text = dialogueList[i];
            }
            }


        }
    }


