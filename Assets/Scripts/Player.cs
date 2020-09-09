using System;
using System.Collections.Generic;
using UnityEngine;


 class Player : MonoBehaviour,IPlayer
{
        //jugador
    [SerializeField] private List<Critter> critters;
    

    internal List<Critter> Critters { get => critters; set => critters = value; }
    private void Start()
    {
      //Critters = new List<Critter>();
       EquipCritters();
       
    }
    public void EquipCritters(int numCritters = 1)
    {
        System.Random selector = new System.Random();

         if (numCritters > 3)
             numCritters = 3;
         if (numCritters <= 0)
              numCritters = 1;
           

         //Aqui equipamos los critters presentes en la Pool

         

    }

        
       
}

