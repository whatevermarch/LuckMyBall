using UnityEngine;
using System.Collections;

public class PlayerControllerPhysicBase : MonoBehaviour
{

    public float acc;
    public float maxSpeed;

    private Rigidbody rb;

    Vector3 movement;
    bool isGrounded;
    float sqrMaxSpeed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        sqrMaxSpeed = maxSpeed * maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y == 0 && !isGrounded)
            isGrounded = true;
        if (transform.position.y < -5)
        {
            GameManager.instance.isGameover = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Move(h, v);
        Jump();
        /*
		if (isGrounded) {
			Vector3 friction = 3 * Vector3.Normalize (new Vector3 (-rb.velocity.x, 0, -rb.velocity.z));
			rb.AddForce (friction);
		}*/
    }

    void Move(float h, float v)
    {

        movement = Vector3.Normalize(new Vector3(h, 0, v)) * acc;

        if (rb.velocity.sqrMagnitude >= sqrMaxSpeed)
        {
            Vector3 resistForce = Vector3.Normalize(new Vector3(-rb.velocity.x, 0, -rb.velocity.z)) * maxSpeed;
            rb.AddForce(resistForce);
            //rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        else
            rb.AddForce(movement);

    }

    void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * 300);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("Checkpoint: " + other.gameObject.name);
            GameManager.instance.checkPoint = other.gameObject.transform.position;
        }
        else if (other.gameObject.CompareTag("Key"))
        {
            Transform parent = other.gameObject.transform.parent;
            parent.gameObject.GetComponent<DoorController>().keys -= 1;
            Debug.Log("Key left: " + parent.gameObject.GetComponent<DoorController>().keys);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Item"))
        {
            ItemController item = other.gameObject.GetComponent<ItemController>();
            if (item.type != "Bullet")
            {
                int maxItem = GameManager.instance.maxItem[item.type];
                if (GameManager.instance.item_quantity[item.type] != maxItem)
                {
                    if (GameManager.instance.item_quantity[item.type] + item.quantity > maxItem)
                    {
                        GameManager.instance.item_quantity[item.type] = maxItem;
                    }
                    else
                        GameManager.instance.item_quantity[item.type] += item.quantity;
                    Destroy(other.gameObject);
                }
            }
            else
            {
                GameManager.instance.item_quantity[item.type] += item.quantity;
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.instance.isEnd = true;
        }
    }
}
