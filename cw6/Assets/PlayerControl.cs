using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;

    public float jumpForce;
    bool grounded;

    public Transform GroundCheck;
    public float rad;
    public LayerMask ground;

    bool isRight = true;
    SpriteRenderer sprite;

    Animator anim;
    const string IDLE_ANIM = "idle";
    const string WALK_ANIM = "walk";
    const string JUMP_ANIM = "jump";

    string CurrAnim;    



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite= GetComponent<SpriteRenderer>();
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody2D>();

        MovePlayer();
        PlayerJump();
    }

    void MovePlayer()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if(isRight && Input.GetKey(KeyCode.A))
        {
            isRight = false;
            sprite.flipX = true;
        }
        else if(!isRight && Input.GetKey(KeyCode.D))
        {
            isRight = true;
            sprite.flipX = false;
        }



        if(rb.velocity.x != 0 && rb.velocity.y ==0)
        {
            PlayAnim(WALK_ANIM);
        }
        else if(rb.velocity.x != 0 && rb.velocity.y != 0)  
        {
            PlayAnim(IDLE_ANIM);
        }
       
        
    
    }

    void PlayerJump()
    {
        grounded = Physics2D.OverlapCircle(GroundCheck.position, rad, ground);

       if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            PlayAnim(JUMP_ANIM);
        }
    }

    void PlayAnim(string NextAnim)
    {
        if(CurrAnim == NextAnim)
        {
            return;
        }
        anim.Play(NextAnim);
        CurrAnim = NextAnim;
    }

}
