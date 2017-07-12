using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
	public GameObject MovingPad;
	public GameObject AttackPad;
	public GameObject DialoguePanel;
	public GameObject MainCamera;

	public bool isMeetPostgraduateRabbit;
	public bool isClickPostgraduateRabbit;

	private GameObject Player;

	private void Start()
	{
		isMeetPostgraduateRabbit = false;
		isClickPostgraduateRabbit = false;
		Player = GameObject.FindWithTag("Player").gameObject;
		DialoguePanel.SetActive(false);
	}

	private void Update()
	{
		if(isMeetPostgraduateRabbit == true && isClickPostgraduateRabbit == true)
		{
			Debug.Log("원생 토끼랑 대화 시작!");
			DialogueStart();
			isClickPostgraduateRabbit = false;
		}
	}

	private void DialogueStart()
	{
		MovingPad.SetActive(false);
		AttackPad.SetActive(false);
		CloseUp();
		DialoguePanel.SetActive(true);
	}

	private void CloseUp()
	{
		MainCamera.GetComponent<Camera>().orthographicSize = 5f;
		MainCamera.transform.position = Player.transform.position + new Vector3(1, -1.5f, -10f);
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