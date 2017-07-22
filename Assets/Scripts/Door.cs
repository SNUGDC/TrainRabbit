using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	public enum DoorPosition
	{
		Right, Left
	}

	public DoorPosition doorPosition;
}