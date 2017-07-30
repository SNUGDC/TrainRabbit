using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueController : MonoBehaviour
{
    public TextAsset ttext;
    public int dialogueOrder = 0;
    public GameObject DialogueEnd;
    public Text nextName;
	public Text nextText;

    private List<string> dialogueList = new List<string>();
    private List<string> nameList = new List<string>();
/*
    private void Start()
    {
        //Debug.Log(ttext.text);
        DialougeReader reader = new DialougeReader();
        reader.Load(ttext);

        foreach (var row in reader.GetRowList())
        {
            nameList.Add(row.name);
            dialogueList.Add(row.dialogue);
            Debug.Log(row.name + ": " + row.dialogue);
        }
    }

    private void Update()
    {
        nextName.text = nameList[dialogueOrder];
        nextText.text = dialogueList[dialogueOrder];
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueOrder == dialogueList.Count -2)
            {
                dialogueOrder++;
                nextName.text = nameList[dialogueOrder];
                nextText.text = dialogueList[dialogueOrder];
                DialogueEnd.SetActive(true);
            }
            else
            {
                dialogueOrder++;
                nextName.text = nameList[dialogueOrder];
                nextText.text = dialogueList[dialogueOrder];
            }
        }
    }
*/}


