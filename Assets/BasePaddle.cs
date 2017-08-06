using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePaddle : MonoBehaviour {

    public float freedom = 19.25F;
    public float speed = 40f;
    private bool isFirstTap = false;
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;


    public void GoLeft()
    {
        transform.position = new Vector2(transform.position.x - Time.deltaTime * speed,
                                            transform.position.y);
      
    }

    public void GoRight()
    {
        transform.position = new Vector2(transform.position.x + Time.deltaTime * speed,
                                            transform.position.y);
      
    }
}
