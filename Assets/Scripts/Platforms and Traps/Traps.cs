using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    Animator animator;
    public float bounce = 20f;
    [SerializeField] bool isFireTrap = false;
    bool isHit;
    bool isFire;
    void Start()
    {
        animator = GetComponent<Animator>();
        if(isFireTrap)
        {
            InvokeRepeating("Fire", 0, 15f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(gameObject.CompareTag("Trampoline"))
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
                StartCoroutine(TrampolineAnimation());
            }
            if(gameObject.CompareTag("Fire"))
            {
                if(isHit)
                {
                    collision.gameObject.GetComponent<PlayerController>().Hit();
                }
                if(isFire)
                {
                    collision.gameObject.GetComponent<PlayerController>().GameOver();
                }
            }
        }
    }

    IEnumerator TrampolineAnimation()
    {
        animator.SetBool("jump", true);
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("jump", false);
    }

    void Fire()
    {
        StartCoroutine(FireAnimation());
    }

    IEnumerator FireAnimation()
    {
        yield return new WaitForSeconds(5f);
        isHit = true;
        animator.SetBool("hit", isHit);
        yield return new WaitForSeconds(5f);
        isHit = false;
        animator.SetBool("hit", isHit);
        isFire = true;
        animator.SetBool("on", isFire);
        yield return new WaitForSeconds(5f);
        isFire = false;
        animator.SetBool("on", isFire);
    }
}
