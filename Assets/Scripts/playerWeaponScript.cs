using UnityEngine;
using System.Collections;

public class playerWeaponScript : MonoBehaviour {

	public Sprite		sprite;
	public GameObject	bullet;
	public int			ammo;
	public int			ammoMax;
	public float		fireRate;
	public float		power;

	public bool			isEquiped {get; private set;}
	private bool		canShoot;
	private GameObject	spawn;

	// Use this for initialization
	void Start () {
		isEquiped = false;
		canShoot = true;
		bullet = transform.Find ("Bullet").gameObject;
		spawn = transform.Find ("Spawn").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shoot(Vector2 dir, Quaternion ori) {
		//Debug.Log ("Shoot !");
		if (canShoot) {
			Vector3 pos = spawn.transform.position;
			GameObject b = (GameObject)Instantiate(bullet, pos,  ori);
			b.SetActive(true);

			b.GetComponent<Rigidbody2D>().AddForce(dir * power);
			canShoot = false;
			StartCoroutine(NextShoot());
		}
	}

	IEnumerator NextShoot() {
		yield return new WaitForSeconds (fireRate);
		canShoot = true;
	}
	

	public void GetWeapon(GameObject box) {
		WeaponBoxScript s = box.GetComponent<WeaponBoxScript> ();
		ammo = s.ammo;
		ammoMax = s.ammoMax;
		fireRate = s.fireRate;
		power = s.power;
		sprite = s.sprite;
		bullet = GameObject.Instantiate (s.bullet);
		bullet.SetActive (false);
		isEquiped = true;
	}

	public void DropWeapon() {
		isEquiped = false;
	}
}
