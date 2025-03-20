using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    LevelManager levelManager;
    Animator animator;
    bool isCheckPoint = false;
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().enabled = false;
            SoundManager.Play("CheckPoint");
            isCheckPoint = true;
            FlagAnimation();
            StartCoroutine(FlagCoroutine());
            FlagAnimation();
            levelManager.LevelWon();
        }
    }

    IEnumerator FlagCoroutine()
    {
        yield return new WaitForSeconds(2.4f);
        isCheckPoint = false;
    }

    void FlagAnimation()
    {
        animator.SetBool("IsReachedEnd", isCheckPoint);
    }
}
