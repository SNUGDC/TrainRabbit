using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovingCursorController : MonoBehaviour
{
	private Vector2 origin;
	private Vector2 movingVector;

	private float widthPixels;
    Touch[] myTouch;

	void Awake()
	{
		widthPixels = Screen.currentResolution.width;
	}
	private void Start()
	{
		origin = transform.position;
		movingVector = Vector2.zero;
	}

	private void Update()
	{
        myTouch = Input.touches;
		TellMovingVectorToPlayer(movingVector);
	}

	public void TrackingMouse()
	{
        if (myTouch.Any())
        {
            Vector2 mousePos = myTouch.Where(a => a.position.x < widthPixels * 0.5).First().position;
            movingVector = new Vector2(mousePos.x, mousePos.y) - origin;
        }
		
		transform.position = CursorPos(movingVector);
	}

	public void GoToOrigin()
	{
		transform.position = origin;
		movingVector = Vector2.zero;
	}

	private Vector2 CursorPos(Vector2 movingVector)
	{
		if(movingVector.magnitude <= widthPixels * 0.07f)
		{
			return movingVector + origin;
		}

		return origin + movingVector.normalized * widthPixels * 0.07f;
	}

	private void TellMovingVectorToPlayer(Vector2 movingVector)
	{
		PlayerController.movingVector = movingVector.normalized;
	}
}