using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Central : MonoBehaviour {
    public GameObject play_button;
    public GameObject GM;
    public int cur_scene;

	// Use this for initialization
	void Start () {
        cur_scene = 0;
	}


    #region Button 
    #region ShoppingButton

    public void Shopping_0 ()
    {

    }

    public void Shopping_1()
    {

    }
    public void Shopping_2()
    {

    }
    public void Shopping_3()
    {

    }
    public void Shopping_4()
    {

    }
#endregion

    public void Play()
    {
        play_button.SetActive(false);
        GM.SendMessage("Restart");
    }

    #endregion
    // Update is called once per frame
    void Update () {
		
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
