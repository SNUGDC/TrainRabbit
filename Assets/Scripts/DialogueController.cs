using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueController : MonoBehaviour {

    public TextAsset ttext;
    List<string> dialogueList = new List<string>();
    List<string> nameList = new List<string>();
    public int dialogueOrder = 0;

    public GameObject DialogueEnd;
    public Text nextName;
	public Text nextText;

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
    }


    void Update()
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

	private void OnMouseDown(){

	}

}


