using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HuntingController : MonoBehaviour {
	List<Vector2> waypoints = new List<Vector2>();
	int nextStep = -1;
	
	EnemyController self;

	void Awake()
	{
		self = gameObject.GetComponent<EnemyController> ();
	}

	void Update() {
		if (self.currentStatus == EnemyController.Status.hunting) {

			if (waypoints != null && waypoints.Count > 0 && nextStep >= 0 && nextStep < waypoints.Count)
			{
				if((Vector2)transform.position != waypoints[nextStep])
					transform.position = Vector2.MoveTowards (transform.position, waypoints[nextStep], self.speed * 1.5f * Time.deltaTime);
				else
				{					
					nextStep++;
					if (nextStep < waypoints.Count);
						self.Lookat((Vector2)transform.position - (Vector2)waypoints [nextStep]);
				}
			}
		}
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
		waypoints.Clear ();
		nextStep = -1;
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
