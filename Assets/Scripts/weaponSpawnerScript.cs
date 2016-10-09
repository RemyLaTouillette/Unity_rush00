using UnityEngine;
using System.Collections;

public class weaponSpawnerScript : MonoBehaviour {


	public GameObject[] weapons;
	public GameObject weapon;

	// Use this for initialization
	void Start () {
	weapon = weapons[Random.Range(0,weapons.Length - 1)].gameObject;
		GameObject.Instantiate (weapon, transform.localPosition, Quaternion.identity);
		//Weapon.transform.position = transform.localPosition;
		GameObject.Destroy (this.gameObject);

	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
