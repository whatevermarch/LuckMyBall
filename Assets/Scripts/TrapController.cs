using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public int speed;
    public int distance;
    public string direction;

    private float check_direction = 0;
	private bool isReverse;

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
		}
	}

	public void startDestroy(){
		setDestruct = true;
	}

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
			//Destroy(other.gameObject);
           GameManager.instance.life -= 1;
           GameManager.instance.Respawn(); 
           if(GameManager.instance.life <= 0){
                GameManager.instance.isEnd = true;
           }
        }

    }

    void FixedUpdate()
    {
        if (direction == "Down")
        {
            if (check_direction <= distance && !isReverse)
            {
                transform.position += Vector3.down * Time.deltaTime * speed;
                check_direction += Time.deltaTime * speed;
            }
            else
            {
				isReverse = true;
                transform.position += Vector3.up * Time.deltaTime * speed;
                check_direction -= Time.deltaTime * speed;
				if(check_direction <= 0)
					isReverse = false;
            }
        }
        else if (direction == "Up")
        {
            if (check_direction <= distance && !isReverse)
            {
                transform.position += Vector3.up * Time.deltaTime * speed;
                check_direction += Time.deltaTime * speed;
            }
            else
            {
				isReverse = true;
                transform.position += Vector3.down * Time.deltaTime * speed;
                check_direction -= Time.deltaTime * speed;
				if(check_direction <= 0)
					isReverse = false;
            }
        }
        else if (direction == "Right")
        {
            if (check_direction <= distance && !isReverse)
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
                check_direction += Time.deltaTime * speed;
            }
            else
            {
				isReverse = true;
                transform.position += Vector3.left * Time.deltaTime * speed;
                check_direction -= Time.deltaTime * speed;
				if(check_direction <= 0)
					isReverse = false;
            }
        }
        else if (direction == "Left")
        {
            if (check_direction <= distance && !isReverse)
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
                check_direction += Time.deltaTime * speed;
            }
            else
            {
				isReverse = true;
                transform.position += Vector3.right * Time.deltaTime * speed;
                check_direction -= Time.deltaTime * speed;
				if(check_direction <= 0)
					isReverse = false;
            }
        }
    }
}
