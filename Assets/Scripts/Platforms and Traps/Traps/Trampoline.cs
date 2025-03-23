using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Traps
{
    public float bounce = 20f;
  
    IEnumerator TrampolineAnimation()
    {
        animator.SetBool("jump", true);
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("jump", false);
    }

    protected override void TrapItem(Collision2D player)
    {
        player.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        StartCoroutine(TrampolineAnimation());
    }
}
