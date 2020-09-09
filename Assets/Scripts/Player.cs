using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace TallerPractico1_CritterCombat
{
    class Player
    {
        //jugador
        public List<Critter> Critters { get; private set; }
       
        public Player()
        {
            Critters = new List<Critter>();
        }
        public void EquipCritters(int numCritters = 1)
        {
            Random selector = new Random();

            if (numCritters > 3)
                numCritters = 3;
            if (numCritters <= 0)
                numCritters = 1;
            //aqui rellenos los criters de base
            for (int i = 0; i < numCritters; i++)
            {
                Critter newCritter = new Critter(i.ToString() + "-kun", selector.Next(10, 101), selector.Next(10, 101),
                                                    selector.Next(10, 51), selector.Next(0, 8), selector.Next(0, 501));
                Critters.Add(newCritter);

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
                        AttackSkill newSkill = new AttackSkill(j.ToString() + " power", selector.Next(0, 11), selector.Next(0,7));
                        Critters[i].DefineSkills(numSkills, newSkill);
                    }
                    else if (skillCondition == 1)
                    {
                        SupportSkill newSkill = new SupportSkill("x", selector.Next(0, 11),selector.Next(0,3));
                        Critters[i].DefineSkills(numSkills, newSkill);
                    }

                }
            }

        }

        
       
    }
}
