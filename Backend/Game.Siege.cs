using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    partial class Game
    {
        const int MIN_ARMY_SIZE_FOR_SIEGE = 1000;
        const double MIN_ENEMY_OWN_SOLDIER_RATIO_TO_STOP_SIEGE = 0.1;
        const double TIME_NEEDED_FOR_SIEGE = 100;
        void processSiege(Province province)
        {

            if (province.Siege != null)
            {
                Country besieger = province.Siege.Besieger;
                int besiegingArmySize = 0;
                int enemyArmySize = 0;

                //count soldiers
                foreach (Army army in province.ArmiesInProvince)
                {
                    if (!army.BlackFlagged)
                    {
                        if (army.Owner == besieger)
                        {
                            besiegingArmySize += army.Size;
                            if (besiegingArmySize >= MIN_ARMY_SIZE_FOR_SIEGE)
                            {
                                break;
                            }
                        }
                        else if (besieger.War.Contains( army.Owner ))
                        {
                            enemyArmySize += army.Size;
                        }
                    }
                }

                //army still big enough?
                if (besiegingArmySize >= MIN_ARMY_SIZE_FOR_SIEGE)
                {
                    //too many enemies to keep sieging?
                    if ((double)enemyArmySize / besiegingArmySize > MIN_ENEMY_OWN_SOLDIER_RATIO_TO_STOP_SIEGE)
                    {
                        return;
                    }

                    province.Siege.AddTime(1);

                    //Siege finished?
                    if (province.Siege.Duration >= TIME_NEEDED_FOR_SIEGE)
                    {
                        province.Owner = besieger;
                        province.Siege = null;
                    }
                    else
                    {
                        return;
                    }
                }
                else //not enough soldiers to keep sieging
                {
                    province.Siege = null;
                }
            }

            //new besiegers waiting? Count Soldiers
            Dictionary<Country, int> soldiersOfCountry = new Dictionary<Country, int>();
            foreach (Army army in province.ArmiesInProvince)
            {
                if (!army.BlackFlagged && province.Owner.War.Contains(army.Owner))
                {
                    if (soldiersOfCountry.ContainsKey(army.Owner))
                    {
                        soldiersOfCountry[army.Owner] += army.Size;
                    }
                    else
                    {
                        soldiersOfCountry[army.Owner] = army.Size;
                    }
                }
            }

            //find biggest army
            KeyValuePair<Country, int> maxPair = new KeyValuePair<Country, int>(null,0);
            foreach (KeyValuePair<Country,int> pair in soldiersOfCountry)
            {
                if (pair.Value > maxPair.Value)
                {
                    maxPair = pair;
                }
            }

            if (maxPair.Value > MIN_ARMY_SIZE_FOR_SIEGE)
            {
                province.Siege = new Siege(maxPair.Key, province);
            }

        }
    }
}
