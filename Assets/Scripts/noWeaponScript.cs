using UnityEngine;
using System.Collections;

public class noWeaponScript : MonoBehaviour {

	private float			delay;
	private bool			isArmed;
	private Vector3			target;
	private SpriteRenderer	sprite;
	private AudioSource		audio;
	private	bool			isRight;
	private BoxCollider2D	hitbox;

	// Use this for initialization
	void Start () {
		isArmed = true;
		delay = 0.5f;
		sprite = GetComponent<SpriteRenderer> ();
		audio = GetComponent<AudioSource> ();
		hitbox = GetComponent<BoxCollider2D> ();
		sprite.enabled = false;
		hitbox.enabled = false;
	}
	
	// Update is called once per frame
void Update () {
		//transform.position = Vector3.MoveTowards (transform.position, this.target, 3f);
	}

	public void TryToPunch() {
		if (isArmed) {
			//Debug.Log("Punch !");
			audio.Play();
			sprite.enabled = true;
			hitbox.enabled = true;
			isArmed = false;
			SwitchSide();
			StartCoroutine(RearmPunch(delay));
		}
	}

	void SwitchSide() {
		float x = -transform.localScale.x;
		transform.localScale = new Vector3(x, 1f, 1f);
		x = transform.localPosition.x * -1f;
		transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
	}

	IEnumerator RearmPunch(float delay) {
		yield return new WaitForSeconds (delay);
		sprite.enabled = false;
		hitbox.enabled = false;
		isArmed = true;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log ("Take That ! " + coll.name);
	}
}