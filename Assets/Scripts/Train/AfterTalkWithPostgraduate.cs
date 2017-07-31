using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterTalkWithPostgraduate : MonoBehaviour
{
	static public bool isTalked = false;

	private GameObject MapButton;

	private void Start()
	{
		MapButton = GameObject.Find("Map Button");
		MapButton.SetActive(false);
	}

	private void Update()
	{
		if(isTalked == true)
		{
			MapButton.SetActive(true);
			Destroy(GameObject.FindGameObjectWithTag("Postgraduate Rabbit"));
		}
	}

	public void TalkingFinished()
	{
		isTalked = true;
	}
}