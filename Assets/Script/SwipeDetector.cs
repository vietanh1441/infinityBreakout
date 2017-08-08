using System.Collections;
using System.Collections.Generic;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class SwipeDetector : MonoBehaviour {
    GameObject GM;
    GameManager gm_scr;
    GameObject central;
    Central central_scr;
    Transform camera;
    Vector2 current_camera;
	// Use this for initialization
	void Start () {
        GM = GameObject.FindGameObjectWithTag("GM");
        central = GameObject.FindGameObjectWithTag("Central");
        central_scr = central.GetComponent<Central>();
        gm_scr = GM.GetComponent<GameManager>();
        camera = Camera.main.transform;
        current_camera = camera.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Swiping(Gesture gesture)
    {
        Debug.Log(gesture.swipe);
        if (gm_scr.in_game)
        {
            
            if (gesture.swipe == EasyTouch.SwipeDirection.Up)
            {
                GM.SendMessage("Power", value: 0);
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
        else
        {
            FingerUp();
        }
    }

    public void Dragging(Gesture gesture)
    {
        if (gm_scr.in_game)
            return;
        Vector2 start = gesture.startPosition;
        Vector2 current = gesture.position;
        Vector2 delta = current - start;
        float x = delta.x / 400 *15;
        Debug.Log(x);
       

        camera.position = new Vector3(current_camera.x - x, camera.position.y, -10);
    }

    public void FingerUp()
    {
        int c = central_scr.cur_scene;
        float v = 15f * c;
        if(current_camera.x - camera.position.x > 7.5f)
        {
            camera.position = new Vector3(-15 + v, camera.position.y,-10);
            central_scr.cur_scene--;
        }
        else if (current_camera.x - camera.position.x < -7.5f)
        {
            camera.position = new Vector3(15 + v, camera.position.y,-10);
            central_scr.cur_scene++;
          
        }
        else
        {
            camera.position = new Vector2(0+v, camera.position.y);
           
        }
        current_camera = camera.position;
    }
}
