using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static Vector2 movingVector;
	public float moveSpeed;

	private void Start()
	{
		movingVector = Vector2.zero;
	}
	
	private void Update()
	{
		transform.position = new Vector2(transform.position.x + movingVector.x * Time.deltaTime * moveSpeed, transform.position.y + movingVector.y * Time.deltaTime * moveSpeed);
	}
}