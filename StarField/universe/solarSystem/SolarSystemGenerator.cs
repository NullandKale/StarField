using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField.universe.solarSystem
{
    public class SolarSystemGenerator
    {
        public static List<SolarSystemData> generateSolarSystems(Universe universe, int maxSolarSystems)
        {
            List<SolarSystemData> systems = new List<SolarSystemData>();
            vector2 workingPos = new vector2();

            for (int i = 0; i < maxSolarSystems; i++)
            {
                Console.Write("Generating Solar System: " + i + "-" + maxSolarSystems);
                List<vector2> goodPositions = utils.getOnRadius(workingPos.x, workingPos.y, universe.data.solarSystemMinDist);
                workingPos = goodPositions[utils.getIntInRange(0, goodPositions.Count)];

                List<vector2> betterPositions = new List<vector2>();
                for (int j = 0; j < goodPositions.Count; j++)
                {
                    bool isBetter = true;
                    for (int k = 0; k < systems.Count; k++)
                    {
                        if(goodPositions[j].dist(systems[k].position) < universe.data.solarSystemMinDist)
                        {
                            isBetter = false;
                            break;
                        }
                    }
                    if(isBetter)
                    {
                        betterPositions.Add(goodPositions[j]);
                    }
                }

                if(betterPositions.Count > 0)
                {
                    workingPos = betterPositions[utils.getIntInRange(0, betterPositions.Count)];
                }

                SolarSystemData workingData = generateSolarSystemOn(workingPos);

                Console.WriteLine(" @: " + workingPos.toString() + " size: " + workingData.sun.radius + " planetCount: " + workingData.planetCount);

                systems.Add(workingData);
            }

            return systems;
        }

        public static SolarSystemData generateSolarSystemOn(vector2 workingPos)
        {
            SolarSystemData workingData = new SolarSystemData();

            workingData.position = workingPos;
            workingData.name = "Unnamed: " + workingPos.toString();
            workingData.planetCount = utils.getIntInRange(1, 12);
            workingData.objects = new List<updateableObject>();
            workingData.sun = new gameObjects.universeObjects.Sun(true, workingPos, '%', 3);

            for(int i = 0; i < workingData.planetCount; i++)
            {
                List<vector2> orbit = utils.getOnRadius(workingPos.x, workingPos.y, i * 10);
                vector2 startPos = utils.RandomValueFromList(orbit);
                workingData.objects.Add(new gameObjects.universeObjects.Planet(true, startPos, 'O', workingData.sun, orbit));
            }

            return workingData;
        }
    }
}
