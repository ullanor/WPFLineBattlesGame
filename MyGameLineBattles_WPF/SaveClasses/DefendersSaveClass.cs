using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameLineBattles_WPF
{
    [Serializable]
    public class DefendersSaveClass
    {
        public string Name { get; private set; }
        public int HealthPoints { get; private set; }
        public int MyShootingSkill { get; private set; }
        public int ExperiencePoints { get; private set; }
        public DefendersSaveClass(string name,int healthPoints, int myShootingSkill, int ExperiencePoints)
        {
            Name = name;
            HealthPoints = healthPoints;
            MyShootingSkill = myShootingSkill;
            this.ExperiencePoints = ExperiencePoints;
        }
    }
    [Serializable]
    public class DefendersCannonSaveClass : DefendersSaveClass
    {
        public DefendersCannonSaveClass(string name, int healthPoints, int myShootingSkill, int ExperiencePoints) : base(name,healthPoints,myShootingSkill, ExperiencePoints)
        {
        }
    }
    [Serializable]
    public class DefendersArmySaveClass : DefendersSaveClass
    {
        public char DefenderType { get; private set; }
        public int GeneralMoraleSkill { get; private set; }
        public DefendersArmySaveClass(string name, int healthPoints, int myShootingSkill, int ExperiencePoints, char defenderType, int generalMoraleSkill) : base(name, healthPoints, myShootingSkill, ExperiencePoints)
        {
            DefenderType = defenderType;
            GeneralMoraleSkill = generalMoraleSkill;
        }
    }
}
