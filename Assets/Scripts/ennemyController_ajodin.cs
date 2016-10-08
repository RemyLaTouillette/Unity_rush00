using UnityEngine;
using System.Collections;

public class ennemyController_ajodin : MonoBehaviour {
	public Sprite[]			heads;
	public Sprite[]			bodies;
	public GameObject[]		weapons;

	public GameObject		weapon;
	public GameObject		obj;
	public GameObject		punch;
	public SpriteRenderer	sprite_weap;
	public float			speed = 6F;
	public Vector3			lookAt ;
	
	private Rigidbody2D	ctrl;
	private GameObject	feet;
	
	// Variables

	private Vector3	direction;

	// Use this for initialization
	void Start () {
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

	}
	
	void BasicMovement() {
		// WASD movements
		Vector2 move = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		move *= speed;
		ctrl.velocity = Vector2.ClampMagnitude (move, speed);
		// Mouse movement

		//TODO : Reimplement lookAt to Follow the player

		float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg - 90f;
		ctrl.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		direction = Vector3.Normalize (-lookAt);
		// Feet's animation
		feet.GetComponent<Animator> ().SetFloat ("speed", ctrl.velocity.magnitude / 3f);
	}
	
	public void EquipWeapon(GameObject weapon) {
		//transform.Find("Weapon").gameObject = weapon;
		//this.weapon = GameObject.Instantiate(weapon);
		this.weapon = weapon;
		sprite_weap.sprite = weapon.GetComponent<weaponScript> ().sprite;
		//weapon.GetComponent<weaponScript> ().last_shoot = 0.0f;
	}

}
