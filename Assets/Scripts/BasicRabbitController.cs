using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRabbitController : MonoBehaviour
{
	public int HP; //체력
	public int AP; //공격력

	private void Update()
	{
		if (HP <= 0)
		{
			Destroy(gameObject);
		}
	}
}