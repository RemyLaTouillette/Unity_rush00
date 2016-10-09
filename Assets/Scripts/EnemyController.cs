using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public Sprite[]			heads;
	public Sprite[]			bodies;
	public GameObject[]		weapons;

	GameObject				weapon;
	SpriteRenderer	sprite_weap;
	public float			speed = 2.0f;

	public bool				isAlive = true;
	public static int		maxHp = 2;
	int						hp;
	
	Rigidbody2D	ctrl;
	GameObject	feet;

	[HideInInspector] public enum Status {idle, patrol, hunting, search, back, follow};
	[HideInInspector] public Status currentStatus = Status.idle;
	[HideInInspector] public Vector2 startPosition;
	[HideInInspector] public Status startStatus;
	public bool detectPlayer = false;
	
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
		sprite_weap = transform.Find ("Weapon").gameObject.GetComponent<SpriteRenderer> ();
		EquipWeapon (weapon);
	}

	void Update () {

		if (detectPlayer) {
			currentStatus = EnemyController.Status.follow;
		}

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
	
	public void EquipWeapon(GameObject weapon) {
		//transform.Find("Weapon").gameObject = weapon;
		//this.weapon = GameObject.Instantiate(weapon);
		this.weapon = weapon;
		sprite_weap.sprite = weapon.GetComponent<weaponScript> ().sprite;
		//weapon.GetComponent<weaponScript> ().last_shoot = 0.0f;
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


}
