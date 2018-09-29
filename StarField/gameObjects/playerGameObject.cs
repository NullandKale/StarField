using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField.gameObjects
{
    [Serializable]
    public class playerGameObject : updateableObject
    {

        public playerGameObject(vector2 pos) : base(pos)
        {
            toDisplay = '^';
        }
    }
}
