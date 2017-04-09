using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplayController : MonoBehaviour {

	public Image Health;
	public float CurrentHP;
	public float MaxHp;
	public int Bullet;
	public int MaxBomb = 4;
	public int Bomb;
	public int Life;
	public Text display_bullet;
	public Image[] LP = new Image[3];
	public Image[] BombImage = new Image[4];
	

	// Use this for initialization
	void Start () {
		Bullet = GameManager.instance.bullet;
		Bomb = GameManager.instance.bomb;
		MaxHp = GameManager.instance.maxHealth;
		CurrentHP = GameManager.instance.health;
		Life = GameManager.instance.life;
		Bomb = GameManager.instance.bomb;
	}
	
	// Update is called once per frame
	void Update () {
		Bullet = GameManager.instance.bullet;
		Bomb = GameManager.instance.bomb;
		CurrentHP = GameManager.instance.health;
		Life = GameManager.instance.life;
		Bomb = GameManager.instance.bomb;

		HealthBarHandler ();
		LifePointController ();
		BombController ();
		BulletController ();
	}

	void HealthBarHandler()
	{
		//if (hp != Health.fillAmount) {}
		Health.fillAmount = CurrentHP/100;

		if (Health.fillAmount >= 1) {
			Health.color = new Color32 (0, 255, 0, 255);
		} 
		else if (Health.fillAmount >= 0.3) {
			Health.color = new Color32 ((byte)MapHpToPercent (CurrentHP, 30, MaxHp, 255, 0), 255, 0, 255);
		} 
		else {
			Health.color = new Color32 (255 , (byte)MapHpToPercent (CurrentHP, 0, MaxHp/2, 0, 255), 0, 255);
		}

	}

	float MapHpToPercent(float value, float inMin , float inMax, float outMin , float outMax)
	{
		return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	void LifePointController()
	{
		if (Life == 3) {
			LP [0].color = new Color32 (195, 59, 59, 255);
			LP [1].color = new Color32 (195, 59, 59, 255);
			LP [2].color = new Color32 (195, 59, 59, 255);
		} else if (Life == 2) {
			LP [0].color = new Color32 (195, 59, 59, 255);
			LP [1].color = new Color32 (195, 59, 59, 255);
			LP [2].color = new Color32 (255, 255, 255, 255);
		} else if (Life == 1) {
			LP [0].color = new Color32 (195, 59, 59, 255);
			LP [1].color = new Color32 (255, 255, 255, 255);
			LP [2].color = new Color32 (255, 255, 255, 255);
		} else if (Life == 0) {
			LP [0].color = new Color32 (255, 255, 255, 255);
			LP [1].color = new Color32 (255, 255, 255, 255);
			LP [2].color = new Color32 (255, 255, 255, 255);
		}
	}

	void BombController()
	{
		
		if (Bomb == 1) {
			BombImage [0].enabled = true;
			BombImage [1].enabled = false;
			BombImage [2].enabled = false;
			BombImage [3].enabled = false;
		}
		else if(Bomb == 2)
			{
				BombImage [0].enabled = true;
				BombImage [1].enabled = true;
				BombImage [2].enabled = false;
				BombImage [3].enabled = false;

			}
		else if(Bomb == 3)
			{
				BombImage [0].enabled = true;
				BombImage [1].enabled = true;
				BombImage [2].enabled = true;
				BombImage [3].enabled = false;

			}
		else if(Bomb == 4)
			{
				BombImage [0].enabled = true;
				BombImage [1].enabled = true;
				BombImage [2].enabled = true;
				BombImage [3].enabled = true;

			}
		else {
				BombImage [0].enabled = false;
				BombImage [1].enabled = false;
				BombImage [2].enabled = false;
				BombImage [3].enabled = false;
		}
			
	}
	void BulletController()
	{
		display_bullet.text = "" + Bullet;
	}
}
