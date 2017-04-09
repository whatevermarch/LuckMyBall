using UnityEngine;
using System.Collections;

public class MovingPlaneController : MonoBehaviour {

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
	/*
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			Vector3 scale = other.transform.localScale;
			dummyForScale = new GameObject ();
			dummyForScale.transform.SetParent (transform);
			other.transform.SetParent (dummyForScale.transform);
			other.transform.localScale = scale;
		}

	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			Vector3 scale = other.transform.localScale;
			other.transform.SetParent (null);
			dummyForScale.transform.SetParent (null);
			Destroy (dummyForScale);
			other.transform.localScale = scale;
		}
		
	}
*/
}
