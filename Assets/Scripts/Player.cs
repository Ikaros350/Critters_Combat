using System;
using System.Collections.Generic;
using UnityEngine;


    class Player : MonoBehaviour
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
            //aqui rellenos los criters de base

            for (int i = 0; i < numCritters; i++)
            {
                /*Critter newCritter = new Critter(i.ToString() + "-kun", selector.Next(10, 101), selector.Next(10, 101),
                                                    selector.Next(10, 51), selector.Next(0, 8), selector.Next(0, 501));
                
                Critters.Add(newCritter);
                 */
                Critters[i].DefineCritter(i.ToString() + "-kun", selector.Next(10, 101), selector.Next(10, 101),
                                                    selector.Next(10, 51), selector.Next(0, 8), selector.Next(0, 501));
            }

            //aqui le damos las skils a cada criter
            for (int i = 0; i < Critters.Count; i++)
            {
                int numSkills = selector.Next(1, 4);

                for (int j = 0; j < numSkills; j++)
                {
                    int skillCondition = selector.Next(0, 2);
                    if (skillCondition == 0) 
                    {
                    Debug.Log("Entre aqui");
                        AttackSkill newSkillA = new AttackSkill(j.ToString() + " power", selector.Next(0, 11), selector.Next(0,7));
                        Critters[i].DefineSkills(numSkills, newSkillA);
                    }
                    else if (skillCondition == 1)
                    {
                        SupportSkill newSkillS = new SupportSkill("x", selector.Next(0, 11),selector.Next(0,3));
                        Critters[i].DefineSkills(numSkills, newSkillS);
                    }

                }
            }

        }

        
       
    }

