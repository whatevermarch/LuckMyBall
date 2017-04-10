using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public GameObject shot;
	public float fireInterval = 0.5f;

	float destroyTimer = 2.5f;
	bool setDestruct = false;
	int shotAngle;
	float timer;

	// Use this for initialization
	void Start () {
		shotAngle = 0;
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (setDestruct) {
			destroyTimer -= Time.deltaTime;
			if (destroyTimer < 0)
				Destroy (this.gameObject);
		} else if (timer > fireInterval){
			shotAngle = (shotAngle + 10) % 360;
			Vector3 shotVector = new Vector3 (Mathf.Cos ((float)shotAngle*Mathf.PI/180), 0, Mathf.Sin ((float)shotAngle*Mathf.PI/180));
			BlastController blast = Instantiate (shot, transform.position + shotVector, Quaternion.identity).GetComponent<BlastController> () as BlastController;
			blast.setDir (shotVector);
			timer = 0f;
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			GameManager.instance.life -= 1;
			if (GameManager.instance.life <= 0) {
				GameManager.instance.isEnd = true;
			} else {
				GameManager.instance.health = GameManager.instance.maxHealth;
			}
			GameManager.instance.Respawn ();
		}

	}

	public void startDestroy(){
		setDestruct = true;
	}

}
