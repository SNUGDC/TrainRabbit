using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static Vector2 movingVector;
	public int Conscience;
	public GameObject AttackCollider;
	//public GameObject AttackedCollider;
	public float moveSpeed;

	private void Start()
	{
		if(PlayerPrefs.HasKey("Conscience") == false)
		{
			Conscience = 1000;
		}
		else
		{
			Conscience = PlayerPrefs.GetInt("Conscience");
		}
		
		movingVector = Vector2.zero;
	}
	
	private void Update()
	{
		transform.position = new Vector2(transform.position.x + movingVector.x * Time.deltaTime * moveSpeed, transform.position.y + movingVector.y * Time.deltaTime * moveSpeed);
	}

	public void Attack()
	{
		foreach (GameObject rabbit in AttackCollider.GetComponent<GetObjectToBeAttacked>().RabbitToBeAttacked)
		{
			rabbit.GetComponent<BasicRabbitController>().HP -= gameObject.GetComponent<BasicRabbitController>().AP;
			if(rabbit.tag == "Normal Rabbit")
			{
				Conscience = Conscience - 10;
			}
		}
	}
}