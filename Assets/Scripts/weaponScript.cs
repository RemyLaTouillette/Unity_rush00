using UnityEngine;
using System.Collections;

public class weaponScript : MonoBehaviour {

//	public string		weapName;
	public Sprite		sprite;
	public GameObject	projectile;
	
	public bool			melee;
	public int			ammoMax;
	public int			ammo;
	public float		fire_rate;
	public float		power;

	private float		last_shoot = 0f;

	public bool		isEquiped;

	// Use this for initialization
	void Start () {
		isEquiped = false;
		last_shoot = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TryToShoot(Vector3 pos, Vector3 dir, Quaternion ori) {
		if (Time.time > last_shoot && ammo > 0) {
			ammo--;
			GameObject b = (GameObject)Instantiate(projectile, pos,  ori);
			b.GetComponent<Rigidbody2D>().AddForce(dir * power);
			last_shoot = Time.time + fire_rate;
		}
	}
}
