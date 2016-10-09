using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	
	private GameObject			box;

	private playerWeaponScript	weap;
	private SpriteRenderer		sr;
	private noWeaponScript		punch;
	private Rigidbody2D			ctrl;
	private GameObject			feet;
	private GameObject			bullet;
	
	// Variables
	private float	speed;
	private float	energy;
	private bool	canDash;
	private Vector2	direction;
	private bool	maxPayne;
	
	
	// Use this for initialization
	void Start () {
		// Init GameObject
		ctrl = GetComponent<Rigidbody2D> ();
		feet = transform.Find("Feet").gameObject;
		//sprite_weap = transform.Find ("Weapon").gameObject.GetComponent<SpriteRenderer> ();
		punch = transform.Find ("Punch").GetComponent<noWeaponScript> ();
		weap = transform.Find ("Weapon").GetComponent<playerWeaponScript>();
		sr = weap.GetComponent<SpriteRenderer> ();
		box = null;
		// Init variables
		speed = 6f;
		energy = 100f;
		canDash = true;
		maxPayne = true;

		if (box == null) {
			Debug.Log ("Cool");
		}

		//EquipWeapon (weapon);
		//UnequipWeapon ();
	}
	
	// Update is called once per frame
	void Update () {

		BasicMovement ();
		DashMovement ();

		//
		if (Input.GetButton ("Fire1")) {
			if (weap.isEquiped) {
				weap.Shoot(direction, transform.localRotation);
			} else
				Punch();
		}
		if (Input.GetButtonDown ("Fire2")) {
			if (box != null) {
				box.transform.position = transform.localPosition;
				box.SetActive(true);
				box.GetComponent<Rigidbody2D>().AddForce(direction * weap.power);
				box.GetComponent<Rigidbody2D>().AddTorque(-20f);
				box = null;
				sr.sprite = null;
				GameObject.Destroy(weap.bullet);
				weap.DropWeapon();
				//UnequipWeapon();
			}
		}

		EnergyGeneration (0.001f);
		//Debug.Log ("energy: " + energy);

		if (Input.GetKeyDown (KeyCode.E)) {
			Debug.Log ("Try to get weapon");

			if (!weap.isEquiped) {
				RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.position);
				if (hits.Length > 1) {
					foreach (RaycastHit2D hit in hits) {
						if (hit.collider.tag == "Weapon") {
							Debug.Log ("get: " + hit.collider.name);


							//weap = hit.collider.gameObject.GetComponent<weaponScript>();
							//weap = weaponScript.Instantiate(g.GetComponent<weaponScript>());


							//Debug.Log ("ammo: " + weap.ammo);
							box = hit.collider.gameObject;
							weap.GetWeapon(box);
							transform.Find ("Weapon").GetComponent<SpriteRenderer>().sprite = weap.sprite;
							this.box = box;
							box.SetActive(false);
							//GameObject.Destroy(hit.collider.gameObject);
							//Debug.Log ("verif: " + weap.ammo);
							//weap.isEquiped = true;

							//sprite.sprite = weap.sprite;
							//EquipWeapon(hit.collider.gameObject);
							//GameObject.Destroy(hit.collider.gameObject);
							break;
						}
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			BulletTime();
		}
	}

	void EnergyGeneration(float rate) {
		energy += rate;
		if (energy > 100f)
			energy = 100f;
	}

	void BasicMovement() {
		// WASD movements
		Vector2 move = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		move *= speed;
		ctrl.velocity = Vector2.ClampMagnitude (move, speed);
		// Mouse movement

		Vector3 lookAt = transform.position - Camera.main.ScreenToWorldPoint (Input.mousePosition);

		//Vector3 lookAt = transform.localPosition - Camera.main.ScreenToWorldPoint (Input.mousePosition);



		float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg - 90f;
		ctrl.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

		//direction = Vector3.Normalize (new Vector3(lookAt.x, lookAt.y, angle));
		direction = new Vector2 (-lookAt.x, -lookAt.y).normalized;


		//Debug.Log (direction.magnitude);
		// Feet's animation
		if (ctrl.velocity.magnitude == 0)
			feet.GetComponent<Animator> ().Rebind ();
		else {
			DashMovement();
		}
		feet.GetComponent<Animator> ().SetFloat ("speed", ctrl.velocity.magnitude / 3f);
	}

	void BulletTime() {
		Time.timeScale = 0.5f;
		maxPayne = false;
		StartCoroutine (ResetBulletTime ());
	}

	IEnumerator ResetBulletTime() {
		yield return new WaitForSeconds (2f);
		Time.timeScale = 1f;
	}

	void DashMovement() {
		if (Input.GetButtonDown ("Jump") && canDash && energy >= 50f) {
			energy -= 50f;
			canDash = false;
			speed *= 2;
			StartCoroutine(DashReset());
		}
	}

	IEnumerator DashReset() {
		yield return new WaitForSeconds (0.1f);
		speed /= 2;
		canDash = true;
	}
	/*
	bool CheckForDash() {

	}
	*/
	void Punch() {
		punch.TryToPunch ();
	}

	public void EquipWeapon() {

	}

	public void UnequipWeapon() {

	}
}
