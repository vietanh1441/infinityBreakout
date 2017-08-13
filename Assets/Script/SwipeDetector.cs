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
    Vector2 current_camera, current_scroller;
    public Transform scroller;
    public GameObject[] UIparent = new GameObject[4];
	// Use this for initialization
	void Start () {
        GM = GameObject.FindGameObjectWithTag("GM");
        central = GameObject.FindGameObjectWithTag("Central");
        central_scr = central.GetComponent<Central>();
        gm_scr = GM.GetComponent<GameManager>();
        camera = Camera.main.transform;
        current_camera = camera.position;
        current_scroller = scroller.position;
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

        if (central_scr.in_shop)
        {
            float y = delta.y / 100;
            
            scroller.position = new Vector3(scroller.position.x, current_scroller.y +y, 0);
            return;
        }

        float x = delta.x / 400 *7.5f;
        //Debug.Log(x);
        

        camera.position = new Vector3(current_camera.x - x, camera.position.y, -10);

        
    }

    public void FingerUp()
    {
        if(central_scr.in_shop)
        {
            //Fix the scroller position
            current_scroller.y = (int)scroller.position.y;

            //Display practice scene
            central_scr.DisplayPractice(scroller.gameObject.GetComponent<Scroller>().current);

            return;


        }
        int c = central_scr.cur_scene;
        float v = 18f * c;
        if(current_camera.x - camera.position.x > 3.5f)
        {
            camera.position = new Vector3(-18 + v, camera.position.y,-10);
            central_scr.cur_scene--;
        }
        else if (current_camera.x - camera.position.x < -3.5f)
        {
            camera.position = new Vector3(18 + v, camera.position.y,-10);
            central_scr.cur_scene++;
          
        }
        else
        {
            camera.position = new Vector2(0+v, camera.position.y);
           
        }
        current_camera = camera.position;
    }
}
