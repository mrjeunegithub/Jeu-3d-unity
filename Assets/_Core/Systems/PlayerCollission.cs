using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerCollission : MonoBehaviour
{
    public int life = 3;
    public int coins = 0;
    SpriteRenderer sr;
    Rigidbody2D rb;
    bool IsInvinsible=false;
    public Transform flag;
    public Transform endFlag;
    public Transform endWalk;
    public Transform startWalk;
    public AudioClip coinClip;
    public AudioClip maskClip;
    public AudioClip deathClip;
    public AudioClip endClip;
    public GameObject pickupEffect;
    public GameObject winEffect;
    public Transform winPointAppear;

    Vector3 endPosition;
    Vector3 SW;
    AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("UpdateTime");
        endPosition = endFlag.position;
        SW = startWalk.position;
    }

     /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ennemis") && !IsInvinsible)
        {
            IsInvinsible = true;
            PlayerInfos.pi.SetLife(-1);
            TakeDamage();
        }

        if (other.CompareTag("coin"))
        {
            audioSource.PlayOneShot(coinClip, 0.3f);
            GameObject go = Instantiate(pickupEffect,other.transform.position + new Vector3 (0, 0, 0),Quaternion.identity);
            PlayerInfos.pi.GetCoins();
            Destroy(go, 0.3f);
            Destroy(other.gameObject);
            PlayerInfos.pi.SetScore(100);
        }

        if (other.CompareTag("EndZone"))
        {
            int ajoutScore = PlayerInfos.pi.GetTime() * 50 + 1000;
            PlayerInfos.pi.SetScore(ajoutScore);
            SceneManager.LoadScene("Fin");
        }

         if (other.CompareTag("mask"))
        {
            audioSource.PlayOneShot(maskClip);
            PlayerInfos.pi.SetMask();
            PlayerInfos.pi.SetScore(10000);
            Destroy(other.gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("fall"))
        {
            PlayerInfos.pi.SetLife(-3);
            TakeDamage();
        }

        if (other.gameObject.CompareTag("EndLevel"))
        {
            audioSource.PlayOneShot(endClip, 0.3f);
            StartCoroutine("RaisedFlag");
            GameObject go = Instantiate(winEffect, winPointAppear.position + new Vector3 (0, 1, 0),Quaternion.identity);
            rb.position = SW;
            PlayerInfos.pi.SetLevelEnd();
            StartCoroutine("WalkEnd");
            Destroy(go, 2f);
        }

    }

    

    public void TakeDamage()
    {
        if (PlayerInfos.pi.life <= 0)
        {
            audioSource.PlayOneShot(deathClip, 0.3f);
            Die();
        }

        if (PlayerInfos.pi.life > 0)
        {  
            transform.position += Vector3.left * Time.deltaTime;
            StartCoroutine("ResetInvincible");
        }
    }

    public void Die()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * 100);
        GetComponent<Collider2D>().isTrigger = true;
        Invoke("RestartLevel", 0.8f);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    

    IEnumerator ResetInvincible()
    {
        for(int i =0; i < 10; i++)
        {
            yield return new WaitForSeconds(.3f);
            sr.enabled = !sr.enabled;
        }
        yield return new WaitForSeconds(.3f);
        sr.enabled = true;
        IsInvinsible = false;
    }

    IEnumerator RaisedFlag()
    {
        yield return new WaitForSeconds(.1f);
        while(Vector3.Distance(endPosition, flag.position) >= 0.2f)
        {
            flag.position += new Vector3(0, 10 * Time.deltaTime, 0);
            yield return new WaitForSeconds(.1f);
        }
    }

    IEnumerator WalkEnd()
    {
        yield return new WaitForSeconds(.3f);
        while(Vector3.Distance(endWalk.position, transform.position) >= 0.395f)
        {
            transform.position += Vector3.right * 3 * Time.deltaTime;
            yield return new WaitForSeconds(.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInfos.pi.GetTime() <= 0)
        {
            PlayerInfos.pi.SetLife(-3);
            TakeDamage();
        }
        
    }

    IEnumerator UpdateTime()
    {
        int currTime = PlayerInfos.pi.GetTime();
        yield return new WaitForSeconds(.5f);
        while(currTime >= 0)
        {
            PlayerInfos.pi.SetTime(-1);
            currTime --;
            yield return new WaitForSeconds(.5f);
        }
    }



}
