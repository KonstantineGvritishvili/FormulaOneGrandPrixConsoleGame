using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practise.Tyres;

namespace Practise
{
    internal class Car
    {
        public int Hp { get; }
        public double FuelAmount { get; private set; }
        public Tyre Tyre { get; private set; }
        public const double MaxFuelCapacity = 160;

        public Car(int hp, double fuelAmount, Tyre tyre)
        {
            Hp = hp;
            FuelAmount = Math.Min(fuelAmount, MaxFuelCapacity);
            Tyre = tyre;
        }

        public void Refuel(double fuel)
        {
            FuelAmount = Math.Min(FuelAmount + fuel, MaxFuelCapacity);
        }

        public void ChangeTyre(Tyre newTyre)
        {
            Tyre = newTyre;
        }

        public void ConsumeFuel(double amount)
        {
            FuelAmount -= amount;
            if (FuelAmount < 0)
            {
                throw new InvalidOperationException("Out of fuel");
            }
        }
    }

}
