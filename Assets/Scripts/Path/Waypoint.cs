using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

	public float overlapRadius = 1.1f;
	public List<Transform> links = new List<Transform>();

	void Awake()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, overlapRadius);
		int i = 0;
		while (i < hits.Length) {
			if (hits[i].gameObject.tag == "Waypoint" && hits[i].transform.position != transform.position)
				links.Add(hits[i].transform);
			i++;
		}	
	}
}
