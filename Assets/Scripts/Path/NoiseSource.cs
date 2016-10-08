using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoiseSource : MonoBehaviour {

	public float overlapRadius = 1.1f;

	//List<Transform> links = new List<Transform>();
		
	void Update()
	{
		if (Input.GetKey (KeyCode.A)) {
			getEnemies();
		}
	}

	void getEnemies()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, overlapRadius);
		int i = 0;
		while (i < hits.Length) {
			if (hits[i].gameObject.tag == "Enemy")
				hits[i].GetComponent<PathController>().GoToTarget();
			i++;
		}	
	}
}
