using System;
using System.Collections.Generic;
using UnityEngine;


 public class Player : PlayerBase
{
    //jugador
    protected override void Awake()
    {
        //Critters = new List<Critter>();
        PoolCritters = GameObject.FindGameObjectWithTag("Pool").GetComponent<PoolObject>();
        EquipCritters(3);  
    }

}

