using UnityEngine;
using System.Collections;

public class objectScript : MonoBehaviour {

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		//if (rb2d.velocity.magnitude > 0)
		rb2d.velocity = rb2d.velocity * 0.95f;
	}
}
