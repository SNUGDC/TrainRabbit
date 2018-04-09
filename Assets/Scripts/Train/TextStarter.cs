using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextStarter : MonoBehaviour
{
	public TextAsset[] Dialogue;
    private GameObject MovingPad;
    private GameObject AttackPad;
    private GameObject DialoguePanel;
    private GameObject MainCamera;
	private GameObject Player;
	private GameObject TalkCollider;
	private bool isSeat;
    private BasicRabbitController brc;

    private void Start()
    {
        brc = GetComponentInParent<BasicRabbitController>();
        GameObject canvas = GameObject.Find("Canvas");
        MovingPad = canvas.transform.Find("Moving Pad").gameObject;
        AttackPad = canvas.transform.Find("Attack Pad").gameObject;
        DialoguePanel = canvas.transform.Find("Dialogue Panel").gameObject;
        MainCamera = GameObject.Find("Main Camera");
		Player = GameObject.FindGameObjectWithTag("Player");
		isSeat = gameObject.transform.parent.gameObject.GetComponent<BasicRabbitController>().isSeat;
    }

	private void Update()
	{
		if(Input.GetMouseButtonUp(0))
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 myPos = transform.position;

			if(Vector2.Distance(mousePos, myPos) <= 1f)
			{
				ClickBubble();
			}
		}
	}

	private void ClickBubble()
	{
		Debug.Log("말풍선을 누름");
		TalkCollider = gameObject.transform.parent.transform.Find("Talk Collider").gameObject;
		bool isMeetPlayer = TalkCollider.GetComponent<InteractionWithPlayer>().isMeetPlayer;
        
		if(isMeetPlayer == true)
		{
			DialogueStart();
			Debug.Log("대화 시작!");
		}
	}

	private void DialogueStart()
	{
		var playerAge = FindObjectOfType<TrainGenerator>().playerAge;
        if(!(playerAge == PlayerStatus.PlayerAge.Happy || playerAge == PlayerStatus.PlayerAge.Sad)) SoundManager.PlayOtherMusic(brc.rabbit);
		SoundManager.PlayTalk();

		MovingPad.SetActive(false);
		AttackPad.SetActive(false);
		LookEachOther(isSeat);
		CloseUp();
		DialoguePanel.SetActive(true);
		PassDialogue();
		Debug.Log("TextStarter.DialogueStart");
		DialoguePanel.GetComponent<DialogueController>().ReadDialogue(gameObject);
		DialoguePanel.GetComponent<DialogueController>().dialogueOrder = 0;
		gameObject.transform.parent.GetComponent<BasicRabbitController>().isTalking = true;
	}

	private void PassDialogue()
	{
		if(Dialogue.Length <= 1)
		{
			DialoguePanel.GetComponent<DialogueController>().Dialogue = Dialogue[0];
			return;
		}

		if(Player.GetComponent<PlayerController>().isQuest == false)
		{
			DialoguePanel.GetComponent<DialogueController>().Dialogue = Dialogue[0];
		}
		else if(Player.GetComponent<PlayerController>().isQuest == true)
		{
			if(Player.GetComponent<PlayerController>().isQuestComplete == false)
			{
				DialoguePanel.GetComponent<DialogueController>().Dialogue = Dialogue[1];
			}
			else if(Player.GetComponent<PlayerController>().isQuestComplete == true)
			{
				DialoguePanel.GetComponent<DialogueController>().Dialogue = Dialogue[2];
			}
		}
	}

	private void CloseUp()
	{
		MainCamera.GetComponent<Camera>().orthographicSize = 5f;

		Vector3 playerPos = Player.transform.position;
		Vector3 myPos = transform.parent.transform.position;

		MainCamera.transform.position = (playerPos + myPos) / 2 + new Vector3 (0,0,-10);
	}

	private void LookEachOther(bool isSeat)
	{
		if(isSeat == true)
			return;

		Vector3 playerPos = Player.transform.position;
		Vector3 myPos = transform.parent.transform.position;

		if(myPos.x > playerPos.x)
		{
			transform.parent.transform.rotation = Quaternion.Euler(0, 180, 0);
		}
		else
		{
			transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}

	public void DialogueEnd()
	{
        SoundManager.ResumeMainMusic();
		MainCamera.GetComponent<Camera>().orthographicSize = 8f;
		MainCamera.transform.position = new Vector3(0, 0f, -10f);
		MovingPad.SetActive(true);
		AttackPad.SetActive(true);
		DialoguePanel.SetActive(false);
	}
}