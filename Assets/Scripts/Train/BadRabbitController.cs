using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BadRabbit { Drunken, Gongik, Hentai, Jeondomon, Merchant}

public class BadRabbitController : MonoBehaviour
{
	    public BadRabbit badRabbit;
	public float HP; //체력
	public float AP; //공격력
	public float moveSpeed = 1f;

	private float movingTime;
	private float waitingTime;
	private float gameTime;
	private Vector2 movingVector = new Vector2 (0, 0);
    private Transform player;

    private SpriteRenderer spriteRenderer;

	    public TrainGenerator tr;

	private void Start()
	{
        gameTime = Random.Range(-4f, -1f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
        float step = moveSpeed * Time.deltaTime;
        if ((transform.position - player.position).magnitude > 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
       
        if ((transform.position - player.position).magnitude < 3f)
        {
            GetComponent<Animator>().SetTrigger("attack");
            PlayerController.HP -= AP * Time.deltaTime; 
        }

        LookEachOther();


        /*
        if (isSeat == false && isTalking == false)
		{
			MoveBackAndForth();
		}
		*/
        spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100f);
        

		if (HP <= 0)
		{            if(badRabbit == BadRabbit.Gongik)
            {
                tr.GongikInstanceCooltime = 0f;
            }
            FindObjectOfType<MusicManager>().PlayDeath();
            Destroy(gameObject);
		}
	}

    
    private void LookEachOther()
    {
        Vector3 playerPos = player.position;
        Vector3 myPos = transform.transform.position;

        if (myPos.x > playerPos.x)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    

    /*
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

	private void MoveBackAndForth()
	{
		if(gameTime == 0f)
		{
			movingVector = DecideBackAndForthVector();
			movingTime = Random.Range(0.3f, 1.2f);
			waitingTime = Random.Range(2f, 10f);
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
	}
    */
}