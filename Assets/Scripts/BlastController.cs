using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastController : MonoBehaviour {

	public float speed;
	public float shotYield;
	private Rigidbody rb;

	float timer;
	float lifeTime;
	Vector3 direction = Vector3.zero;
	bool isDirAssigned = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		lifeTime = shotYield / speed;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > lifeTime)
			Destroy (this.gameObject);

		if (direction != Vector3.zero && !isDirAssigned) {
			rb.velocity = direction * speed;
			isDirAssigned = true;
		}

	}

	void OnTriggerEnter(Collider col){
		if (col.tag != "Enemy") {
			if (col.tag == "Player")
				Destroy (col.gameObject);
			Destroy (this.gameObject);
		}
	}

	public void setDir(Vector3 dir){
		if (dir == Vector3.zero)
			dir = Vector3.forward;
		direction = dir;
	}
}
