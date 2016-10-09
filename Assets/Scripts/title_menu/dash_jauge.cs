using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class dash_jauge : MonoBehaviour {

	public float	jaugeMax = 255;
	public float	jaugeVal = 10;
	public Image	jaugeFg = null;
	public Vector3	scaleTmp = Vector3.zero;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scaleTmp = this.jaugeFg.transform.localScale;
		if (jaugeVal < jaugeMax && jaugeVal >= 0f)
			scaleTmp.x = jaugeVal * 1f / jaugeMax;
		else if (jaugeVal >= jaugeMax)
			scaleTmp.x = 1;
		else if (jaugeVal < 0)
			scaleTmp.x = 0f;
		this.jaugeFg.transform.localScale = scaleTmp;
	}
}
