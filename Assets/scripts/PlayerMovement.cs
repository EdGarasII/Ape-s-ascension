using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D col;
    [SerializeField] private LayerMask jumpableGround;
    private SpriteRenderer sprite;
    private Animator anim;
    private float DirX = 0f;
    [SerializeField] private float MoveSpeed = 7f;
    [SerializeField] private float JumpSpeed = 14f;
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
    private void Update()
    {
        DirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (DirX * MoveSpeed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
           rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
        }

        MovementAnimation();
    }
    private void MovementAnimation()
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
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }
    public void GoToHome()
    {
        SceneManager.LoadScene("Menu");
    }
}
