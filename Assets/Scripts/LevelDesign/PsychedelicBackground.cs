using UnityEngine;
using System.Collections;

public class PsychedelicBackground : MonoBehaviour {
	public float				freq = 0.025F;
	public float				time = 0.001F;

	private float[]				colorTab = {0, 0, 0};
	private SpriteRenderer		backgroundColor;
	private float				alphaVector;
	private Color				tmpColor;

	// Use this for initialization
	void Start () {
		backgroundColor = gameObject.GetComponent<SpriteRenderer>();
		colorTab[0] = backgroundColor.color.r;
		colorTab[1] = backgroundColor.color.b;
		colorTab[2] = backgroundColor.color.g;
		tmpColor.a = 1F;
		StartCoroutine(Loop ());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator Loop () {
		while (true)
		{
			for(int i = 0; i < 3; i++)
			{
				if (colorTab[i] >= 0.6F)
					colorTab[i] = 0.5F;
				else if (colorTab[i] <= 0.05F)
					colorTab[i] = 0.1F;
				else
					colorTab[i] = colorTab[i] +  Random.Range(-freq, freq);
			}
			tmpColor.r = colorTab[0];
			tmpColor.b = colorTab[1];
			tmpColor.g = colorTab[2];
		
			backgroundColor.color = tmpColor;
			yield return new WaitForSeconds(time);
		}
	}
}
