using UnityEngine;
using System.Collections;

public class bg_animation : MonoBehaviour {

	public Vector3	bg_scale;
	public Vector3	start_scale;
	public Vector3	tmp_pos;
	public Vector4	tmp_color;
	public Camera	cam_pos;
	public float	bg_speed;

	// Use this for initialization
	void Start () {
		start_scale = transform.localScale;
		tmp_color = this.GetComponent<SpriteRenderer>().color;
		tmp_color.w = 0.4f;
		this.GetComponent<SpriteRenderer>().color = tmp_color;
	}
	
	// Update is called once per frame
	void Update () {

		tmp_pos.x = Screen.width;
		tmp_pos.y = Screen.height;
		tmp_pos.z = 0f;
		tmp_pos = cam_pos.ScreenToWorldPoint (tmp_pos);

		if (transform.localScale.x >= tmp_pos.x / 3f) {
			transform.localScale = start_scale;
		}
		else
		{
			bg_scale = transform.localScale;
			bg_scale.x += bg_speed * Time.deltaTime;
			bg_scale.y += bg_speed * Time.deltaTime;
			transform.localScale = bg_scale;
		}
	}
}
