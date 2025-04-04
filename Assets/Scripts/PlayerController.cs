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
    public float speedMultiplyer = 1f;


    Vector2 vectorGravity;
    Vector2 playerStartPos;
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
        playerStartPos = transform.position;
    }

    private void OnEnable()
    {
        HealthItem.OnHealthCollect += Heal;
        SpeedBoostItem.OnSpeedCollect += SpeedBoost;
    }
    private void OnDestroy()
    {
        HealthItem.OnHealthCollect -= Heal;
        SpeedBoostItem.OnSpeedCollect -= SpeedBoost;
    }
    void Update()
    {
        if(transform.position.y < yBound)
        {
            Respawn();
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
        //player movement based on left and right arrow key
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * Time.deltaTime * speed * speedMultiplyer);

        //player run animation based on player input
        RunAnimation(horizontalInput);

        //flip the player sprite in direction of movement
        playerSprite.flipX = horizontalInput < 0 ? true : false;
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
            rb.velocity -= vectorGravity * fallMultiplyer * Time.deltaTime;  // fall on the grond smothly
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
        ReduceHealth();
        StartCoroutine(GetHit());
        PlaySound("Hit");
    }

    void ReduceHealth()
    {
        HealthManager.health--;
        if (HealthManager.health <= 0)
        {
            GameOver();
        }
    }

    void Heal()
    {
        if (HealthManager.health < 3)
        {
            HealthManager.health++;
        }
    }

    void SpeedBoost(float multiplyer)
    {
        StartCoroutine(SpeedBoostPowerup(multiplyer));
    }

    IEnumerator SpeedBoostPowerup(float multiplyer)
    {
        speedMultiplyer = multiplyer;
        yield return new WaitForSeconds(5f);
        speedMultiplyer = 1f;
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
    }

    public void Respawn()
    {
        transform.position = playerStartPos;
        ReduceHealth();
    }
    public void GameOver()
    {
        HealthManager.health = 0;
        PlaySound("GameOver");
        levelManager.DisplayGameOver();
    }

    #region Animations
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
        animator.SetBool("appear", true);
    }
    void DisappearingAnimation()
    {
        animator.SetBool("disaapear", true);
    }

    #endregion
    void PlaySound(string soundName)
    {
        SoundManager.Play(soundName);
    }
}
