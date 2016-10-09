using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoiseSource : MonoBehaviour {

	public KeyCode key;
	void Update()
	{
		if (Input.GetKey (key)) {
			getEnemies();
		}
	}

	void getEnemies()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
		int i = 0;
		while (i < hits.Length) {
			if (hits[i].gameObject.tag == "Enemy")
				hits[i].GetComponent<EnemyController>().Alerted(transform.position);
			i++;
		}	
	}
}
