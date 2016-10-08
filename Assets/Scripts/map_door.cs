using UnityEngine;
using System.Collections;

public class map_door : MonoBehaviour {

	public float smooth = 1.0f;
	public Vector2 offset;
	public string[] who;

	Vector2 def;	
	bool open;
	bool enter;	

	void Start()
	{
		def = new Vector2(transform.position.x, transform.position.y);
	}

	void Update ()
	{
		if (open)
			transform.position = Vector2.MoveTowards (transform.position, def - offset, smooth);
		else
			transform.position = Vector2.MoveTowards (transform.position, def, smooth);
	}

	void OnTriggerEnter2D (Collider2D coll){
		//if (other.gameObject.tag == "Player") {
			open = true;
		//}
	}

	void OnTriggerExit2D (Collider2D coll){
		//if (other.gameObject.tag == "Player") {
			open = false;
		//}
	}
}
