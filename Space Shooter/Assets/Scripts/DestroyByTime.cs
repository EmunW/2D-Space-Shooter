using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	public float lifetime; //Time until the gameobject is destroyed
	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
	}
}
