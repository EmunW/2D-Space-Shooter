using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}
public class PlayerCont : MonoBehaviour {

	private Rigidbody rb;
	public Boundary boundary;
	public float speed;
	public float tilt;
	public GameObject bolt;
	public Transform boltSpawn;
	private AudioSource ok;
	public float fireRate;
	private float nextFire;
	private bool powerUp;
	public float powerBullets;		//Amount of bullets fast fired.
	public float fastFireRate;
	private float totalPowerBullets;
	// Use this for initialization
	void Start () {
		ok = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody>();
	}

	void OnTriggerEnter(Collider powah){
		if(powah.CompareTag("PowerUp")){
			powerUp = true;
			totalPowerBullets = powerBullets;
			Destroy(powah.gameObject);
		}
	}
	void Update(){
		if(powerUp == true){
			if((Input.GetButton("Fire1") && Time.time > nextFire) && totalPowerBullets > 0){
				nextFire = Time.time + fastFireRate;
				Instantiate(bolt, boltSpawn.position, boltSpawn.rotation);
				ok.Play();
				totalPowerBullets--;
			}
			else if(totalPowerBullets <= 0.0f){
				powerUp = false;
			}
		}
		else if(Input.GetButton("Fire1") && Time.time > nextFire){
			nextFire = Time.time + fireRate;
			Instantiate(bolt, boltSpawn.position, boltSpawn.rotation);
			ok.Play();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement*speed;
		rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
		0.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
