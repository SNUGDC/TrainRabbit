using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterTalkWithPostgraduate : MonoBehaviour
{
	static public bool isTalked = false;

	private GameObject MapButton;
	private GameObject RightDoor;

	private void Start()
	{
		MapButton = GameObject.Find("Map Button");
		RightDoor = GameObject.Find("Right Door");
		MapButton.SetActive(false);
		RightDoor.tag = "Untagged";
	}

	private void Update()
	{
		if(isTalked == true)
		{
			MapButton.SetActive(true);
			RightDoor.tag = "Door";
			Destroy(GameObject.FindGameObjectWithTag("Postgraduate Rabbit"));
		}
	}

	public void TalkingFinished()
	{
		isTalked = true;
	}
}