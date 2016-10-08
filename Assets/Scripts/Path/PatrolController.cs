using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolController : MonoBehaviour {

	public List<Transform> waypoints = new List<Transform>();
	public float speed	= 1f;

	Vector2 target 		= new Vector3(-10f,-10f); 
	int currentStep		= 0;
	int nextStep		= 0;
	Rigidbody2D body;

	void Awake()
	{
		if (waypoints.Count > 0) {
			transform.position = waypoints [0].position;;
			nextStep = 1;
		}
	}

	void Start()
	{
		//body = GetComponent<Rigidbody2D> ();
	}
	
	void Update() {

		float step = speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards (transform.position, waypoints[nextStep].position, step);

		if (transform.position == waypoints[nextStep].position) {
			nextStep = (nextStep == waypoints.Count -1) ? 0 : nextStep + 1;
		}

		transform.rotation = Quaternion.LookRotation (Vector3.forward, (Vector2)transform.position - (Vector2)waypoints[nextStep].position);
	}
}
