using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger : MonoBehaviour {

    GameObject GM;

    // Use this for initialization
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y <0)
        {
            GM.SendMessage("GetNewStage");
        }
    }
}
