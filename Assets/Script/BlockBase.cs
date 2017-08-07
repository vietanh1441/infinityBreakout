using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour {

    GameObject GM;
    public int gold;
    
    private GameObject hud;
    
    // Use this for initialization
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM");
        if(gold == 0)
        {
            gold = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).x < -0.05f)
        {
            transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector3(1.05f, 0, 0)).x, transform.position.y);
        }
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1.05f)
        {
            transform.position = new Vector2(Camera.main.ViewportToWorldPoint(new Vector3(-0.05f, 0, 0)).x, transform.position.y);
        }
    }



    protected void DelayDestroy()
    {
        Invoke("DoDestroy", 0.1f);
    }

    protected void DoDestroy()
    {
        GetComponent<Collider2D>().enabled = false;
        GM.SendMessage("DestroyBlock", gameObject);
        AddMoney();
        gameObject.GetComponent<DestroyAnimation>().GetDestroyed();
    }

    protected void AddMoney()
    {
        GM.SendMessage("GetGold", gold);
        AddHud();
    }

    private void AddHud()
    {
        hud = NGUITools.AddChild(GameObject.FindGameObjectWithTag("UIRoot"), GM.GetComponent<GameManager>().hud_prefab);
        hud.GetComponent<HUDText>().Add(gold, Color.yellow, 1);
        hud.SendMessage("SetTarget", transform);
    }
}
