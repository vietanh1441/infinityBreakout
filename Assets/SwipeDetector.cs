using System.Collections;
using System.Collections.Generic;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class SwipeDetector : MonoBehaviour {
    GameObject GM;
	// Use this for initialization
	void Start () {
        GM = GameObject.FindGameObjectWithTag("GM");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Swiping(Gesture gesture)
    {
        Debug.Log(gesture.swipe);
        if(gesture.swipe == EasyTouch.SwipeDirection.Up)
        {
            GM.SendMessage("Power", value:0);
        }
        else if (gesture.swipe == EasyTouch.SwipeDirection.Right)
        {
            GM.SendMessage("Power", value: 1);
        }
        else if (gesture.swipe == EasyTouch.SwipeDirection.Left)
        {
            GM.SendMessage("Power", value: 3);
        }
        else if (gesture.swipe == EasyTouch.SwipeDirection.Down)
        {
            GM.SendMessage("Power", value: 2);
        }
    }
}
