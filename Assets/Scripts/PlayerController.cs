using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Components
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer playerSprite;
    GameManager gameManager;
    LevelManager levelManager;

    [Header("Variables")]
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float fallMultiplyer;
    [SerializeField] float speedMultiplyer = 1f;


    Vector2 vectorGravity;
    public bool isOnGround = false;
    bool isJumping;
    bool secondJump = false;
    public bool isHit = false;
    float yBound = -6f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        gameManager = GameManager.Instance;
        vectorGravity = new Vector2(0, -Physics2D.gravity.y);
        //gameManager.isGameStarted = true;
    }

    void Update()
    {
        //if(gameManager.isGameStarted)
        //{
        //    Appear();
        //    gameManager.isGameStarted = false;
        //}
        if(transform.position.y < yBound)
        {
            GameOver();
        }
        PlayerMovement();
    }

    void PlayerMovement()
    {
        PlayerRun();
        PlayerJump();

        FallAnimation();

    }

    void PlayerRun()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed * speedMultiplyer);

        RunAnimation(horizontalInput);

        if (horizontalInput < 0)
        {
            playerSprite.flipX = true;
        }
        else
        {
            playerSprite.flipX = false;
        }
    }

    void PlayerJump()
    {
        if (isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
                secondJump = true;
                isJumping = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && secondJump)
            {
                jumpForce = 5f;
                Jump();
                secondJump = false;
                isJumping = false;
                DoubleJumpAnimation(true);
            }
        }
        if (rb.velocity.y < 0)
        {
            isJumping = false;
            DoubleJumpAnimation(false);
            rb.velocity -= vectorGravity * fallMultiplyer * Time.deltaTime;
        }
        JumpAnimation();
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isOnGround = false;
        PlaySound("Jump");
    }

    public void Hit()
    {
        isHit = true;
        rb.AddForce(Vector2.left);
        HealthManager.health--;
        if (HealthManager.health <= 0 )
        {
            GameOver();
        }
        StartCoroutine(GetHit());
        PlaySound("Hit");
    }
    IEnumerator GetHit()
    {
        HitAnimation();
        yield return new WaitForSeconds(3);
        isHit = false;
        HitAnimation();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpForce = 10f;
            isOnGround = true;
        }
        if(collision.gameObject.CompareTag("Collectible"))
        {
            PlaySound("Collect");
            levelManager.AddScore(collision.gameObject.name.ToString());
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Spike"))
        {
            GameOver();
        }
        if (collision.gameObject.CompareTag("Saw"))
        {
            Hit();
        }
        if(collision.gameObject.CompareTag("Heart"))
        {
            PlaySound("Collect");
            HealthManager.health++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("SpeedBoost"))
        {
            StartCoroutine(SpeedBoost());
            Destroy(collision.gameObject);
        }
    }

    public void GameOver()
    {
        HealthManager.health = 0;
        PlaySound("GameOver");
        levelManager.DisplayGameOver();
    }

    IEnumerator SpeedBoost()
    {
        speedMultiplyer = 2f;
        yield return new WaitForSeconds(5f);
        speedMultiplyer = 1f;
    }
    void RunAnimation(float input)
    {
        animator.SetFloat("speed", Mathf.Abs(input));
    }
    void JumpAnimation()
    {
        animator.SetBool("isJumping", isJumping);
    }

    void DoubleJumpAnimation(bool doubleJump)
    {
        animator.SetBool("doubleJump", doubleJump);
    }

    void FallAnimation()
    {
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void HitAnimation()
    {
        animator.SetBool("isHit", isHit);
    }

    IEnumerator Appear()
    {
        AppearingAnimation();
        yield return new WaitForSeconds(0.5f);
        isHit = false;
        AppearingAnimation();
    }
    void AppearingAnimation()
    {
        animator.SetBool("appear", gameManager.isGameStarted);
    }
    void DisappearingAnimation()
    {
        animator.SetBool("disaapear", true);
    }
    void PlaySound(string soundName)
    {
        SoundManager.Play(soundName);
    }
}
