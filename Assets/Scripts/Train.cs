using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
	public int trainNumber;

    public bool IsThereNextTrain(bool isRightDoor)
    {
        if (trainNumber == 10 && isRightDoor == false)
            return false;
        else if (trainNumber == 1 && isRightDoor == true)
            return false;

        return true;
    }
}