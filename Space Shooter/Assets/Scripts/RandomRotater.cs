using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotater : MonoBehaviour {

	public float tumble;
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		//Gives a random Vector3
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
