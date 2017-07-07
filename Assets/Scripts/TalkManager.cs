using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
	public bool isMeetPostgraduateRabbit;
	public bool isClickPostgraduateRabbit;

	private void Start()
	{
		isMeetPostgraduateRabbit = false;
		isClickPostgraduateRabbit = false;
	}

	private void Update()
	{
		if(isMeetPostgraduateRabbit == true && isClickPostgraduateRabbit == true)
		{
			Debug.Log("원생 토끼랑 대화 시작!");
			isClickPostgraduateRabbit = false;
		}
	}
}