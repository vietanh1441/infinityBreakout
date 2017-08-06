using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPosition : MonoBehaviour {
    public bool up, right, left;
	// Use this for initialization
	void Start () {
        if (left)
        {
            transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x
                                                        , transform.position.y, transform.position.z);
        }
        if (up)
        {
            transform.position = new Vector3(transform.position.x,
                Camera.main.ViewportToWorldPoint(new Vector3(0, 0.9f, 0)).y, transform.position.z);
        }
        if (right)
        {
            transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x
                                                        , transform.position.y, transform.position.z);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
