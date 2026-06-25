using UnityEngine;
using UnityEngine.UIElements;

public class EnnemiMove : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sr;
    public Collider2D colliderToDestroy;
    public Transform LeftPoint;
    Vector3 LP;
    Vector3 RP;
    AudioSource audioSource;
    public Transform RightPoint;
    Vector3 GoTo;
    public float Speed;
    bool IsLife = true;
    public AudioClip killClip;
    public GameObject killEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        LP = LeftPoint.position;
        RP = RightPoint.position;
        GoTo = LP;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("IsLife", false);
            IsLife = false;
            Destroy(colliderToDestroy);
            BoxCollider2D box = GetComponent<BoxCollider2D>();
            box.enabled=false;
            audioSource.PlayOneShot(killClip, 0.3f);
            GameObject go = Instantiate(killEffect, transform.position + new Vector3 (0, 0, 0),Quaternion.identity);
            Destroy(go, 0.3f);
            PlayerInfos.pi.SetScore(500);
            Destroy(gameObject, .3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLife)
        {
            if(GoTo == LP)
            {
                transform.position -= new Vector3(Speed * Time.deltaTime, 0, 0);
                if (Vector3.Distance(GoTo, transform.position) <= 0.2f)
                {
                    GoTo = RP;
                    flip();
                }
            }

            if (GoTo == RP)
            {
                transform.position += new Vector3 (Speed * Time.deltaTime, 0, 0);
                if (Vector3.Distance(GoTo, transform.position) <= 0.1f)
                {
                    GoTo = LP;
                    flip();
                }
            }
        }
    }

    void flip()
    {
        if (GoTo == LP)
            sr.flipX = false;

        if (GoTo == RP)
            sr.flipX = true;
    }
}
