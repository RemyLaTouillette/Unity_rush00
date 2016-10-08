using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Title : MonoBehaviour {

	private Vector2		dist;
	private Vector2		outline_dist;
	private Vector2		dist_start;
	private	Quaternion	rot;
	private	Vector3		tmp_scale;
	public	int			timer;
	public	int			waiting_time = 1;
	public	int			dir_1 = 1;
	public	int			dir_2 = 1;
	public bool			isSelected = false;

	// Use this for initialization
	void Start () {
		dist = this.gameObject.GetComponent<Shadow> ().effectDistance;
		outline_dist = this.gameObject.GetComponent<Outline> ().effectDistance;
		dist_start = dist;
		timer = Mathf.RoundToInt (Time.timeSinceLevelLoad);
	}
	
	// Update is called once per frame
	void Update ()
	{
		dist = this.gameObject.GetComponent<Shadow> ().effectDistance;
		outline_dist = this.gameObject.GetComponent<Outline> ().effectDistance;
		rot = this.transform.rotation;
		tmp_scale = this.transform.localScale;

		if (dist.x >= 2)
			dir_1 = -1;
		if (dist.x <= 0)
			dir_1 = 1;

		if (outline_dist.x >= 3)
			dir_2 = -1;
		if (outline_dist.x <= 0)
			dir_2 = 1;

		outline_dist.x += 1 * Time.deltaTime * dir_2;
		outline_dist.y += 1 * Time.deltaTime * dir_2;

		tmp_scale.x += 0.1f * Time.deltaTime * dir_1;
		tmp_scale.y += 0.1f * Time.deltaTime * dir_1;

		rot.z += 0.03f * Time.deltaTime * dir_1;

		dist.x += 1 * Time.deltaTime * dir_1;
		dist.y += 1 * Time.deltaTime * dir_1;

		this.gameObject.GetComponent<Shadow> ().effectDistance = dist;
		this.gameObject.GetComponent<Outline> ().effectDistance = outline_dist;
		this.transform.localScale = tmp_scale;
		this.transform.rotation = rot;
	}
}
