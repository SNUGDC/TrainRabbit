using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectToBeAttacked : MonoBehaviour
{
	public List<GameObject> RabbitToBeAttacked;
	private GameObject rabbit;

	private void Start()
	{
		rabbit = transform.parent.gameObject;
		if(rabbit.tag == "Normal Rabbit")
		{
			gameObject.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag != "Attacked Collider")
			return;

		if(RabbitToBeAttacked.Contains(coll.gameObject.transform.parent.gameObject) == false)
		{
			RabbitToBeAttacked.Add(coll.gameObject.transform.parent.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag != "Attacked Collider")
			return;

		if(RabbitToBeAttacked.Contains(coll.gameObject.transform.parent.gameObject) == true)
		{
			RabbitToBeAttacked.Remove(coll.gameObject.transform.parent.gameObject);
		}
	}
}