using System;
using System.Collections.Generic;
using System.Text;

namespace LB7_Zoo
{
        public class Kangaroo : Animal
        {
        public Kangaroo(string name, double food, bool breed, bool social)
    : base(name, food, "Трава/Овочі", breed, social)
        {
            Species = "Кенгуру";
        }

        public override string GetHousingRequirements() => "Відкрите поле з навісом, можливість проживання групами.";
        }
    }

