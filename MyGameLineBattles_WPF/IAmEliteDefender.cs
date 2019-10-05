using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameLineBattles_WPF
{
    interface IAmGeneralDefender
    {
        bool IncreaseDefenderMorale();
        void SetMyRealPicture();
    }
    interface IAmEliteDefender
    {
        void SetMyRealPicture();
    }
    interface IAmRecruitDefender
    {

    }
    interface IAmEliteEnemy
    {
        void SetMyRealPicture();
    }

    public enum DifficultyLevel
    {
        Easy = 0,
        Normal = 1,
        Hard = 2,
        Brutal = 3
    }

    public enum DefenderPrice
    {
        Recruit = 50,
        Normal = 200,
        Elite = 500,
        General = 600,
        Cannon = 750
    }

    public enum LocationName
    {
        BlueCity = 0,
        Plains = 1,
        Grassland = 2,
        RedCity = 3,
        Hills = 4,
        Mountains = 5,
        OrcCity = 6,
        Plateau = 7,
        Desert = 8,
        RedOutpost = 9,
        DarkSands = 10,
        DemonLair = 11
    }
    public enum DefenderName
    {
        Atos = 0,
        Portos = 1,
        Aramis = 2,
        Garond = 3,
        Hagen = 4,
        Albert = 5,
        Richard = 6,
        Olgierd = 7,
        Diego = 8,
        Bruno = 9,
        Walium = 10,
        Olaf = 11,
        Peter = 12,
        Hans = 13,
        Bard = 14,
        Sasza = 15,
        Pablo = 16,
        Travis = 17,
        Marco = 18,
        Sebastian = 19,
        Evan = 20,
        Allan = 21,
        Artur = 22,
        Pedro = 23,
        Ralf = 24        
    }
}
