using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

	public List<Transform> links = new List<Transform>();
	float overlapRadius;	

	void Awake()
	{
		overlapRadius = GetComponent<CircleCollider2D> ().radius;
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, overlapRadius);
		int i = 0;
		while (i < hits.Length) {
			if (hits[i].gameObject.tag == "Waypoint" && hits[i].transform.position != transform.position)
				links.Add(hits[i].transform);
			i++;
		}	
	}

	public List<Vector2> getPath (Waypoint target, List<Waypoint> previous) {
		if (previous.Contains (this) || (previous.Count > 50)) {
			return null;
		}

		previous = new List<Waypoint> (previous);
		previous.Add (this);
		List<Vector2> path = null;
		if (this == target) {
			path = new List<Vector2> ();
			path.Add (this.transform.position);
			return path;
		}

		List<Vector2>[] paths = new List<Vector2>[links.Count];
		int i = 0;
		foreach (Transform link in links) {
			paths[i] = link.gameObject.GetComponent<Waypoint>().getPath (target, previous);
			i++;
		}
		path = null;
		int pathCount = int.MaxValue;
		foreach (List<Vector2> p in paths) {
			if (p != null && p.Count < pathCount) {
				path = p;
				pathCount = p.Count;
			}
		}
		if (path != null)
			path.Add (this.transform.position);

		return path;
	}

}
