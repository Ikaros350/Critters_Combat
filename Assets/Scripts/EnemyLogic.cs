using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : PlayerBase
{
    bool myTurn;
    int state;

    protected override void Awake()
    {
        //Critters = new List<Critter>();
        PoolCritters = GameObject.FindGameObjectWithTag("Pool").GetComponent<PoolObject>();
        EquipCritters(2);

    }
    public bool MyTurn { get => myTurn; private set => myTurn = value; }
 
    void Update()
    {
        if(myTurn)
        {
            state = selector.Next(1, 4);
            if (state == 1)
            {
                
                myTurn = false;
            }
            if (state == 2)
            {
                
                myTurn = false;
            }
            if (state == 3)
            {
                
                myTurn = false;
            }
        }
        else
        {

        }
    }
}
