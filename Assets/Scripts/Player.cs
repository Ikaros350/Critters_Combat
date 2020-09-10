using System;
using System.Collections.Generic;
using UnityEngine;


 public class Player : MonoBehaviour,IPlayer
{
    //jugador
    
    [SerializeField] private List<GameObject> critters;
    PoolObject poolCritters;

    public List<GameObject> Critters { get => critters;  private set => critters = value; }
    private void Start()
    {
        //Critters = new List<Critter>();
        poolCritters = GameObject.FindGameObjectWithTag("Pool").GetComponent<PoolObject>();
        EquipCritters(3);
       
    }
    public void EquipCritters(int numCritters = 1)
    {
        System.Random selector = new System.Random();

         if (numCritters > 3)
             numCritters = 3;
         if (numCritters <= 0)
              numCritters = 1;


        //Aqui equipamos los critters presentes en la Pool
        for (int i = 0; i < numCritters; i++)
        {
            GameObject clone = poolCritters.GetItem(transform.position);
            //clone.transform.eulerAngles = transform.eulerAngles;
            critters.Add(clone);
            critters[i].transform.parent = gameObject.transform;
        }
        for(int i = 0; i < numCritters; i++)
        {
            if (i != 0)
            {
                critters[i].SetActive(false);
            }
        }
        
    }

        
       
}

