using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController gameController;
	public int scoreValue;

	void Start(){
		//Finds the first object in the scene that is tabbed with "GameController"
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if(gameController == null){
			Debug.Log("Cannot find 'GameController' script");
		}
	}
	void OnTriggerEnter(Collider hi){
		Debug.Log(hi.name);
		if(!hi.CompareTag("Boundary")){

			if(hi.CompareTag("Player")){
				Instantiate(playerExplosion, hi.transform.position, hi.transform.rotation);
				gameController.GameOver();
			}
			if((hi.tag != "Enemy" && hi.tag != "PowerUp")
				&& hi.tag != "Enemy"){	//So asteroids don't collide
				if(explosion != null){
					Instantiate(explosion, transform.position, transform.rotation);
				}
				Destroy(hi.gameObject);
				Destroy(gameObject);	//Destroys the asteroid
				gameController.AddScore(scoreValue);
			}
		}
	}
}
