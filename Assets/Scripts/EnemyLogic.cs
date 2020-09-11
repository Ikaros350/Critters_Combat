using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : PlayerBase
{
    
    int state;

    protected override void Awake()
    {
        //Critters = new List<Critter>();
        PoolCritters = GameObject.FindGameObjectWithTag("Pool").GetComponent<PoolObject>();
        EquipCritters(2);

    }
    
 
 
    public int MadeAction()
    {
        state = selector.Next(1, 4);

        if (Critters[0].Moveset.Count == 3)
        {

            if (state == 1)
            {
                return 0;

            }
            if (state == 2)
            {
                return 1;

            }
            if (state == 3)
            {
                return 2;

            }

        }
        else if (Critters[0].Moveset.Count == 2)
        {

            if (state == 1)
            {
                return 0;

            }
            if (state == 2)
            {
                return 1;

            }

        }
        else
        {
            return 0;
        }

        return 0;
       
    }
}
