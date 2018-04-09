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

    public static bool isTalking = false;

    private List<Dialogue> dialogueList = new List<Dialogue>();
    private GameObject mainCamera;
    private AudioSource SEAudio;
    private GameObject QuestAccept;
    private GameObject QuestRefuse;
    private GameObject Player;

    private GameObject dialoguePanel, movingPad, attackPad, forKinder;
    private GameObject bubbleObject;
    /*void Awake()
    {
        dialoguePanel = GameObject.Find("Dialogue Panel");
        movingPad = GameObject.Find("Moving Pad");
        attackPad = GameObject.Find("Attack Pad");
        forKinder = GameObject.Find("For Kinder Stage");
    }*/
    private void Start()
    {
		Player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GameObject.Find("Main Camera");
        SEAudio = GetComponent<AudioSource>();
        QuestAccept = ChoicePanel.transform.Find("Yes").gameObject;
        QuestRefuse = ChoicePanel.transform.Find("No").gameObject;

        ReadDialogue();
    }

    public void ReadDialogue(GameObject parent = null)
    {
        if(parent != null) bubbleObject = parent;
        TextReader reader = new TextReader();
        reader.Parse(Dialogue);
        dialogueList = reader.dialogueList;
        isTalking = true;

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

            PlayerData.HP += status_Int[0];
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
		        SoundManager.SetIsOnTrain(false);
                SoundManager.PlayOtherMusic(MusicType.stageClear);
                break;
            }
        }
    }

    public void DialogueEnd()
    {
        Debug.Log("Dialogue End!");
        var playerAge = FindObjectOfType<TrainGenerator>().playerAge;
        if(!(playerAge == PlayerStatus.PlayerAge.Happy || playerAge == PlayerStatus.PlayerAge.Sad)) SoundManager.ResumeMainMusic();
        
        isTalking = false;
		if(!Player.GetComponent<PlayerController>().isQuest){
			Destroy(bubbleObject);
		}
        mainCamera.transform.position = new Vector3(0,0,-10);
        mainCamera.GetComponent<Camera>().orthographicSize = 8f;

        /*dialoguePanel.SetActive(false);
        movingPad.SetActive(true);
        attackPad.SetActive(true);
        if (forKinder != null)
        {
            forKinder.GetComponent<AfterTalkWithPostgraduate>().TalkingFinished();
        }*/

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
            SoundManager.PlayTalk();
        }
        Debug.Log(dialogueOrder+" / "+dialogueList.Count);
        /*if (dialogueOrder >= dialogueList.Count)
        {
            DialogueEnd();
        }*/
    }
}


