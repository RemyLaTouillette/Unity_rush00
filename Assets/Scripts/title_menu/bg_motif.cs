using UnityEngine;
using System.Collections;

public class bg_motif : MonoBehaviour {

	public Vector4	tmp_color;
	private int		max_time = 1;	
	private int		timer;
	private int		i = 1;

	// Use this for initialization
	void Start () {
		timer = Mathf.RoundToInt(Time.timeSinceLevelLoad);
	}

	// Update is called once per frame
	void Update () {
		if (Mathf.RoundToInt(Time.timeSinceLevelLoad) - timer >= max_time)
		{
			timer = Mathf.RoundToInt(Time.timeSinceLevelLoad);
			//tmp_color = this.gameObject.GetComponent<SpriteRenderer>().color;
			if (i == 1)
				this.gameObject.GetComponent<Camera>().backgroundColor = Color.blue;
			if (i == 2)
				this.gameObject.GetComponent<Camera>().backgroundColor = Color.yellow;
			if (i == 3)
				this.gameObject.GetComponent<Camera>().backgroundColor = Color.green;
			if (i == 4)
				this.gameObject.GetComponent<Camera>().backgroundColor= Color.cyan;
			i = (i > 4) ? 1 : i + 1;
		}
	}
}