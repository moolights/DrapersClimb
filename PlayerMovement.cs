using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sprite;
    public Vector3 initialPos;

    [SerializeField] private float runSpeed = 40.0f;
    [SerializeField] private float horizontalMove = 0.0f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform theGround;

    private bool isGrounded = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
        sprite = GetComponent<SpriteRenderer>();
        initialPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        //rb.position = new Vector2()
    }

    // Update is called once per frame
    void Update()
    {
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed * Time.fixedDeltaTime;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(horizontalMove, jumpForce);
            isGrounded = false;
        }

        if(Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
            sprite.flipX = false;
        }

        if(Input.GetKeyDown("left") || Input.GetKeyDown("a"))
        {
            sprite.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject == theGround.gameObject)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        transform.position = initialPos;
    }
}
