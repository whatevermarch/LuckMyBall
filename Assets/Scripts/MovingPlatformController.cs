using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour {

	public float speed;
	public float TimeToRev;

	GameObject dummyForScale;
	bool isReverse;
	float driveTime;
	//Vector3 initDir;

	// Use this for initialization
	void Start () {
		isReverse = false;
		driveTime = 0f;
	}

	// Update is called once per frame
	void Update () {
		if (driveTime > TimeToRev) {
			if (isReverse)
				isReverse = false;
			else
				isReverse = true;

			driveTime = 0;
		}

		if(isReverse)
			transform.Translate (-Vector3.forward * speed * Time.deltaTime);
		else
			transform.Translate (Vector3.forward * speed * Time.deltaTime);

		driveTime += Time.deltaTime;
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			other.transform.SetParent (transform);
		}

	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			other.transform.SetParent (null);
		}

	}
}
