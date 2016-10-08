using UnityEngine;
using System.Collections;

public class playerControllerTest : MonoBehaviour {

	// GameObject
	private CharacterController	ctrl;

	// Variables
	private Vector3	move;
	private Vector3 lookAt;
	private float	speed;


	// Use this for initialization
	void Start () {
		// Init GameObject
		ctrl = GetComponent<CharacterController> ();

		// Init variables
		speed = 6f;
	}
	
	// Update is called once per frame
	void Update () {

		// Basic movement
		move = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0f);
		move *= speed;
		ctrl.Move (move * Time.deltaTime);

		// Look at mouse position
		lookAt = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector3 dir = transform.position - lookAt;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
		ctrl.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

		// Set feet's animation based on character speed
		GetComponentInChildren<Animator> ().SetFloat ("speed", Vector3.Magnitude(move) / 3f);
	}
}
