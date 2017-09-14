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
    public GameObject ClearPanel;

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

        ReadDialogue();
    }

    public void ReadDialogue()
    {
        TextReader reader = new TextReader();
        reader.Parse(Dialogue);
        dialogueList = reader.dialogueList;

        DialogueButton.SetActive(true);
        DialogueButton.GetComponent<Text>().text = "다음";
        DialogueButton.GetComponent<Button>().enabled = false;
        ChoicePanel.SetActive(false);
    }

    private void Update()
    {
        if(Dialogue == null)
            return;
        if(dialogueOrder >= dialogueList.Count)
        {
            DialogueButton.GetComponent<Text>().text = "대화를 끝내려면 여기를 누르세요.";
            DialogueButton.GetComponent<Button>().enabled = true;
            return;
        }

        UpdateDialogue();
    }

    private void UpdateDialogue()
    {
        if(dialogueOrder >= dialogueList.Count)
            return;

        speakerName.text = dialogueList[dialogueOrder].Speaker;
        speakerText.text = dialogueList[dialogueOrder].Text;

        SoundEffect();
        Choice();
        StatusChange();
        ImagePopUp();
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
            QuestAccept.GetComponent<QuestManager>().QuestItemName = dialogueList[dialogueOrder].Text.Trim();
            dialogueOrder -= 1;
        }
    }

    private void StatusChange()
    {
        if(dialogueList[dialogueOrder].Speaker == "Status")
        {
            string[] status_String = dialogueList[dialogueOrder].Text.Split(',');
            int[] status_Int = new int[status_String.Length];

            for(int i = 0; i < status_String.Length; i++)
            {
                status_Int[i] = System.Convert.ToInt32(status_String[i].Trim());
            }

            PlayerController.HP += status_Int[0];
            Debug.Log(PlayerData.Conscience);
            PlayerData.Conscience += status_Int[1];
            Debug.Log(PlayerData.Conscience);

            speakerName.text = "System";
            speakerText.text = "체력이 " + status_Int[0] + "만큼 토성이 " + status_Int[1] + "만큼 회복되었다.";
            dialogueOrder += 1;
        }
    }

    private void ImagePopUp()
    {
        if(dialogueList[dialogueOrder].Speaker == "Image")
        {
            switch(dialogueList[dialogueOrder].Text.Trim())
            {
                case "Clear":
                ClearPanel.SetActive(true);
                gameObject.SetActive(false);
                break;
            }
        }
    }

    public void DialogueEnd()
    {
        MusicManager.ResumeMainMusic();
        mainCamera.transform.position = new Vector3(0,0,-10);
        mainCamera.GetComponent<Camera>().orthographicSize = 8f;
    }

    public void NextText()
    {
        dialogueOrder++;
        
        if (!(dialogueList[dialogueOrder].Speaker == "Sound"
            || dialogueList[dialogueOrder].Speaker == "Choice"
            || dialogueList[dialogueOrder].Speaker == "Status"
            || dialogueList[dialogueOrder].Speaker == "Image"
            || dialogueList[dialogueOrder].Speaker == "효과음"))
        {
            MusicManager.PlayTalk();
        }
    }
}


