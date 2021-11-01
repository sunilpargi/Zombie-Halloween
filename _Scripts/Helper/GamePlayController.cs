using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ZombieGoal
{
    PLAYER,
    FENCE
}

public enum GameGoal
{
    KILL_ZOMBIES,
    WALK_TO_GOAL_STEPS,
    DEFEND_FENCE,
    TIMER_COUNTDOWN,
    GAME_OVER
}

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    [HideInInspector]
    public bool bullet_And_Bullet_FX, rocket_Bullet_Created;

    [HideInInspector]
    public bool playerAlive , fenceDestroyed;

    public ZombieGoal zombieGoal = ZombieGoal.PLAYER;
    public GameGoal gameGoal = GameGoal.DEFEND_FENCE;

    public GameObject pausePanel, gameOver;

    public int zombie_Count = 20;
    public int Timer_Count = 100;

    private Transform player_target;
    private Vector3 player_Previous_Position;

    public int step_Count = 100;
    private int initial_Step_Count;

    public Text zombieCounter_text, timer_Text, stepCounter_Text;
    private Image playerLife;

    [HideInInspector]
    public int coinCount;
    void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        playerAlive = true;

        if(gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
            player_target = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
            player_Previous_Position = player_target.position;

            initial_Step_Count = step_Count;
            //stepCounter_Text = GameObject.Find("StepCounter").GetComponent<Text>();

            stepCounter_Text.text = step_Count.ToString();

        }

        if(gameGoal == GameGoal.TIMER_COUNTDOWN || gameGoal == GameGoal.DEFEND_FENCE)
        {
            timer_Text = GameObject.Find("TimerCounter Text").GetComponent<Text>();
            timer_Text.text = Timer_Count.ToString();

            InvokeRepeating("TimerCountDown", 0, 1);
        }

        if (gameGoal == GameGoal.KILL_ZOMBIES)
        {
            zombieCounter_text = GameObject.Find("ZombieCounter").GetComponent<Text>();
            zombieCounter_text.text = zombie_Count.ToString();

        }

        playerLife = GameObject.Find("Life Full").GetComponent<Image>();
    }

    private void Update()
    {
        if(gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
            CountPlayerMovement();
        }
    }

    private void CountPlayerMovement()
    {
        Vector3 playerCurrentPosition = player_target.position;

        float dist = Vector3.Distance(new Vector3(player_target.position.x, 0, 0), new Vector3(player_Previous_Position.x, 0, 0));

        if(playerCurrentPosition.x > player_Previous_Position.x)
        {
            if(dist > 1)
            {
                step_Count--;

                if(step_Count <= 0)
                {
                    GameOver();
                }

                player_Previous_Position = player_target.position;
            }
        }

        else if(playerCurrentPosition.x < player_Previous_Position.x)
        {
            if(dist > 0.8f)
            {

                step_Count++;

                if (step_Count >= initial_Step_Count)
                {
                    step_Count = initial_Step_Count;

                   
                }

                player_Previous_Position = player_target.position;
            }
        }

        stepCounter_Text.text = step_Count.ToString();
    }

    private void OnDisable()
    {
        instance = null;
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;

        }
    }

    public void pausepanel()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumePanel()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void Quitgame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(TagManager.MAIN_MENU_NAME);
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    void TimerCountDown()
    {
        Timer_Count--;
        timer_Text.text = Timer_Count.ToString();

        if(Timer_Count <= 0)
        {
            CancelInvoke("TimerCountDown");

            GameOver();
        }
    }

   public void ZombieDied()
    {
        zombie_Count--;
        zombieCounter_text.text = zombie_Count.ToString();

        if (zombie_Count <= 0)
        {
            GameOver();
        }
    }

    public void PlayerLifeCounter(float fillPrecentage)
    {
        fillPrecentage /= 100f;

        playerLife.fillAmount = fillPrecentage;
    }
}
