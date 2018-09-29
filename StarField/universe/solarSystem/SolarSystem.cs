using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField.universe
{
    public class SolarSystem
    {
        public Universe universe;
        public Dictionary<vector2, SolarSystemData> solarSystems;

        public SolarSystem(Universe universe)
        {
            this.universe = universe;
            solarSystems = new Dictionary<vector2, SolarSystemData>();
        }

        public void save()
        {
            universe.data.solarSystems = solarSystems;
        }

        public void load(Dictionary<vector2, SolarSystemData> solarSystems)
        {
            this.solarSystems = solarSystems;
        }

        public void generate(int count)
        {
            List<SolarSystemData> solarSystemList = solarSystem.SolarSystemGenerator.generateSolarSystems(universe, universe.data.solarSystemMaxCount);

            for(int i = 0; i < solarSystemList.Count; i++)
            {
                solarSystems.Add(solarSystemList[i].position, solarSystemList[i]);
            }
        }
    }

    [Serializable]
    public struct SolarSystemData
    {
        public string name;
        public vector2 position;
        public List<updateableObject> objects;
    }

}
