using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

	private GameObject myUI;
	private string time;
	void Start () {
		myUI = GameObject.Find("Canvas");
		time = GameManager.instance.minutes + "." + GameManager.instance.seconds;
		myUI.transform.Find("Time").GetComponent<Text>().text = PlayerPrefs.GetString("Player Time");
	}

	public void Submit(){
		string playername = myUI.transform.Find("InputField").FindChild("Text").GetComponent<Text>().text;
        PlayerPrefs.SetString("Player Name", playername);
        PlayerPrefs.SetString("Player Time", time);
        PlayerPrefs.SetFloat("Time", GameManager.instance.time_count);
		SceneManager.LoadScene("Hi Score");
	}
}
