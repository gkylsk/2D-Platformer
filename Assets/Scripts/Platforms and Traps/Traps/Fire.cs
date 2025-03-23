using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Traps
{
    bool isHit;
    bool isFire;
    protected override void Start()
    {
        base.Start();
        InvokeRepeating("FireTrap", 0, 15f);
    }

    void FireTrap()
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

    protected override void TrapItem(Collision2D playerObj)
    {
        if (isHit)
        {
            playerObj.gameObject.GetComponent<PlayerController>().Hit();
        }
        if (isFire)
        {
            playerObj.gameObject.GetComponent<PlayerController>().GameOver();
        }
    }
}
