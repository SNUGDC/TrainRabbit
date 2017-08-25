using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

static class PlayerData
{
    public static int Conscience { get; set; }
}

public class PlayerController : MonoBehaviour
{
    //public AudioClip hitSound;
    //private MusicManager theMM;

    public static Vector2 movingVector;
	static public float HP = 100;
	public int AP;
	public GameObject AttackCollider;
	public float moveSpeed;
	public bool isQuest;
	public bool isQuestComplete;

	private float HPDecreasePush = 0.1f;
    private Animator animator;

	private void SetConscience()
	{
		TrainGenerator trainGenerator = GameObject.Find("Train Generator").GetComponent<TrainGenerator>();
		int trainNum = trainGenerator.trainNum;

		if(trainNum != 20)
			return;

		PlayerStatus.PlayerAge playerAge = trainGenerator.playerAge;
		string ConscienceKey = "Conscience_" + playerAge.ToString();

		if(PlayerPrefs.HasKey(ConscienceKey))
		{
			PlayerData.Conscience = PlayerPrefs.GetInt(ConscienceKey);
		}
		else
		{
			Debug.Log("유치원 스테이지가 아니라면 정상적이지 않은 스테이지 접근!! 토성 100으로 시작합니다.");
			PlayerData.Conscience = 100;
		}
	}

	private void Start()
	{
		isQuest = false;
		isQuestComplete = false;
		movingVector = Vector2.zero;

        animator = GetComponent<Animator>();
		SetConscience();
		Debug.Log(PlayerData.Conscience);

        //theMM = FindObjectOfType<MusicManager>();
    }
	
	private void Update()
	{
		MoveWithKeyboard();
		MaxMinStatus();
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

        if(HP < 0.01f)
        {
            SceneManager.LoadScene("Gameover Scene");
        }
    }

	private void MaxMinStatus()
	{
		if(PlayerData.Conscience >= 100)
		{
			PlayerData.Conscience = 100;
		}
		if(PlayerData.Conscience <= 0)
		{
			PlayerData.Conscience = 0;
		}
		if(PlayerController.HP >= 100)
		{
			PlayerController.HP = 100;
		}
		if(PlayerController.HP <= 0)
		{
			Debug.Log("플레이어가 지쳐서 쓰러져부렀다...");
		}
	}

	private void MoveWithKeyboard()
	{
		if(Input.GetKey(KeyCode.RightArrow))
		{
			movingVector = new Vector2(1,0);
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			movingVector = new Vector2(-1,0);
		}
		if(Input.GetKey(KeyCode.UpArrow))
		{
			movingVector = new Vector2(0,1);
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			movingVector = new Vector2(0,-1);
		}
	}

    public void Attack()
    {
        foreach (GameObject rabbit in AttackCollider.GetComponent<GetObjectToBeAttacked>().RabbitToBeAttacked)
        {
            if (rabbit.name.Contains("Bad"))
            {
                rabbit.GetComponent<BadRabbitController>().HP -= AP;
            }
            else
            {
                rabbit.GetComponent<BasicRabbitController>().HP -= AP;
                PlayerData.Conscience = PlayerData.Conscience - 1;
            }
            /*
            if (rabbit.name.Contains("Bunny"))
            {
                Conscience = Conscience - 1;
                PlayerData.Conscience = Conscience;
            }
            */

        }

        animator.SetTrigger("attack");

        FindObjectOfType<MusicManager>().PlayHit();
        /*
        if (hitSound != null)
        {
            theMM.ChangeMusic(hitSound);
        }
        */
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

	public void GetQuest()
	{
		isQuest = true;
	}

    public void reset()
    {
        PlayerController.HP = 100;
        PlayerData.Conscience = 100;

		PlayerPrefs.DeleteAll();
    }
}