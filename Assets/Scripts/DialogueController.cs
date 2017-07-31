﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueController : MonoBehaviour
{
    public TextAsset Dialogue;
    public int dialogueOrder;
    public Text speakerName;
	public Text speakerText;
    public GameObject DialogueEndButton;

    private List<string> dialogueList = new List<string>();
    private List<string> nameList = new List<string>();    
    private GameObject mainCamera;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");

        DialougeReader reader = new DialougeReader();
        reader.Load(Dialogue);

        foreach (var row in reader.GetRowList())
        {
            nameList.Add(row.name);
            dialogueList.Add(row.dialogue);
        }

        DialogueEndButton.SetActive(false);
    }

    private void Update()
    {
        if(Dialogue == null)
            return;
        if(dialogueOrder >= nameList.Count)
            return;

        speakerName.text = nameList[dialogueOrder];
        speakerText.text = dialogueList[dialogueOrder];
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueOrder == dialogueList.Count -2)
            {
                DialogueEndButton.SetActive(true);
            }

            dialogueOrder++;
        }
    }

    public void DialogueEnd()
    {
        mainCamera.transform.position = new Vector3(0,0,-10);
        mainCamera.GetComponent<Camera>().orthographicSize = 8f;
    }
}


