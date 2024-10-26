using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practise.Drivers
{
    internal class AggressiveDriver : Driver
    {
        public AggressiveDriver(string name, Car car) : base(name, 2.7, car)
        {
        }

        public double Speed => base.Speed * 1.3f;
    }

}


