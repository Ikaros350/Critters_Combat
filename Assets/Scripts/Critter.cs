using System;
using System.Collections.Generic;
using UnityEngine;


namespace TallerPractico1_CritterCombat
{
    class Critter : MonoBehaviour
    {
        //criaturas
        Affinity affinityArray;
       
        private int atqCounter, defCounter, spdCounter;

        public float CurrentAtq { get; private set; }
        public float CurrentDef { get; private set; }
        public float CurrentSpd { get; private set; }
        
        public string Name { get; private set; }
        public int BaseAttack { get; private set; } 
        public int BaseDefense { get; private set; }
        public int BaseSpeed { get; private set; }
        public string Affinity { get; private set; }

        
        public List<Skill> Moveset { get; private set; }
        public float Hp { get; private set; }
        public Critter(string name, int baseAttack,int baseDefense,int baseSpeed,int affinitieIndex, float hp)
        {
            affinityArray = new Affinity();
            Moveset = new List<Skill>();
            string[] newAffinities;
            Name = name;
            BaseAttack = baseAttack;
            BaseDefense = baseDefense;
            BaseSpeed = baseSpeed;

            CurrentDef = baseDefense;
            CurrentAtq = baseAttack;
            CurrentSpd = baseSpeed;
           //hacemos una validacion para no darle vida de zero a la criatura 
            if (hp >= 0)
                Hp = hp;
            else
                Hp = 200;

            newAffinities = affinityArray.ReturnAffinities();

             if (affinitieIndex >= newAffinities.Length)
                  Affinity = newAffinities[newAffinities.Length - 1];
              else if (affinitieIndex < 0)
                  Affinity = newAffinities[0];
              else
                  Affinity = newAffinities[affinitieIndex];
            

        }         

        public void DefineSkills(int numSkills, Skill newSkill)
        {
            System.Random selector = new System.Random();

            if (numSkills > 3)
                numSkills = 3;
            if (numSkills <= 0)
                numSkills = 1;

            if(Moveset.Count == 0 && newSkill is AttackSkill)
            {
                Moveset.Add(newSkill);
            }
            else if(Moveset.Count != 0 && Moveset.Count < numSkills)
            {
                Moveset.Add(newSkill);
            }
            else if(Moveset.Count == 0 && newSkill is SupportSkill)
            {
                AttackSkill remplaceSkill = new AttackSkill("basicPunch", selector.Next(1, 11), selector.Next(0, 7));
                Moveset.Add(remplaceSkill);
            }

            
        }

        public float AlterState(int state) //0-subida de ataque , 1 - subida de defensa , 2- disminucion velocidad
        {
            
            float baseValue = 0;
            if (state == 0 )
            {
                if (atqCounter < 3)
                {
                    atqCounter++;
                    CurrentAtq = (BaseAttack + ((BaseAttack * 0.2f)* atqCounter)) ;
                    
                }
                else if (atqCounter >= 3)
                {
                    atqCounter = 3;
                    CurrentAtq = (BaseAttack + ((BaseAttack * 0.2f) * atqCounter));

                }
            }               
            if (state == 1 )
            {
               if( defCounter < 3)
                {
                    defCounter++;
                    CurrentDef = (BaseDefense + ((BaseDefense * 0.2f) * defCounter)) ;
                    
                }else if (defCounter >= 3)
                {
                    defCounter = 3;
                    CurrentDef = (BaseDefense + ((BaseDefense * 0.2f) * defCounter)) ;
                   
                }

            }
            if (state == 2 )
            {
                if (spdCounter < 3)
                {
                    spdCounter++;
                    CurrentSpd = (BaseSpeed - ((BaseSpeed * 0.3f) * spdCounter));
                    
                }else if(spdCounter >= 3)
                {
                    spdCounter = 3;
                    CurrentSpd = (BaseSpeed - ((BaseSpeed * 0.3f) * spdCounter));
                   
                }
            }

            return baseValue;
        }
        
        public void OnHit(float currentAttack, int skillPower, float affinityMultiplier )
        {
          float DamageValue = (currentAttack + skillPower) * affinityMultiplier;

            Hp = Hp - DamageValue;
        }
    }
}
