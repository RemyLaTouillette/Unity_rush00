using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiManagerScript : MonoBehaviour {

	public GameObject	gameManager;

	private gameManagerScript	gms;
	private Image				img_weapon;

	// Use this for initialization
	void Start () {
		gms = gameManager.GetComponent<gameManagerScript>();
		img_weapon = GameObject.Find ("img_weapon").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ChangeWeaponImage(Image img) {
		img_weapon = img;
	}
}
