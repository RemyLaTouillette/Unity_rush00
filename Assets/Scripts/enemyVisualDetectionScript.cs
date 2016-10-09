using UnityEngine;
using System.Collections;

public class enemyVisualDetectionScript : MonoBehaviour {

	private LineRenderer	line;
	private AudioSource		sound;
	private GameObject		p;

	private bool detected;
	private bool inDetector;

	// Use this for initialization
	void Start () {
		p = null;
		detected = false;
		line = GetComponent<LineRenderer> ();
		sound = GetComponent<AudioSource> ();
		line.SetWidth (0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		line.SetPosition(0, transform.localPosition);
		if (canDetect ()) {
			transform.Find ("Indicator").GetComponent<SpriteRenderer> ().enabled = true;
			if (!detected) {
				sound.Play();
				detected = true;
			}
			Debug.Log (name + ": I see you");
		} else {
			detected = false;
			transform.Find ("Indicator").GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.name == "Player") {
			inDetector = true;
			p = coll.gameObject;
			line.SetWidth(0.2f, 0.5f);
			line.SetPosition(1, p.transform.position);
		}
	}
	
	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.name == "Player") {
			inDetector = false;
			line.SetWidth(0f, 0f);
			//transform.Find ("Indicator").GetComponent<SpriteRenderer>().enabled = false;
			p = null;
		}
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.name == "Player") {
			inDetector = true;
			p = coll.gameObject;
			line.SetPosition(1, p.transform.position);
		}
	}

	IEnumerator TestDetect() {
		yield return new WaitForSeconds (3f);
		detected = true;
	}

	bool canDetect() {
		if (p) {
			RaycastHit2D[] hits = Physics2D.LinecastAll(transform.localPosition, p.transform.localPosition);
			if (hits.Length > 1) {
				if (hits[1].collider.gameObject.name == "Player") {
					return true;
				}
			}
		}
		return false;
	}
}
