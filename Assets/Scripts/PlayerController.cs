using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static Vector2 movingVector;
	public int Conscience;
	public GameObject AttackCollider;
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
		
		if (movingVector.x > 0f)
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else if (movingVector.x < 0f)
		{
			transform.rotation = Quaternion.Euler(0, 180, 0);
		}
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

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "Door")
		{
			GameObject door = coll.gameObject;

			Debug.Log(door.GetComponent<Door>().doorPosition);
		}
	}

	private void OnCollisionStay2D(Collision2D coll)
	{
		if(coll.gameObject.GetComponent<BasicRabbitController>() != null)
		{
			GetComponent<BasicRabbitController>().HP -= HPDecreasePush;
		}
	}
}