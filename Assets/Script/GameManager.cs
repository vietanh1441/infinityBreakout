using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public List<GameObject> blocks = new List<GameObject>();
    public GameObject hud_prefab;
    private int gold;

    public GameObject[] prefab = new GameObject[4];
    public List<GameObject> bonus = new List<GameObject>();
    public GameObject ball_prefab;
    private GameObject ball;
    public GameObject barrier;

    private bool transition = false;

    private int up, right, down, left;

    private int up_max, right_max, down_max, left_max;
    public GameObject paddle_prefab;

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

    }

    //Get gold when start the game by getting at central
    private void SetGold()
    {
        gold = 0;
    }

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
            
            GameObject g = Instantiate(prefab[(int)Random.Range(0,j)], new Vector3(Random.Range(-6f, 6f)+paddle.transform.position.x,
                Random.Range(-7f,8f),0), Quaternion.identity);
            g.transform.parent = paddle.transform.parent;
            blocks.Add(g);
        }
        //CreateBonus(0);
    }

    public void DestroyBlock(GameObject b)
    {
        blocks.Remove(b);


    }

    public void GetGold(int g)
    {
        gold += g;
    }

    public void GetNewStage()
    {
        //Check if there is anything left in scene
        //If there is, player lose a life and destroy everything
        if(blocks.Count > 0)
        {
            lives--;
            foreach(GameObject g in blocks)
            {
                Destroy(g);
            }
        }
        blocks.Clear();

        //If there is still lives, continue on, otherwise, fail
        Debug.Log(lives);
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

    private void StartTransition()
    {
        current_stage++;
        InitStage(current_stage);
        Time.timeScale = 0.3f;
        transition = true;
        NormalizeBall();
        NormalizeTime();
    }

    private void EndTransition()
    {
        transition = false;
        Time.timeScale = 1;
    }

    void CreateBonus(int i)
    {
        GameObject g = Instantiate(bonus[i], new Vector3(0, 0, 0), Quaternion.identity);
        foreach(Transform child in g.transform)
        {
            child.parent = paddle.transform;
            blocks.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    //Helper to display some text
  /*  private void DisplayText(string text)
    {
        FeedbackText.text = text;
    }*/
    private void GameOver()
    {
        if (ball != null)
            Destroy(ball);
        if (paddle != null)
            Destroy(paddle);
        if (blocks.Count > 0)
        {
            lives--;
            foreach (GameObject g in blocks)
            {
                Destroy(g);
            }
        }
        blocks.Clear();
        central.SendMessage("Restart");
    }
    //Coroutine which waits and then restarts the level
    //Note: You need to call this method with StartRoutine(RestartAfter(seconds)) else it won't restart
    private void RestartAfter(float seconds)
    {
        Invoke("Restart", seconds);

       
    }

    //Helper to restart the level
    public void Restart()
    {
        current_stage = 0;
        lives = 3;
        Time.timeScale = 1;
        //Get power number from central
        //For now
        up = 1; right = 1; down = 1; left = 1;

        CreateBoard();
    }

    private void CreateBoard()
    {
        ball = Instantiate(ball_prefab, new Vector2(0, 0), Quaternion.identity);
        paddle = Instantiate(paddle_prefab, new Vector2(0, -16), Quaternion.identity);
        paddle.transform.parent = GameObject.FindGameObjectWithTag("Base").transform;
    }

    private void Power(int type)
    {
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
    }

    private void NormalizeBall()
    {
        ball.transform.localScale = new Vector2(1,1);
       // Time.timeScale = 1;

    }

    private void NormalizeTime()
    {
        ball.SendMessage("NormalSpeed");
    }
}
