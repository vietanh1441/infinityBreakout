using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour {
    private bool big = false;
    private bool shrink = false;
    public bool coin = false;
	// Use this for initialization
	void Start () {
        big = false;
        shrink = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (coin)
            return;
		if(big)
        {
            transform.localScale = new Vector2(transform.localScale.x + Time.deltaTime*3, 
                                               transform.localScale.y + Time.deltaTime*3);
        }
        else if (shrink)
        {
            transform.localScale = new Vector2(transform.localScale.x - Time.deltaTime*3,
                                               transform.localScale.y - Time.deltaTime*3);
            if(transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }
	}

    public void GetDestroyed()
    {
        big = true;
        Invoke("GetShrink", 0.5f);
    }

    public void GetShrink()
    {
        big = false;
        shrink = true;
    }
}
