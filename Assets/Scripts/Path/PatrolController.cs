using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolController : MonoBehaviour {

	public List<Transform> waypoints = new List<Transform>();
	public bool invertedWay = false;

	bool backWay 	= false;
	int nextStep	= 0;

	EnemyController self;

	
	void Awake()
	{
		self = gameObject.GetComponent<EnemyController> ();
		if (waypoints.Count > 0) {
			self.currentStatus = EnemyController.Status.patrol;
			transform.position = waypoints [0].position;
			nextStep = 1;
		}
	}
	
	void Update() {

		if (self.currentStatus == EnemyController.Status.patrol) {
			
			if(waypoints [nextStep].position != transform.position)
			{
				transform.position = Vector2.MoveTowards (transform.position, waypoints [nextStep].position, self.speed * Time.deltaTime);
				
				if (transform.position == waypoints [nextStep].position) {
					GetNextStep();
					self.Lookat((Vector2)transform.position - (Vector2)waypoints [nextStep].position);
				}
			}
			self.isMoving();
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
}
