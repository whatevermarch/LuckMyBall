using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

	private GameObject myUI;
	void Start () {
		GameManager.instance.isEnd = false;
		myUI = GameObject.Find("Canvas");
		myUI.transform.Find("Name").GetComponent<Text>().text = PlayerPrefs.GetString("Player Name");
		myUI.transform.Find("Time").GetComponent<Text>().text = PlayerPrefs.GetString("Player Time");
	}
}