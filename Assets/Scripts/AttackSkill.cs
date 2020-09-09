using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class AttackSkill:Skill
    {
        Affinity affinityArray;
        string[] newAffinities;
        //tecnicas de daño
        public string MyAffinity { get; private set; }
   
        public AttackSkill(string name, int power, int affinityIndex) : base( name, power)
        {
            if (power > 0 && power <= 10)
                Power = power;
            else if (power <= 0)
                Power = 1;
            else if (power > 10)
                Power = 10;

            affinityArray = new Affinity();

            newAffinities = affinityArray.ReturnAffinities();

            if (affinityIndex >= newAffinities.Length)
                MyAffinity = newAffinities[newAffinities.Length - 1];
            else if (affinityIndex < 0)
                MyAffinity = newAffinities[0];
            else
                MyAffinity = newAffinities[affinityIndex];
        }

      

    }

