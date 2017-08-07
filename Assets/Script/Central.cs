using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Central : MonoBehaviour {
    public GameObject play_button;
    public GameObject GM;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        play_button.SetActive(false);
        GM.SendMessage("Restart");
    }

    public void GameOver()
    {
        play_button.SetActive(true);
    }

    public void Restart()
    {
        play_button.SetActive(true);

    }

    public void TurnRight()
    {

    }
}
