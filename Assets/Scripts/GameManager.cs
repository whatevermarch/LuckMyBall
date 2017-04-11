using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Canvas timer;
    public GameObject Player = null;
    public Transform startPoint;

    public int maxHealth;
    public int maxBomb;
    public int maxLife;
    public int health;
    public int life;
    public int bullet;
    public int bomb;

	int lastHealth;
	int lastBullet;
	int lastBomb;

    [HideInInspector]
    public string minutes = "Hello";
    [HideInInspector]
    public string seconds = "Darkness";
    [HideInInspector]
    public float time_count;
    [HideInInspector]
    public bool isGameover = false;
    [HideInInspector]
    public bool isEnd = false;
    [HideInInspector]
    public Vector3 checkPoint;
    [HideInInspector]
    public Dictionary<string, int> maxItem = new Dictionary<string, int>();
    [HideInInspector]
    public Dictionary<string, int> item_quantity = new Dictionary<string, int>();

    public void Respawn()
    {
        Player.transform.position = checkPoint;
		health = lastHealth;
		bullet = lastBullet;
		bomb = lastBomb;
		Rigidbody rb = Player.GetComponent<Rigidbody> ();
		rb.velocity = Vector3.zero;
    }

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        if (Player)
        {
			checkPoint = startPoint.position;
			lastHealth = health;
			lastBullet = bullet;
			lastBomb = bomb;
			Player.transform.position = startPoint.position;
            maxItem.Add("Bomb", maxBomb);
            maxItem.Add("Health", maxHealth);
            maxItem.Add("Life", maxLife);
            item_quantity.Add("Bomb", bomb);
            item_quantity.Add("Health", health);
            item_quantity.Add("Life", life);
            item_quantity.Add("Bullet", bullet);
        }
    }

    void Update()
    {
        if (Player)
        {
			if (isEnd) {
				SceneManager.LoadScene ("End Game");
			}
        }
    }

	public void setLastAttrib(int hp, int bullet, int bomb){
		lastHealth = hp;
		lastBullet = bullet;
		lastBomb = bomb;
	}
}