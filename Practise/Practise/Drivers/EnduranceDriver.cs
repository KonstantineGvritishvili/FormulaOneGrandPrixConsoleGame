using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practise.Drivers
{
    internal class EnduranceDriver : Driver
    {
        public EnduranceDriver(string name, Car car) : base(name, 1.5, car)
        {
        }
    }
}
