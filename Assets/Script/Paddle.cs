using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public float freedom = 19.25F;
    public float speed = 40f;
    private bool isFirstTap = false;
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    //public GameObject Text;

    void Update()
    {
        /*
        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector2(mousePosition.x, transform.position.y);
        }
        */

       // Debug.Log(Camera.main.WorldToViewportPoint(transform.position).x);
        if (Camera.main.WorldToViewportPoint(transform.position).x < -0.1f)
        {
            transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0, 0)).x, transform.position.y);
        }
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1.1f)
        {
            transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, 0, 0)).x, transform.position.y);
        }
    }
  

  
    public void GoRight()
    {
        transform.position = new Vector2(transform.position.x + Time.deltaTime * speed,
                                            transform.position.y);
        if (transform.position.x > 18)
        {
            transform.position = new Vector2(-10, transform.position.y);
        }
    }
}

