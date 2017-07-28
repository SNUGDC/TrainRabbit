using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRabbitController : MonoBehaviour
{
	public float HP; //체력
	public float AP; //공격력
	public float moveSpeed = 1f;

	private float movingTime;
	private float waitingTime;
	public float gameTime;
	private Vector2 movingVector = new Vector2 (0, 0);

	private void Start()
	{
		gameTime = 0f;
	}

	private void Update()
	{
		if(gameTime == 0f)
		{
			movingVector = DecideBackAndForthVector();
			movingTime = Random.Range(0.3f, 1.2f);
			waitingTime = Random.Range(2f, 6f);
			gameTime += Time.deltaTime;
		}
		else if(gameTime <= movingTime)
		{
			CheckInside(movingVector);
			transform.position = (Vector2)transform.position + movingVector;
			gameTime += Time.deltaTime;
		}
		else if(gameTime < waitingTime)
		{
			gameTime += Time.deltaTime;
		}
		else if (gameTime >= waitingTime)
		{
			gameTime = 0f;
		}
			
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f);

		if (HP <= 0)
		{
			Destroy(gameObject);
		}
	}

	private Vector2 DecideBackAndForthVector()
	{
		Vector2 movingVector;
		float angle = Random.Range(0f, 360f);

		movingVector = new Vector2 (Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));

		return movingVector * moveSpeed * Time.deltaTime;
	}

	private void CheckInside(Vector2 movingVector)
	{
		if(transform.position.y <= -4f || transform.position.y >= 3f)
			movingVector.y = 0;
		if(transform.position.x <= -12.5f || transform.position.x >= 12.5f)
			movingVector.x = 0;

		if(movingVector.x >= 0f)
			transform.rotation = Quaternion.Euler(0, 0, 0);
		else if(movingVector.x < 0f)
			transform.rotation = Quaternion.Euler(0, 180, 0);
	}

	private void Move()
	{
	}
}