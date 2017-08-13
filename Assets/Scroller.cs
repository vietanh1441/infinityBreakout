using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {

    public List<GameObject> list = new List<GameObject>();
    public int current;

    private int y;
    private int max;
	// Use this for initialization
	void Start () {
        y = (int)transform.position.y;
        InitList();
	}

    private void InitList()
    {
        max = GameObject.FindGameObjectWithTag("Central").GetComponent<Central>().bonus.Count;

        list[0].SendMessage("SetText", value: "" + (max -1));
        list[1].SendMessage("SetText", value: ""+max);
        list[2].SendMessage("SetText", value: "1");
        list[3].SendMessage("SetText", value: "2");
        list[4].SendMessage("SetText", value: "3");
        current = 1;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (transform.position.y - y >= 2)
        {
            GoUp();
            y = y + 2;
            transform.position =new Vector2(transform.position.x, y);
        }
        else if ( transform.position.y - y <= -2)
        {
            GoDown();
            y = y - 2;
            transform.position = new Vector2(transform.position.x, y);
        }
	}

    private void GoUp()
    {
        GameObject t = list[0];
        list.RemoveAt(0);
        int a = current + 3;
        if (a > max)
        {
            a = a - max;
        }
        t.SendMessage("SetText", value: "" + a);
        t.SendMessage("AddOffset", new Vector3(0, -10, 0));
        list.Add(t);
        current++;
        if(current>max)
        {
            current = current - max;
        }
    }

    private void GoDown()
    {
        GameObject t = list[4];
        list.RemoveAt(4);

        int a = current - 3;
        if(a<1)
        {
            if (a == 0)
                a = max;
            else
                a = max + a;
        }
        t.SendMessage("SetText", value: "" + a);
        t.SendMessage("AddOffset", new Vector3(0, 10, 0));
        list.Insert(0, t);
        current--;
        if (current == 0)
            current = max;
    }
}
