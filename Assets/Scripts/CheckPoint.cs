using System.Collections;
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
            //disable player movement
            collision.gameObject.GetComponent<PlayerController>().enabled = false;

            SoundManager.Play("CheckPoint");
            InitiateAnimation();

            levelManager.LevelWon();
        }
    }

    IEnumerator FlagCoroutine()
    {
        yield return new WaitForSeconds(2.4f);
        isCheckPoint = false;
    }

    void InitiateAnimation()
    {
        isCheckPoint = true;
        //flag animation for checkpoint
        FlagAnimation();
        StartCoroutine(FlagCoroutine());
        FlagAnimation();
    }
    void FlagAnimation()
    {
        animator.SetBool("IsReachedEnd", isCheckPoint);
    }
}
