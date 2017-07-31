using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextStarter : MonoBehaviour
{
	public TextAsset Dialogue;
    private GameObject MovingPad;
    private GameObject AttackPad;
    private GameObject DialoguePanel;
    private GameObject MainCamera;
	private GameObject Player;

    private void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        MovingPad = canvas.transform.Find("Moving Pad").gameObject;
        AttackPad = canvas.transform.Find("Attack Pad").gameObject;
        DialoguePanel = canvas.transform.Find("Dialogue Panel").gameObject;
        MainCamera = GameObject.Find("Main Camera");
		Player = GameObject.FindGameObjectWithTag("Player");
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
		GameObject TalkCollider = gameObject.transform.parent.transform.Find("Talk Collider").gameObject;
		bool isMeetPlayer = TalkCollider.GetComponent<InteractionWithPlayer>().isMeetPlayer;

		if(isMeetPlayer == true)
		{
			DialogueStart();
			Debug.Log("대화 시작!");
		}
	}

	private void DialogueStart()
	{
		MovingPad.SetActive(false);
		AttackPad.SetActive(false);
		LookEachOther();
		CloseUp();
		DialoguePanel.SetActive(true);
		DialoguePanel.GetComponent<DialogueController>().Dialogue = Dialogue;
		DialoguePanel.GetComponent<DialogueController>().dialogueOrder = 0;
		gameObject.transform.parent.GetComponent<BasicRabbitController>().isTalking = true;
	}

	private void CloseUp()
	{
		MainCamera.GetComponent<Camera>().orthographicSize = 5f;

		Vector3 playerPos = Player.transform.position;
		Vector3 myPos = transform.parent.transform.position;

		MainCamera.transform.position = (playerPos + myPos) / 2 + new Vector3 (0,0,-10);
	}

	private void LookEachOther()
	{
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
		MainCamera.GetComponent<Camera>().orthographicSize = 8f;
		MainCamera.transform.position = new Vector3(0, 0f, -10f);
		MovingPad.SetActive(true);
		AttackPad.SetActive(true);
		DialoguePanel.SetActive(false);
	}
}