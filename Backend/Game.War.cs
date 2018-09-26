using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    partial class Game
    {
        private void processBattles(Province province)
        {
            HashSet<Army> armies = province.ArmiesInProvince;
            if (armies == null)
                return;

            //sort armies by country
            Dictionary<Country, List<Army>> armiesOfCountry = new Dictionary<Country, List<Army>>();
            foreach (Army army in armies)
            {
                if (army.Size == 0 || army.BlackFlagged)
                    continue;
                if (!armiesOfCountry.ContainsKey(army.Owner))
                {
                    List<Army> armyList = new List<Army>() { army };
                    armiesOfCountry.Add(army.Owner, armyList);
                }
                else
                {
                    armiesOfCountry[army.Owner].Add(army);
                }
            }

            //find pairs of Countries in War
            List<(Country, Country)> warPairs = new List<(Country, Country)>();
            foreach (Country a in armiesOfCountry.Keys)
            {
                if (a.War.Count != 0)
                {
                    foreach (Country b in armiesOfCountry.Keys)
                    {
                        if (a.War.Contains(b))
                        {
                            if (!warPairs.Contains((b, a)))
                            {
                                warPairs.Add((a, b));
                            }
                        }
                    }
                }
            }

            if (warPairs.Count == 0)
                return;
            Random r = new Random();
            (Country countryA, Country countryB) = warPairs[r.Next(0, warPairs.Count)];
            (int lossA, int lossB) = computeBattleResults(armiesOfCountry[countryA], armiesOfCountry[countryB]);
            new Messages.Battle(province, armiesOfCountry[countryA], armiesOfCountry[countryB], lossA, lossB);
            new Messages.Battle(province, armiesOfCountry[countryB], armiesOfCountry[countryA], lossB, lossA);
        }

        const double DAMAGE_FACTOR = 1e-2;
        private (int,int) computeBattleResults(List<Army> armiesA, List<Army> armiesB)
        {
            int sizeA = 0, sizeB = 0;
            foreach (Army army in armiesA)
                sizeA += army.Size;
            foreach (Army army in armiesB)
                sizeB += army.Size;
            Random r = new Random();
            int lossA = (int)Math.Round(DAMAGE_FACTOR * (r.Next(0, 6) + r.Next(0, 6) + r.Next(0, 6)) * Math.Min(sizeA, sizeB) * Math.Log(sizeB + 1) / Math.Log(sizeA + 1) + 0.5);
            int lossB = (int)Math.Round(DAMAGE_FACTOR * (r.Next(0, 6) + r.Next(0, 6) + r.Next(0, 6)) * Math.Min(sizeA, sizeB) * Math.Log(sizeA + 1) / Math.Log(sizeB + 1) + 0.5);
            killSoldiers(armiesA, Math.Min(lossA,sizeA));
            killSoldiers(armiesB, Math.Min(lossB,sizeB));
            return (lossA, lossB);
        }

        //Kills soldiers in Army List. ammount should not be bigger than the total size of the armies
        private void killSoldiers(List<Army> armies, int amount)
        {
            for (int i = 0; amount > 0; i++)
            {
                amount -= Math.Min(amount, armies[i].Size);
                armies[i].Size -= Math.Min(amount, armies[i].Size);
            }
        }

    }
}