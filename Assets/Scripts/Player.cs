using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator an1m;
    SpriteRenderer sr;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpF = 8;
    [SerializeField] Vector2 stickDistance;
    [SerializeField] Vector2 groundDistance;
    [SerializeField] Transform foots;
    [SerializeField] LayerMask groundMask;
    bool beSticking;
    bool onGround;
    bool canStick;
    bool isRun;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an1m = GetComponent<Animator>();
        sr = GetComponent <SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Stick();
        Jump();
        Animate();
    }

    private void FixedUpdate()
    {
        //onGround = Physics2D.OverlapCircle(foots.position, groundDistance, groundMask);
        onGround = Physics2D.OverlapBox(foots.position, groundDistance, 0, groundMask);
        canStick = Physics2D.OverlapBox(transform.position, stickDistance, 0, groundMask);
    }

    void Move()
    {
        float xAxis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed*xAxis, rb.velocity.y);
        isRun = xAxis != 0;
        print(xAxis);
        if (isRun)
        {
            sr.flipX = xAxis < 0;
        }
    }

    void Jump()
    {
        if (onGround || beSticking)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpF);
            }
        }
    }

    void Stick()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canStick)
        {
            beSticking = true;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || !canStick)
        {
            beSticking = false;
            rb.gravityScale = 1;
        }
    }

    void Animate()
    {
        an1m.SetBool("isRun", isRun);
    }
}
