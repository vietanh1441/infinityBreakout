using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeFrom : MonoBehaviour {

    public float y;
    public float range;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        Color temp = gameObject.GetComponent<UILabel>().color;

        temp.a = range - Mathf.Abs(transform.position.y - y)*speed;
        //Debug.Log(temp.a);
        gameObject.GetComponent<UILabel>().color = temp;

    }
}
