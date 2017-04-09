using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private Vector3 playerLocation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		playerLocation = GameObject.Find ("Player").transform.position;

		transform.position = playerLocation + new Vector3 (0.0f, 9.0f, -5.0f);
		transform.LookAt (playerLocation);
	}
}
