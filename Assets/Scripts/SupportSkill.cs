
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class SupportSkill:Skill
    {
        //tecnicas de support

        public SupportSkill(string name , int power, int indexName):base(name,power)
        {
            Power = 0;
           if (indexName == 0)
                Name = "AtkUp"; 
          else if (indexName == 1)
                Name = "DefUp";
          else if (indexName == 2)
                Name = "SpdDwn";
          else 
                Name = "AtkUp";
        }
    }

