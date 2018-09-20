using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarField
{
    public interface iComponent
    {
        void start();
        void stop();
        void update();
    }
}
