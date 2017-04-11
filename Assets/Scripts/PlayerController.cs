using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour {

	public float acc;
	public float maxSpeed;
	public GameObject shot;
	public float fireInterval = 0.2f;
	public GameObject grenade;
	public float fallVelocity = -15f;

	private Rigidbody rb;

	Vector3 movement;
	bool isJumpable;
	float sqrMaxSpeed;
	float timer;
	float maxRayLength = 100f;
	int sceneMask;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		isJumpable = false;
		sqrMaxSpeed = maxSpeed * maxSpeed;
		sceneMask = LayerMask.GetMask ("Scene");
	}
	
	// Update is called once per frame
	void Update () {
		//checkGrounded ();

		timer += Time.deltaTime;
		if (Input.GetButton("Fire1") && timer > fireInterval) {
			if (GameManager.instance.bullet > 0) {
				GameManager.instance.bullet -= 1;
				Shoot ();
				timer = 0f;
			}
		}
		else if (Input.GetButton("Fire2") && timer > fireInterval) {
			if (GameManager.instance.bomb > 0) {
				GameManager.instance.bomb -= 1;
				ThrowBomb();
				timer = 0f;
			}
		}

		//Debug.Log (transform.TransformPoint(new Vector3 (0, 0, 0)));


	}

	void FixedUpdate(){
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		Move (h, v);
		Jump ();
		/*
		if (isGrounded) {
			Vector3 friction = 3 * Vector3.Normalize (new Vector3 (-rb.velocity.x, 0, -rb.velocity.z));
			rb.AddForce (friction);
		}*/

		//Debug.Log (rb.velocity.y);

		if (rb.velocity.y <= fallVelocity) {
			GameManager.instance.life -= 1;
			if (GameManager.instance.life <= 0) {
				GameManager.instance.isEnd = true;
			} else {
				GameManager.instance.health = GameManager.instance.maxHealth;
			}
			GameManager.instance.Respawn ();
		}
	}

	void Move(float h, float v){

		movement = Vector3.Normalize(new Vector3 (h, 0, v)) * acc;

		Vector3 xzVelo = rb.velocity;
		xzVelo.y = 0f;

		if (xzVelo.sqrMagnitude >= sqrMaxSpeed) {
			Vector3 resistForce = Vector3.Normalize (new Vector3 (-rb.velocity.x, 0, -rb.velocity.z)) * maxSpeed;
			rb.AddForce (resistForce);
			//rb.velocity = rb.velocity.normalized * maxSpeed;
		} else
			rb.AddForce (movement);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Checkpoint")
		{
			Debug.Log("Checkpoint: " + other.gameObject.name);
			GameManager.instance.checkPoint = other.gameObject.transform.position;
		}
		else if (other.tag == "Key")
		{
			Transform parent = other.gameObject.transform.parent;
			parent.gameObject.GetComponent<DoorController>().keys -= 1;
			Debug.Log("Key left: " + parent.gameObject.GetComponent<DoorController>().keys);
			Destroy(other.gameObject);
		}
		else if (other.tag == "Item")
		{
			ItemController item = other.gameObject.GetComponent<ItemController>();
			if (item.type == "Bomb")
			{
				int maxItem = GameManager.instance.maxBomb;
				if (GameManager.instance.bomb != maxItem)
				{
					if (GameManager.instance.bomb + item.quantity > maxItem)
					{
						GameManager.instance.bomb = maxItem;
					}
					else
						GameManager.instance.bomb += item.quantity;
					Destroy(other.gameObject);
				}
			}
			else
			{
					GameManager.instance.bullet += item.quantity;
					Destroy(other.gameObject);

			}

		}
		else if (other.tag == "NextStage")
		{
			SceneManager.LoadScene("Scene2");
		}
		else if (other.tag == "Finish")
		{
			SceneManager.LoadScene ("End Game");
		}
	}

	void OnCollisionEnter(Collision obj){
		if (obj.transform.tag == "Floor") {
			isJumpable = true;
			//Debug.Log ("touch");
		}
	}

	void Jump(){
		if (Input.GetButton ("Jump") && isJumpable) {
			isJumpable = false;
			rb.AddForce (Vector3.up * 700);
		}
	}

	float get_angle(){
		Vector3 mousePos = Input.mousePosition;
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;
		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		return angle;
	}

	void Shoot(){
		Instantiate(shot,transform.position,Quaternion.Euler(new Vector3(0, -get_angle() + 90, 0)));
	}

	void ThrowBomb(){
		Instantiate(grenade,transform.position + new Vector3(0,0.89f,0),Quaternion.Euler(new Vector3(0, -get_angle() + 90, 0)));
	}
}
