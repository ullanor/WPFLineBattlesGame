using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyGameLineBattles_WPF
{
    public class Player
    {
        public int PlayerWarbandMorale { get; private set; }
        public int PlayerLeaderSkill { get; private set; }
        private string name;
        public string Name { get { return name; } }
        private int money;
        public int PlayerMoney { get { return money; } }

        public Player(string name, int money)
        {
            this.name = name;
            this.money = money;
            PlayerWarbandMorale = 50;
        }

        public void ChangePlayerWarbandMorale(int amount)
        {
            PlayerWarbandMorale = amount;
        }
        public void IncreasePlayerLeaderSkill(int amount)
        {
            PlayerLeaderSkill += amount;
        }
        public void PayFor(int amount)
        {
            money -= amount;
        }
        public void GetPaid(int amount)
        {
            money += amount;
        }
        public void ChangePlayerName(string name)
        {
            this.name = name;
        }
        public void SetMoneyAfterLoad(int loadedAmount)
        {
            money = loadedAmount;
        }

        

    } 
}
