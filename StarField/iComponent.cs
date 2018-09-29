using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField
{
    public interface iComponent
    {
        void start(updateableObject gameObject);
        void stop(updateableObject gameObject);
        void update(updateableObject gameObject);
    }
}
