using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	
	// GameObject
	public GameObject		weapon;
	public GameObject		obj;
	public GameObject		punch;
	public SpriteRenderer	sprite_weap;

	private Rigidbody2D	ctrl;
	private GameObject	feet;
	
	// Variables
	private float	speed;
	private Vector3	direction;
	
	
	// Use this for initialization
	void Start () {
		// Init GameObject
		ctrl = GetComponent<Rigidbody2D> ();
		feet = transform.Find("Feet").gameObject;
		sprite_weap = transform.Find ("Weapon").gameObject.GetComponent<SpriteRenderer> ();

		// Init variables
		speed = 6f;

		EquipWeapon (weapon);
		//UnequipWeapon ();
	}
	
	// Update is called once per frame
	void Update () {

		BasicMovement ();

		//
		if (Input.GetButton ("Fire1")) {
			if (weapon == null) {
				Vector3 v = direction;
				Vector3 p = transform.Find ("Punch").gameObject.transform.position;
				Quaternion ori = GetComponent<Rigidbody2D>().transform.rotation;
				//GameObject b = (GameObject)Instantiate(punch, p, Quaternion.identity);
				punch.GetComponent<weaponScript>().TryToShoot(p, v, ori);
			} else {
				Vector3 v = direction;
				Vector3 p = transform.Find ("Spawn").gameObject.transform.position;
				weapon.GetComponent<weaponScript>().TryToShoot(p, v, Quaternion.identity);
			}
			//Debug.Log("Try to shoot");
		}
		if (Input.GetButtonDown ("Fire2")) {
			Vector3 v = direction;
			Vector3 p = transform.Find ("Spawn").gameObject.transform.position;


			//Debug.Log("Try to drop weapon");
			if (obj != null) {
				GameObject b = (GameObject)Instantiate(obj, p, Quaternion.identity);
				b.GetComponent<Rigidbody2D>().AddForce(v * 300f);
				b.GetComponent<Rigidbody2D>().AddTorque(-20f);

				UnequipWeapon();

			}
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			Debug.Log ("Try to get weapon");

			RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.position);
			if (hits.Length > 1) {
				foreach (RaycastHit2D hit in hits) {
					if (hit.collider.tag == "Weapon") {
						//EquipWeapon(hit.collider.gameObject);
						//GameObject.Destroy(hit.collider.gameObject);
					}
				}
			}
		}
	}

	void BasicMovement() {
		// WASD movements
		Vector2 move = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		move *= speed;
		ctrl.velocity = Vector2.ClampMagnitude (move, speed);
		// Mouse movement
		Vector3 lookAt = transform.position - Camera.main.ScreenToWorldPoint (Input.mousePosition);
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

	public void UnequipWeapon() {
		this.weapon = null;
		sprite_weap.sprite = null;
		obj = null;
	}
}
