using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D: MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private LayerMask lm;

    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private Vector2 move;
    private bool isGrounded;
    private Transform groundCheck;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck");
    }

    // Update is called once per frame
    void Update()
    {
	// Changer l'orientation du joueur en fonction du sens de deplacement
        if (move.x < 0) sp.flipX = true;
        if (move.x > 0) sp.flipX = false;

        // Le joueur est il au sol ?
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, lm);
    }

    private void FixedUpdate()
    {
        Vector2 v = rb.linearVelocity;
        v.x = move.x * speed;
        rb.linearVelocity = v;
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if(value.isPressed && isGrounded) rb.linearVelocityY = jumpForce;
    }
}
