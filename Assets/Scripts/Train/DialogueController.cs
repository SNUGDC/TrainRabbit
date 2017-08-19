using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueController : MonoBehaviour
{
    public TextAsset Dialogue;
    public int dialogueOrder;
    public Text speakerName;
	public Text speakerText;
    public GameObject DialogueButton;
    public GameObject ChoicePanel;

    private List<Dialogue> dialogueList = new List<Dialogue>();
    private GameObject mainCamera;
    private AudioSource SEAudio;
    private GameObject QuestAccept;
    private GameObject QuestRefuse;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        SEAudio = GetComponent<AudioSource>();
        QuestAccept = ChoicePanel.transform.Find("Yes").gameObject;
        QuestRefuse = ChoicePanel.transform.Find("No").gameObject;

        TextReader reader = new TextReader();
        reader.Parse(Dialogue);
        dialogueList = reader.dialogueList;

        DialogueButton.SetActive(true);
        DialogueButton.GetComponent<Text>().text = "다음";
        DialogueButton.GetComponent<Button>().enabled = false;
    }

    private void Update()
    {
        if(Dialogue == null)
            return;
        if(dialogueOrder >= dialogueList.Count)
            return;

        SoundEffect();
        Choice();
        speakerName.text = dialogueList[dialogueOrder].Speaker;
        speakerText.text = dialogueList[dialogueOrder].Text;

        if (dialogueOrder == dialogueList.Count - 1)
        {
            DialogueButton.GetComponent<Text>().text = "대화를 끝내려면 여기를 누르세요.";
            DialogueButton.GetComponent<Button>().enabled = true;
        }
    }

    private void SoundEffect()
    {
        if(dialogueList[dialogueOrder].Speaker == "Sound")
        {
            SEAudio.PlayOneShot(Resources.Load(dialogueList[dialogueOrder].Text.Trim(), typeof(AudioClip)) as AudioClip);
            dialogueOrder += 1;
        }
    }

    private void Choice()
    {
        if(dialogueList[dialogueOrder].Speaker == "Choice")
        {
            DialogueButton.SetActive(false);
            ChoicePanel.SetActive(true);
            dialogueOrder -= 1;
        }
    }

    public void DialogueEnd()
    {
        mainCamera.transform.position = new Vector3(0,0,-10);
        mainCamera.GetComponent<Camera>().orthographicSize = 8f;
    }

    public void NextText()
    {
        dialogueOrder++;
    }
}


