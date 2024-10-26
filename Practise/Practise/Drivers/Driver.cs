using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Practise.Drivers
{
    internal abstract class Driver
    {
        public string Name { get; }
        public double TotalTime { get; private set; }
        public Car Car { get; }
        public double FuelConsumptionPerKm { get; }
        public double Speed => (Car.Hp + Car.Tyre.Degradation) / Car.FuelAmount;

        protected Driver(string name, double fuelConsumptionPerKm, Car car)
        {
            Name = name;
            FuelConsumptionPerKm = fuelConsumptionPerKm;
            Car = car;
        }

        public void IncreaseTime(double time)
        {
            TotalTime += time;
        }
    }
}
