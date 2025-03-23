using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : Traps
{
    protected override void TrapItem(Collision2D playerObj)
    {
        playerObj.gameObject.GetComponent<PlayerController>().Hit();
    }
}
