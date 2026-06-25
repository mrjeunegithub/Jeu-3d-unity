using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfos : MonoBehaviour
{
    public static PlayerInfos pi;

    public int life = 3;
    public int coins = 0;
    public int time = 400;
    public int score = 0;
    public int mask = 0;
    public TMP_Text coinTxt;
    public TMP_Text timeTxt;
    public TMP_Text scoreTxt;
    public TMP_Text maskTxt;
    public bool endLevel = false;
    
    public Image[] Lifes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        pi = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        coinTxt.text = "x" + coins.ToString();
        timeTxt.text = "Time \n" + time.ToString();
        scoreTxt.text = "00" + score.ToString();
        maskTxt.text = mask.ToString() + " / 3";
        endLevel = false;
    }

    public void SetLife(int life)
    {
        this.life += life;

        if (this.life < 0)
        {
            this.life = 0;
        }

        if (this.life > 3)
        {
            this.life = 3;
        }

        SetHealthBar();
    }
    public void SetMask()
    {
        this.mask ++;

        if (this.life > 3)
        {
            this.life = 3;
        }

        SetMaskBar();
    }

    public void SetTime(int amount)
    {
        this.time += amount;
        if (this.time <= 0)
        {
            this.time = 0;
        }
        SetTimeZone();
    }

    public void SetScore(int amount)
    {
        this.score += amount;
        SetScoreBar();
    }

    public int GetTime()
    {
        return this.time;
    }

    public void GetCoins()
    {
        this.coins ++;
        coinTxt.text = "x" + coins.ToString();
    }

    public void SetLevelEnd()
    {
        this.endLevel = true;        
    }

    public bool GetEndLevel()
    {
        return endLevel;
    }

    public int GetScore()
    {
        return this.score;
    }

    void SetHealthBar()
    {
        foreach (Image img in Lifes)
        {
            img.enabled = false;
        }

        for (int i=0; i<this.life; i++)
        {
            Lifes[i].enabled = true;
        }
    }

    void SetTimeZone()
    {
        timeTxt.text = "Time \n" + this.time.ToString();
    }

    void SetScoreBar()
    {
        scoreTxt.text = score.ToString();
    }

    void SetMaskBar()
    {
        maskTxt.text = mask.ToString() + " / 3";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
