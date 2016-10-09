using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public Sprite[]			heads;
	public Sprite[]			bodies;
	public GameObject[]		weapons;


	GameObject				weapon;
	public float 			range = 3.0f;
	public float			speed = 2.0f;

	bool			isAlive = true;
	static int		maxHp = 2;
	int				hp;
	
	Rigidbody2D	ctrl;
	GameObject	feet;

	[HideInInspector] public enum Status {idle, patrol, hunting, search, back, follow};
	public Status currentStatus = Status.idle;
	[HideInInspector] public Vector2 startPosition;
	[HideInInspector] public Status startStatus;

	GameObject targetDetected = null;
	float 	targetDistance;
	Vector2 targetPosition;
	
	// Variables

	private Vector3	direction;

	// Use this for initialization
	void Start () {
		hp = maxHp;
		startPosition = (Vector2)transform.position;
		startStatus = currentStatus;
		SpriteRenderer[] children = gameObject.GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer child in children)
			if (child.tag == "Head")
				child.sprite = heads[Random.Range (0, heads.Length)];
		gameObject.GetComponent<SpriteRenderer>().sprite = bodies[Random.Range(0, bodies.Length)];
		weapon = weapons[Random.Range (0, weapons.Length)];
		ctrl = GetComponent<Rigidbody2D> ();
		feet = transform.Find("Feet").gameObject;
		//weapon = transform.Find ("Weapon").gameObject.GetComponent<enemyWeaponScript> ();
	}

	void Update () {

		if (targetDetected != null) {
			targetDistance = Vector2.Distance((Vector2)targetDetected.transform.position, (Vector2)transform.position);
			//if (playerDistance < range) <---------------------SHOOT HERE 
			//	try To shoot ???
		}
		if (currentStatus == Status.follow && targetDetected != null && (Vector2)targetDetected.transform.position != targetPosition && targetDistance > range)
			GetComponent<HuntingController> ().GoToTarget ((Vector2)targetDetected.transform.position);

		if (hp == 0 && isAlive == true)
		{			
			dieNow ();
		}
	}

	public void isMoving()
	{	
		feet.GetComponent<Animator> ().SetFloat ("speed", currentStatus == Status.patrol ? speed / 2.0f : speed);
	}

	public void isStaying()
	{
		feet.GetComponent<Animator> ().SetFloat ("speed", 0.0f);		
	}

	public void DetectPlayer(GameObject player)
	{
		targetDetected = player;
		targetPosition = targetDetected.transform.position;
		currentStatus = Status.follow;
		GetComponent<HuntingController> ().GoToTarget ((Vector2)targetPosition);
	}

	public void lostDetection(Vector2 position)
	{
		targetDetected = null;
		Alerted (position);
	}

	public void Alerted(Vector2 target)
	{
		currentStatus = Status.hunting;
		GetComponent<HuntingController> ().GoToTarget (target);
	}

	public void Lookat(Vector2 at)
	{
		transform.rotation = Quaternion.LookRotation (Vector3.forward, at);
	}

	public void dieNow()
	{
		isAlive = false;
		Debug.Log("ARGH");
		Destroy(gameObject);
	}

	public bool isInRange()
	{
		if (targetDetected != null && targetDistance < range)
			return true;
		return false;
	}


}
