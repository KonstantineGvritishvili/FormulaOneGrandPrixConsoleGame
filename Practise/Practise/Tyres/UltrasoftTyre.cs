using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practise.Tyres
{
    internal class UltrasoftTyre : Tyre
    {
        public double Grip { get; }

        public UltrasoftTyre(double hardness, double grip) : base("Ultrasoft", hardness)
        {
            Grip = grip;
        }

        public override void Degrade()
        {
            Degradation -= Hardness + Grip;
            if (Degradation < 30)
            {
                throw new InvalidOperationException("Blown Tyre");
            }
        }
    }
}
