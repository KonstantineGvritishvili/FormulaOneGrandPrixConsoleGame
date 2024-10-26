using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practise.Tyres
{
    internal abstract class Tyre
    {
        public string Name { get; }
        public double Hardness { get; }
        public double Degradation { get; set; }

        protected Tyre(string name, double hardness)
        {
            Name = name;
            Hardness = hardness;
            Degradation = 100.0;
        }

        public virtual void Degrade()
        {
            Degradation -= Hardness;
            if (Degradation < 0)
            {
                throw new InvalidOperationException("Blown Tyre");
            }
        }
    }
}
