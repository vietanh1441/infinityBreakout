using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Central : MonoBehaviour {
    public GameObject play_button, practice_button;
    public GameObject GM;
    public int cur_scene;

    private GameObject bonus_obj;

    public List<GameObject> bonus = new List<GameObject>();

    public GameObject extra_bg;

    //price of each item
    public int[] price = new int[5];

    //modifier, the rate at which the price of the item increase
    public int[] modifier = new int[5];

    //the amount of each current item
    public int[] rss = new int[5];
    public int gold;
    public int highscore;
    public bool in_shop;

    //UI
    public GameObject[] label_obj = new GameObject[4];
    public UILabel[] label = new UILabel[4];

    // Use this for initialization
    void Start () {
        cur_scene = 0;
        Init();
        SetUpLabel();
        InitLabel();
        //Invoke("FadeOut", 2);
	}


    public void FadeIn()
    {
        extra_bg.SendMessage("FadeIn");
        
    }

    public void FadeOut()
    {
        extra_bg.SendMessage("FadeOut");
    }

    private void Init()
    {
        //Get them from playerPref
    }

    #region Button 
    #region ShoppingButton

    public void Shopping_0 ()
    {

        Buy(0);
    }

    public void Shopping_1()
    {
        Buy(1);
    }
    public void Shopping_2()
    {
        Buy(2);
    }
    public void Shopping_3()
    {
        Buy(3);
    }
    public void Shopping_4()
    {
        Buy(4);
    }
    public void Shopping_5()
    {
        Camera.main.transform.position = new Vector3(36, -30, -10);
        in_shop = true;
    }

    public void Buy(int item)
    {
        //Check Price
        if(gold >= price[item])
        {
            Debug.Log("Transaction Successful");
            //if price is good, remove gold by the amount and increase comodity by one
            gold -= price[item];
            price[item] = price[item] * modifier[item];
            rss[item]++;
            InitLabel();
        }
        else
        {
            Debug.Log("NotEnoughMoney");
        }


    }


    private int current_bonus;
    public void DisplayPractice(int b)
    {
        Debug.Log(b);
        //Set BG to Fade in
        FadeIn();

        current_bonus = b-1;
        //Invoke("RepealAndReplace",1);

        //Set BG to fade out
        //Invoke("FadeOut", 2);
    }

    public void RepealAndReplace()
    {
        //if there is an old one, destroy it
        if(bonus_obj!=null)
        {
            Destroy(bonus_obj);
        }
        Debug.Log(bonus[current_bonus]);
        //Instantiate the item
        bonus_obj = Instantiate(bonus[current_bonus], new Vector3(36,-28,0), Quaternion.identity);
        FadeOut();
    }

    #endregion

    public void Play()
    {
        play_button.SetActive(false);
        GM.SendMessage("Restart");
    }

    public void PracticePlay()
    {

        GM.SendMessage("Practice");
        Camera.main.transform.position = new Vector3(0, -4, -10);
    }

    public void PractiveOver()
    {
        Camera.main.transform.position = new Vector3(36, -4, -10);
    }
    #endregion
    // Update is called once per frame
    void Update () {
		
	}

    #region UI
    private void SetUpLabel()
    {
        for (int i = 0; i < label_obj.Length; i++)
        {
            label[i] = label_obj[i].GetComponent<UILabel>();
        }
    }

    public void InitLabel()
    {
        
        label[0].text = "" + gold;
        SetUpPrice();
        SetUpRss();
    }

    public void SetUpPrice()
    {
        for(int i = 1; i < 6; i++)
        {
            label[i].text = " " + price[i-1];
        }
    }

    public void SetUpRss()
    {
        for (int i = 6; i <11; i++)
        {
            label[i].text = " " + rss[i-6];
        }
    }
    #endregion


    public void GameOver()
    {
        play_button.SetActive(true);
    }

    public void Restart()
    {
        play_button.SetActive(true);

    }

    public void TurnRight()
    {

    }
}
