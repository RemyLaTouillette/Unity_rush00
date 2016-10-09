using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class overlay_update : MonoBehaviour {

	public Text			gunType;
	public Text			ammoCount;
	//public player scripts.

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () {
		//if (textHasChanged)
		this.gunType.text = "Katana";
		this.ammoCount.text = "35" + "/" + "50";
	}
}
