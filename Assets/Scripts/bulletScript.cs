using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

	public Vector3 	dir;
	public float	range;

	private float	t = 0f;

	// Use this for initialization
	void Start () {
		t = Time.time + range;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > t) {
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D() {
		GameObject.Destroy (this.gameObject);
	}

	void OnTriggerEnter2D() {
		GameObject.Destroy (this.gameObject);
	}

}
