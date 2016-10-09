using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HuntingController : MonoBehaviour {
	List<Vector2> waypoints = new List<Vector2>();
	int nextStep = -1;
	Vector2 target = new Vector2(-1000f, 0f);
	EnemyController self;

	bool onTheBackWay = false;

	void Awake()
	{
		self = gameObject.GetComponent<EnemyController> ();
	}

	void Update() {
		if (self.currentStatus == EnemyController.Status.hunting || self.currentStatus == EnemyController.Status.back) {
			if (waypoints != null && waypoints.Count > 0 && nextStep >= 0 && nextStep < waypoints.Count)
			{
				self.isMoving();
				if((Vector2)transform.position != waypoints[nextStep])
					transform.position = Vector2.MoveTowards (transform.position, waypoints[nextStep], self.speed * 1.5f * Time.deltaTime);
				else
				{					
					nextStep++;
					if (nextStep < waypoints.Count)
						self.Lookat((Vector2)transform.position - (Vector2)waypoints [nextStep]);
				}
			}
			else
			{
				switch (self.currentStatus)
				{
				case EnemyController.Status.hunting:
					self.currentStatus = EnemyController.Status.search;
					break;
				case EnemyController.Status.back:
					self.currentStatus = EnemyController.Status.idle;
					break;
				}
				self.isStaying();
			}
		}
		if (self.currentStatus == EnemyController.Status.search) {
		}

		if ((self.currentStatus == EnemyController.Status.idle || self.currentStatus == EnemyController.Status.patrol) && nextStep != -1)
			clearHunting ();

		/*if (self.currentStatus == EnemyController.Status.back && onTheBackWay == false) {
			onTheBackWay = true;
			target.x = -1000.0f;
			nextStep = 0;
			waypoints.Reverse();
			//self.Lookat((Vector2)transform.position - (Vector2)waypoints [nextStep]);
		}*/
	}

	void clearHunting()
	{
		target.x = -1000.0f;
		nextStep = -1;
		clearWay ();
	}

	void clearWay()
	{
		waypoints.Clear ();
	}

	GameObject GetCloseWaypoint(Vector2 position)
	{
		GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
		GameObject closestObject = null;
		foreach(GameObject obj in waypoints)
		{
			if(!closestObject)
				closestObject = obj;
			else if(Vector2.Distance(position, obj.transform.position) <= Vector2.Distance(position, closestObject.transform.position))
				closestObject = obj;
		}
		return closestObject;
	}
	
	public void GoToTarget(Vector2 target)
	{
		if (this.target == target)
			return;
		clearHunting ();
		this.target = target;
		self.Lookat((Vector2)transform.position - target);
		Waypoint closeWayp = GetCloseWaypoint (transform.position).GetComponent<Waypoint>();

		List<Waypoint> previous = new List<Waypoint>();
		List<Vector2> path = closeWayp.getPath (GetCloseWaypoint(target).GetComponent<Waypoint>(), previous);
		if (path != null) {
			path.Reverse ();
			foreach (Vector2 way in path)
				waypoints.Add (way);
			nextStep = 0;			
		}
	}
	
}
