using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpingPower;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public SpriteRenderer blocMove;
    public AudioClip walkClip;
    public AudioClip jumpClip;
    private float horizontal;
    private SpriteRenderer sr;
    private AudioSource audioSource;
    bool canPlaySound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        blocMove.enabled = false;
        canPlaySound = true;
    }

    void FixedUpdate()
    {
        if (!PlayerInfos.pi.GetEndLevel())
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
        if (horizontal != 0f)
        {
            if (canPlaySound)
            {
                canPlaySound = false;
                audioSource.PlayOneShot(walkClip, 0.3f);
                Invoke("canPlay", 0.2f);
            }
        }
    }

    void canPlay()
    {
        canPlaySound = true;
    }

    public void Move (InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x ;
        sr.flipX = horizontal < 0 ? true : false;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded()&&!PlayerInfos.pi.GetEndLevel())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            audioSource.PlayOneShot(jumpClip, 0.3f);
            if (canPlaySound)
            {
                canPlaySound = false;
                audioSource.PlayOneShot(walkClip, 0.2f);
                Invoke("canPlay", 0.02f);
            }
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(.25f, .1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
    
}
