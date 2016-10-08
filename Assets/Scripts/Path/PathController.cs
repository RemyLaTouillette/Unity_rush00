using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathController : MonoBehaviour {

	public List<Transform> waypoints = new List<Transform>();
	public float speed	= 1f;
	public bool invertedWay = false;

	enum Status {idle, patrol, sad};
	Status currentStatus;
	bool backWay = false;
	Vector2 target 		= new Vector3(-10f,-10f); 
	int currentStep		= 0;
	int nextStep		= 0;

	void Awake()
	{
		if (waypoints.Count > 0) {
			transform.position = waypoints [0].position;;
			nextStep = 1;
			currentStatus = Status.patrol;
		}
		else
			currentStatus = Status.idle;
	}

	void Update() {

		if (currentStatus == Status.patrol) {

			if(waypoints [nextStep].position != transform.position)
			{
				float step = speed * Time.deltaTime;
				transform.position = Vector2.MoveTowards (transform.position, waypoints [nextStep].position, step);

				if (transform.position == waypoints [nextStep].position) {
					GetNextStep();
					Lookat((Vector2)transform.position - (Vector2)waypoints [nextStep].position);
				}
			}
		}
	}

	void GetNextStep()
	{
		if (!invertedWay)
			nextStep = (nextStep == waypoints.Count - 1) ? 0 : nextStep + 1;
		else if (nextStep > 0 && (nextStep == waypoints.Count - 1 || backWay)) {
			nextStep = nextStep - 1;
			backWay = true;
			
		} else {
			nextStep = nextStep + 1;
			backWay = false;
		}
		
		if (nextStep < 0)
			nextStep = 0;
		else if (nextStep > waypoints.Count - 1)
			nextStep = waypoints.Count - 1;
	}

	void Lookat(Vector2 at)
	{
		transform.rotation = Quaternion.LookRotation (Vector3.forward, at);
	}

	GameObject GetCloseWaypoint()
	{
		GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
		GameObject closestObject = null;
		foreach(GameObject obj in waypoints)
		{
			if(!closestObject)
				closestObject = obj;
			else if(Vector3.Distance(transform.position, obj.transform.position) <= Vector3.Distance(transform.position, closestObject.transform.position))
				closestObject = obj;
		}
		return closestObject;
	}
	
	public void GoToTarget()
	{
		currentStatus = Status.sad;
		Destroy(GetCloseWaypoint ());
	}
}
