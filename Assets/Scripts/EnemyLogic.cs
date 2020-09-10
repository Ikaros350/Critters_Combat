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
    
 
 
    public Skill MadeAction()
    {
        state = selector.Next(1, 4);

        if (Critters[0].Moveset.Count == 3)
        {
           
            if (state == 1)
            {
                return Critters[0].Moveset[0];

            }
            if (state == 2)
            {
                return Critters[0].Moveset[1];

            }
            if (state == 3)
            {
                return Critters[0].Moveset[2];

            }

        }
       else if (Critters[0].Moveset.Count == 2)
        {

            if (state == 1)
            {
                return Critters[0].Moveset[0];

            }
            if (state == 2)
            {
                return Critters[0].Moveset[1];

            }

        }
        else
        {
            return Critters[0].Moveset[0];
        }

        return Critters[0].Moveset[0];
       
    }
}
