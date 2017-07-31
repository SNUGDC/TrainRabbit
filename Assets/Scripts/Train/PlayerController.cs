using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public static Vector2 movingVector;
	public float HP;
	public int AP;
	public int Conscience;
	public GameObject AttackCollider;
	public float moveSpeed;

	private float HPDecreasePush = 0.05f;
    private Animator animator;

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

        animator = GetComponent<Animator>();
	}
	
	private void Update()
	{
		//앞에 있는 토끼는 앞에 있도록 해줍니다.
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f);

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

        if (movingVector != Vector2.zero)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
    }

	public void Attack()
	{
		foreach (GameObject rabbit in AttackCollider.GetComponent<GetObjectToBeAttacked>().RabbitToBeAttacked)
		{
			rabbit.GetComponent<BasicRabbitController>().HP -= AP;
			if(rabbit.tag == "Normal Rabbit")
			{
				Conscience = Conscience - 10;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "Door") //문에 부딪치면 다음 열차 차량으로 넘어가는 코드
		{
			GameObject door = coll.gameObject;
            bool isRightDoor;

            if (door.GetComponent<Door>().doorPosition == Door.DoorPosition.Right)
                isRightDoor = true;
            else
                isRightDoor = false;
			
			Train NowTrain = GameObject.Find("Train").GetComponent<Train>();
            //Debug.Log(GameObject.Find("Train").GetComponent<Train>().IsThereNextTrain(isRightDoor));
			if(NowTrain.IsThereNextTrain(isRightDoor) && isRightDoor == true)
			{
				Debug.Log(Train.trainNumber + "에서 " + (Train.trainNumber - 1) + "으로 넘어갑니다.");
				Train.trainNumber -= 1;
				Scene scene = SceneManager.GetActiveScene(); //현재 씬 가져오기
				SceneManager.LoadScene(scene.name);
			}
		}
	}

	private void OnCollisionStay2D(Collision2D coll)
	{
		if(coll.gameObject.GetComponent<BasicRabbitController>() != null)
		{
			HP -= HPDecreasePush;
		}
	}
}