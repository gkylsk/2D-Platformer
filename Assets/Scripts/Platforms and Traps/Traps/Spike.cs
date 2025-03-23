using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Traps
{
    protected override void TrapItem(Collision2D playerObj)
    {
        playerObj.gameObject.GetComponent<PlayerController>().GameOver();
    }
}
