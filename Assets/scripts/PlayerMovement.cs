using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private BoxCollider2D col;
    [SerializeField] private LayerMask jumpableGround;
    private SpriteRenderer sprite;
    private Animator anim;
    public float DirX = 0f;
    [SerializeField] public float MoveSpeed = 7f;
    [SerializeField] public float JumpSpeed = 14f;
    public GameObject pauseMenuScreen;

    private enum movementState { idle, run, jump, fall }

        
       

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    public void Update()
    {
        Move();
        Jump();
        MovementAnimation();
    }
    public void Move()
    {
        DirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(DirX * MoveSpeed, rb.velocity.y);
    }
    
    public void Jump() 
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
        }
    }
    public void MovementAnimation()
    {
        movementState state;
        if (DirX > 0f)
        {
            state = movementState.run;
            sprite.flipX = false;
        }
        else if (DirX < 0f)
        {
            state = movementState.run;
            sprite.flipX = true;
        }
        else
        {
            state = movementState.idle;
        }
        if(rb.velocity.y > .1f)
        {
            state = movementState.jump;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = movementState.fall;
        }
        anim.SetInteger("state", (int)state);
    }
    public bool IsGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    
}
