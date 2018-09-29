using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField.universe
{
    public class Universe
    {
        public UniverseData data;
        public SolarSystem solarSystem;

        public Universe()
        {
            data = new UniverseData(10_000, 1_000, 250);
            solarSystem = new SolarSystem(this);
        }

        public void save()
        {
            data.solarSystems = solarSystem.solarSystems;

            game.save.universe_data = data;
        }

        public void load()
        {
            data = game.save.universe_data;
        }

        public void generate()
        {
            solarSystem.generate(data.solarSystemMaxCount);
        }
    }

    [Serializable]
    public struct UniverseData
    {
        public int minPos;
        public int maxPos;
        public int solarSystemMinDist;
        public int solarSystemMaxCount;

        public Dictionary<vector2, SolarSystemData> solarSystems;

        public UniverseData(int size, int minSolarSystemDist, int maxSolarSystemCount)
        {
            minPos = -size;
            maxPos =  size;
            solarSystemMinDist = minSolarSystemDist;
            solarSystemMaxCount = maxSolarSystemCount;
            solarSystems = new Dictionary<vector2, SolarSystemData>();
        }
    }
}
