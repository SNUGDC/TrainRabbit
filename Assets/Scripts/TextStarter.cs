using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextStarter : MonoBehaviour
{
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
    }

	private void OnMouseDown()
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
		CloseUp();
		DialoguePanel.SetActive(true);
		gameObject.transform.parent.GetComponent<DialogueController>().dialogueOrder = 0;
	}

	private void CloseUp()
	{
		MainCamera.GetComponent<Camera>().orthographicSize = 5f;
		//MainCamera.transform.position = Player.transform.position + new Vector3(1, -1.5f, -10f);
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