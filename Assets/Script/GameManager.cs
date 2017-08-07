using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public List<GameObject> blocks = new List<GameObject>();
    public GameObject hud_prefab;
    private int gold, stage_gold, highscore;

    //dealing with UI label
    public GameObject[] label_obj = new GameObject[4];
    public UILabel[] label = new UILabel[4];

    //prefab
    public GameObject[] prefab = new GameObject[4];
    public GameObject gold_prefab;
    public List<GameObject> bonus = new List<GameObject>();
    public GameObject ball_prefab;
    private GameObject ball;
    public GameObject barrier;
    private GameObject gold_coin;
    public GameObject paddle_prefab;

    private bool transition = false;

    //power up
    private int up, right, down, left;
    private int up_max, right_max, down_max, left_max;


    private bool in_game = false;

    private int lives = 3;
    private int current_stage = 0;

    private bool stage_clear = false;
    private GameObject paddle;

    
    //Text element to display certain messages on
   // public GUIText FeedbackText;

    //Text to be displayed when entering one of the gamestates
    public string GameNotStartedText;
    public string GameCompletedText;
    public string GameFailedText;

    //Sounds to be played when entering one of the gamestates
    public AudioClip StartSound;
    public AudioClip FailedSound;

    public GameObject central;

    // Use this for initialization
    void Start()
    {
        //Restart();
        //InitStage(current_stage);
        SetGold();
        SetUpLabel();
        //InitLabel();
    }


    /// <summary>
    /// UI RELATED CODE
    /// </summary>

    private void SetUpLabel()
    {
        for (int i = 0; i < label_obj.Length; i++)
        {
            label[i] = label_obj[i].GetComponent<UILabel>();
        }
    }

    private void SetUpInGameUI()
    {
        label_obj[6].SetActive(false);
        label_obj[7].SetActive(false);
        label_obj[8].SetActive(false);
        for (int i = 0; i< 6; i++)
        {
            label_obj[i].SetActive(true);
        }
    }

    private void GameOverUI()
    {
        for (int i = 0; i < 6; i++)
        {
            label_obj[i].SetActive(false);
        }
        label_obj[6].SetActive(true);
        label_obj[7].SetActive(true);
        label_obj[8].SetActive(true);
        SetGameOverUI();
        PostGame();
    }

    private void SetGameOverUI()
    {
       
        label[6].text = "HighScore: " + highscore;
        label[7].text = "Score:       " + stage_gold;
        label[8].text = "Gold:          " + gold;

        
    }

    public void InitLabel()
    {
        
        label[0].text = "" + lives;
        label[1].text = "" + stage_gold;
        label[2].text = "" + up;
        label[3].text = "" + right;
        label[4].text = "" + down;
        label[5].text = "" + left;
    }

    public void SetLabel(int l, int v)
    {
        label[l].text = "" + v;
    }

    //Get gold when start the game by getting at central
    private void SetGold()
    {
        gold = 0;
    }

  
    public void DestroyBlock(GameObject b)
    {
        if(blocks.Contains(b))
        blocks.Remove(b);


    }

    /// END UI RELATED

    public void GetGold(int g)
    {
        stage_gold += g;
        SetLabel(1, stage_gold);
    }

    

    // Update is called once per frame
    void Update()
    {
        if(gold_flow)
        {
            //error checking
            if(stage_gold < 0)
            {
                gold_flow = false;
                return;
            }
            stage_gold--;
            gold++;
            SetGameOverUI();
            if(stage_gold == 0)
            {
                gold_flow = false;
            }
        }
    }

    


    /// <summary>
    /// GAME FLOW RELATED
    /// </summary>


    //Coroutine which waits and then restarts the level
    //Note: You need to call this method with StartRoutine(RestartAfter(seconds)) else it won't restart
    private void RestartAfter(float seconds)
    {
        Invoke("Restart", seconds);

       
    }

    //Helper to restart the level
    //call when the button is pushed
    public void Restart()
    {
        current_stage = 0;
        lives = 3;
        Time.timeScale = 1;
        gold_flow = false;
        CancelInvoke("HighScoreBlink");
        if(stage_gold > highscore)
        {
            highscore = stage_gold;
        }
        gold += stage_gold;

        stage_gold = 0;
        //Get power number from central
        //For now
        up = 1; right = 1; down = 1; left = 1;
        Debug.Log("In Restart");
        SetUpInGameUI();
        InitLabel();
        CreateBoard();
    }

    private void CreateBoard()
    {
        ball = Instantiate(ball_prefab, new Vector2(0, 0), Quaternion.identity);
        paddle = Instantiate(paddle_prefab, new Vector2(0, -16), Quaternion.identity);
        paddle.transform.parent = GameObject.FindGameObjectWithTag("Base").transform;
        in_game = true;
    }


    //This is called when the ball get to the end 
    //this function will deal with the remain coin/block/score/lives
    //then decide whether to call for next stage or game over
    public void GetNewStage()
    {
        //Check if there is anything left in scene
        //If there is, player lose a life and destroy everything
        if (blocks.Count > 0)
        {
            lives--;
            foreach (GameObject g in blocks)
            {
                Destroy(g);
            }
        }
        blocks.Clear();
        if (gold_coin != null)
        {
            Destroy(gold_coin);
        }

        //If there is still lives, continue on, otherwise, fail
        Debug.Log(lives);
        SetLabel(0, lives);
        if (lives > 0)
        {

            StartTransition();


        }
        else
        {
            //Fail
            GameOver();
        }
    }

    //Starting the transition into new stage
    private void StartTransition()
    {
        current_stage++;
        InitStage(current_stage);
        Time.timeScale = 0.3f;
        transition = true;
        NormalizeBall();
        NormalizeTime();
    }

    private void NormalizeBall()
    {
        ball.transform.localScale = new Vector2(1, 1);
        // Time.timeScale = 1;

    }

    private void NormalizeTime()
    {
        ball.SendMessage("NormalSpeed");
    }

    //Create new stage
    public void InitStage(int stage)
    {
        stage_clear = false;

        int j = 0;
        for (int i = 0; i < stage; i++)
        {
            if (i > 2)
                j = 2;
            if (i > 5)
                j = 3;

            GameObject g = Instantiate(prefab[(int)Random.Range(0, j)], new Vector3(Random.Range(-6f, 6f),
                Random.Range(-7f, 6f), 0), Quaternion.identity);
            g.transform.parent = paddle.transform.parent;
            blocks.Add(g);
        }
        float r = Random.Range(0f, 1f);
        Debug.Log(r);
        if (r < 0.33f)
        {
            gold_coin = Instantiate(gold_prefab, new Vector3(Random.Range(-6f, 6f),
                Random.Range(-7f, 6f), 0), Quaternion.identity);
        }
        //CreateBonus(0);
    }

    //When there is a chance that the game will create a premade stage instead of procedure generated stage
    void CreateBonus(int i)
    {
        GameObject g = Instantiate(bonus[i], new Vector3(0, 0, 0), Quaternion.identity);
        foreach (Transform child in g.transform)
        {
            child.parent = paddle.transform;
            blocks.Add(child.gameObject);
        }
    }

    //The end of transition, called when the ball touch the paddle
    private void EndTransition()
    {
        transition = false;
        Time.timeScale = 1;
    }



    //Helper to display some text
    /*  private void DisplayText(string text)
      {
          FeedbackText.text = text;
      }*/
    private void GameOver()
    {
        in_game = false;
        if (ball != null)
            Destroy(ball);
        if (paddle != null)
            Destroy(paddle);
        if (gold_coin != null)
            Destroy(gold_coin);
        if (blocks.Count > 0)
        {
            lives--;
            foreach (GameObject g in blocks)
            {
                Destroy(g);
            }
        }
        blocks.Clear();
        GameOverUI();
        central.SendMessage("GameOver");
        //gold += stage_gold;
        //central.SendMessage("Restart");
    }

    private void PostGame()
    {
        if(stage_gold > highscore)
        {
            HighScoreCelebrate();
        }
        else
        {
            AddGold();
        }
    }

    private bool high_blink;
    private int time_blink;
    private void HighScoreCelebrate()
    {
        highscore = stage_gold;
        SetGameOverUI();
        high_blink = false;
        time_blink = 0;
        HighScoreBlink();
        Invoke("AddGold",1);
    }


    private void HighScoreBlink()
    {

        label_obj[6].SetActive(high_blink);
        if (high_blink == true)
            time_blink++;
        high_blink = !high_blink;

        if (time_blink < 4)
        {
            Invoke("HighScoreBlink", 0.3f);
        }
    }

    private bool gold_flow;
    private void AddGold()
    {
        gold_flow = true;
    }

    /// <summary>
    /// END GAME FLOW
    /// </summary>




    //Call when a power is activate
    private void Power(int type)
    {
        if (!in_game)
        {
            if(type == 1)
            {

            }
            return;
        }
        if (transition)
            return;
        //up
        //Big Ball for s second
        if(type == 0 && up > 0)
        {
            up --;
            //then make ball big
            ball.transform.localScale = new Vector2(ball.transform.localScale.x * 2,
                                                    ball.transform.localScale.y * 2);
            //make it normal after x second;
            Invoke("NormalizeBall", 3);
        }

        //right
        //slow down for s second
        if (type == 1 && right > 0)
        {
            right--;
            //then make ball big
            ball.SendMessage("SlowDown");
            //make it normal after x second;
            Invoke("NormalizeTime", 3);
        }

        //down
        //if blocks.count > 0, make a barrier to let ball bounce back
        if (type == 2 && down > 0)
        {
            down--;
            barrier.SetActive(true);

        }
        //left
        //randomly destroy s blocks
        if (type == 3 && left > 0)
        {
            left--;
            if (blocks.Count < 3)
            {
                foreach (GameObject b in blocks)
                {
                    b.SendMessage("DelayDestroy");
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    blocks[i].SendMessage("DelayDestroy");
                }
            }
        }
        InitLabel();
    }

 
}
