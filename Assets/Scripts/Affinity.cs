using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerPractico1_CritterCombat
{
    class Affinity
    {
        private string[] affinities;

           

        public Affinity()
        {
            affinities = new string[] { "Fire", "Wind", "Water", "Earth", "Dark", "Light" } ;
  
        }
        public string[] ReturnAffinities()
        { 
            return affinities;
        }

       public float AfinityTable(string affinityAtck, string affinityDef)
        {
            float affinityMultiplier = 1;

            if(affinityAtck == affinityDef)
                affinityMultiplier = 0.5f;


            switch (affinityAtck)
            {
                case "Dark":
                    if(affinityDef.Equals("Light"))
                        affinityMultiplier = 2;
                    break;

                case "Light":
                    if (affinityDef.Equals("Dark"))
                        affinityMultiplier = 2;
                    break;

                case "Fire":
                    if(affinityDef.Equals("Water"))
                         affinityMultiplier = 2;
                   if(affinityDef.Equals("Earth"))
                         affinityMultiplier = 0f;
                    break;

                case "Water":
                    if(affinityDef.Equals("Fire"))
                        affinityMultiplier = 0.5f;
                    if(affinityDef.Equals("Wind"))
                        affinityMultiplier = 2;
                    break;

                case "Wind":
                    if(affinityDef.Equals("Water"))
                        affinityMultiplier = 0.5f;
                    if(affinityDef.Equals("Earth"))
                        affinityMultiplier = 0.5f;
                    break;
                case "Earth":
                    if(affinityDef.Equals("Wind"))
                        affinityMultiplier = 2;
                    break;
            }

       

            return affinityMultiplier;
        } //Tabla de multiplicadores de las afinidades
    }
}
