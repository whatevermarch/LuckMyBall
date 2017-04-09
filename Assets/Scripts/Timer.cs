using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{

    [HideInInspector]
    public string minutes;
    [HideInInspector]
    public string seconds;
    private float startTime;
    private float t = 0;
    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        bool isEnd = GameManager.instance.isEnd;
        if (!isEnd)
        {
            t = Time.time - startTime;
            minutes = ((int)t / 60).ToString();
            seconds = (t % 60).ToString("f2");
            gameObject.GetComponent<Text>().text = minutes + "." + seconds;
        }
        else{
            GameManager.instance.time_count = t;
            GameManager.instance.minutes = minutes;
            GameManager.instance.seconds = seconds;
            Debug.Log("Time : " + t);
            Debug.Log("PlayerPrefs : " + PlayerPrefs.GetFloat("Time"));
            if(t < PlayerPrefs.GetFloat("Time")){
                Debug.Log("Hello");
                SceneManager.LoadScene("End Game");
            }
            else
                SceneManager.LoadScene("Hi score");
        }

    }
}
