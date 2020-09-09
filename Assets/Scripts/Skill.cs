﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


  public  abstract class Skill
    {
        //ataques
        //atributos-------------
        public string Name { get; protected set; }
        public int Power { get; protected set; }

      public Skill(string name, int power)
        {
            Name = name;
            Power = power;
        }
    }

