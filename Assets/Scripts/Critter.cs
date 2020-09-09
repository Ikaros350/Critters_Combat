using System;
using System.Collections.Generic;
using UnityEngine;

class Critter : MonoBehaviour
{
        //criaturas
        [SerializeField] Affinity affinityArray = new Affinity();
        [SerializeField] float hp;
        [SerializeField] private float baseAttack, baseDefense, baseSpeed,
            currentAtq, currentDef, currentSpd;
        private int atqCounter, defCounter, spdCounter;

        [SerializeField] private string name, affinity;

        [SerializeField] private List<Skill> moveSet;
        public float BaseAttack { get => baseAttack; private set => baseAttack = value; }
        public float BaseDefense { get => baseDefense; private set => baseDefense = value; }
        public float BaseSpeed { get => baseSpeed; private set => baseSpeed = value; }
        public float CurrentAtq { get => currentAtq; private set => currentAtq = value; }
        public float CurrentDef { get => currentDef; private set => currentDef = value; }
        public float CurrentSpd { get => currentSpd; private set => currentSpd = value; }
        public float Hp { get => hp; private set => hp = value; }
        public string Name { get => name; private set => name = value; }
        public string Affinity { get => affinity; private set => affinity = value; }
        public List<Skill> Moveset { get => moveSet; private set => moveSet = value; }

        public void Awake()
        {
            AwakeCritter();
        }
        public void DefineCritter(string name, int baseAttack,int baseDefense,int baseSpeed,int affinitieIndex, float hp)
        {
           
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
    void AwakeCritter()
    {
        Moveset = new List<Skill>();
        System.Random selector = new System.Random();
        DefineCritter("Wun".ToString() + "-kun", selector.Next(10, 101), selector.Next(10, 101),
                                                    selector.Next(10, 51), selector.Next(0, 8), selector.Next(0, 501));
        int numSkills = selector.Next(1, 4);

        for (int j = 0; j < numSkills; j++)
        {
            int skillCondition = selector.Next(0, 2);
            if (skillCondition == 0)
            {

                AttackSkill newSkillA = new AttackSkill(j.ToString() + " power", selector.Next(0, 11), selector.Next(0, 7));
                DefineSkills(numSkills, newSkillA);
            }
            else if (skillCondition == 1)
            {
                SupportSkill newSkillS = new SupportSkill("x", selector.Next(0, 11), selector.Next(0, 3));
                DefineSkills(numSkills, newSkillS);
            }

        }
    }

    public void DefineSkills(int numSkills, Skill newSkill)
    {
            System.Random selector = new System.Random();
           
            if (numSkills > 3)
                numSkills = 3;
            if (numSkills <= 0)
                numSkills = 1;
            
            if (Moveset.Count == 0 && newSkill is AttackSkill)
            {
            Debug.Log(newSkill.Name);
            Moveset.Add(newSkill);
            }
            else if(Moveset.Count != 0 && Moveset.Count < numSkills)
            {
            Debug.Log(newSkill.Name);
            Moveset.Add(newSkill);
            }
            else if(Moveset.Count == 0 && newSkill is SupportSkill)
            {

            AttackSkill remplaceSkill = new AttackSkill("basicPunch", selector.Next(1, 11), selector.Next(0, 7));
            Debug.Log(remplaceSkill.Name);
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

