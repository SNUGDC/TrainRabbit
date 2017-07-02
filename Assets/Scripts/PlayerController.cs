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

	private float HPDecreasePush = 0.02f;

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
		if(transform.position.y >= 3f && movingVector.y > 0)
		{
			movingVector = new Vector2 (movingVector.x, 0);
			transform.position = new Vector2 (transform.position.x, 3f);
		}
		else if (transform.position.y <= -5 && movingVector.y < 0)
		{
			movingVector = new Vector2 (movingVector.x, 0);
			transform.position = new Vector2 (transform.position.x, -5f);
		}

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

	private void OnCollisionStay2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "Normal Rabbit")
		{
			GetComponent<BasicRabbitController>().HP -= HPDecreasePush;
		}
	}
}