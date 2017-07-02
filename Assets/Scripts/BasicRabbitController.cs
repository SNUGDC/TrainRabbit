using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRabbitController : MonoBehaviour
{
	public float HP; //체력
	public float AP; //공격력

	private void Start()
	{
		
	}

	private void Update()
	{
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f);

		if (HP <= 0)
		{
			Destroy(gameObject);
		}
	}
}