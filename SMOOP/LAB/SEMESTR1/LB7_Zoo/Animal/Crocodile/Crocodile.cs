using System;
using System.Collections.Generic;
using System.Text;

namespace LB7_Zoo
{
        public class Crocodile : Animal
        {
        public Crocodile(string name, double food, bool breed, bool social)
    : base(name, food, "Риба/М'ясо", breed, social)
        {
            Species = "Крокодил";
        }

        public override string GetHousingRequirements() => "Тераріум з підігрівом води та береговою зоною.";
        }
    }

