using UnityEngine;
using System.Collections;

public class noWeaponScript : MonoBehaviour {

	private float last_punch;
	private Vector3	target;

	// Use this for initialization
	void Start () {
		last_punch = Time.time;	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = Vector3.MoveTowards (transform.position, this.target, 3f);
	}

	public void Punch(Vector3 dir) {
		target = dir;
	}
}
