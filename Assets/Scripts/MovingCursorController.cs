using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCursorController : MonoBehaviour
{
	private Vector2 origin;
	private Vector2 movingVector;

	private void Start()
	{
		origin = transform.position;
		movingVector = Vector2.zero;
	}

	private void Update()
	{
		TellMovingVectorToPlayer(movingVector);
	}

	public void TrackingMouse()
	{
		movingVector = new Vector2 (Input.mousePosition.x, Input.mousePosition.y) - origin;
		
		transform.position = CursorPos(movingVector);
	}

	public void GoToOrigin()
	{
		transform.position = origin;
		movingVector = Vector2.zero;
	}

	private Vector2 CursorPos(Vector2 movingVector)
	{
		if(movingVector.magnitude <= 75f)
		{
			return movingVector + origin;
		}

		return origin + movingVector.normalized * 75;
	}

	private void TellMovingVectorToPlayer(Vector2 movingVector)
	{
		PlayerController.movingVector = movingVector.normalized;
	}
}