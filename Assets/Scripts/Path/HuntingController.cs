using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HuntingController : MonoBehaviour {
	List<Vector2> waypoints = new List<Vector2>();
	int nextStep = -1;
	Vector2 target = new Vector2(-1000f, 0f);
	EnemyController self;

	bool onTheBackWay = false;
	bool running = true;
	IEnumerator routine;

	void Awake()
	{
		self = gameObject.GetComponent<EnemyController> ();
	}

	IEnumerator LookatCoroutine()
	{
		for (int i=0; i<5; i++) {
			switch(i)
			{
				case 0:
					self.Lookat(self.transform.rotation * Vector3.left);
					break;
				case 1:
					self.Lookat(self.transform.rotation * Vector3.right);
					break;
				case 2:
					self.Lookat(self.transform.rotation * Vector3.down);
					break;
				case 3:
					self.Lookat(self.transform.rotation * Vector3.up);
					break;
				case 4:
					if (self.currentStatus == EnemyController.Status.back)
						GoToTarget(self.startPosition);
					break;
			}
			yield return new WaitForSeconds (1);
		}
	}

	void Update() {
	
		if (nextStep != -1 && (self.currentStatus == EnemyController.Status.hunting || self.currentStatus == EnemyController.Status.back)) {
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
					self.currentStatus = self.startStatus;
					break;
				}
				self.isStaying();
			}
		}
		else if (self.currentStatus == EnemyController.Status.search) {
			clearHunting ();
			StartCoroutine(LookatCoroutine());
			self.currentStatus = EnemyController.Status.back;
			

		}

		if ((self.currentStatus == EnemyController.Status.idle || self.currentStatus == EnemyController.Status.patrol) && nextStep != -1)
			clearHunting ();
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
