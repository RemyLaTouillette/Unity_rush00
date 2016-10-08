using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolController : MonoBehaviour {

	public List<Transform> waypoints = new List<Transform>();
	public float speed	= 1f;
	public bool invertedWay = false;

	bool backWay = false;
	Vector2 target 		= new Vector3(-10f,-10f); 
	int currentStep		= 0;
	int nextStep		= 0;

	void Awake()
	{
		if (waypoints.Count > 0) {
			transform.position = waypoints [0].position;;
			nextStep = 1;
		}
	}

	void Update() {

		float step = speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards (transform.position, waypoints[nextStep].position, step);

		if (transform.position == waypoints[nextStep].position) {
			if (!invertedWay)
				nextStep = (nextStep == waypoints.Count -1) ? 0 : nextStep + 1;
			else if (nextStep > 0 && (nextStep == waypoints.Count -1 || backWay))
			{
				nextStep = nextStep - 1;
				backWay = true;

			}
			else 
			{
				nextStep = nextStep + 1;
				backWay = false;
			}

			if (nextStep < 0)
				nextStep = 0;
			else if (nextStep > waypoints.Count - 1)
				nextStep = waypoints.Count - 1;
		}

		transform.rotation = Quaternion.LookRotation (Vector3.forward, (Vector2)transform.position - (Vector2)waypoints[nextStep].position);
	}
}
