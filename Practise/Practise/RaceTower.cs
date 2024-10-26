using Practise;
using Practise.Drivers;
using Practise.Tyres;
using System;
using System.Collections.Generic;
using System.Linq;

internal class RaceTower
{
    private int totalLaps;
    private int trackLength;
    private int currentLap;
    private List<Driver> drivers = new List<Driver>();
    private string weather = "Sunny";

    public void SetTrackInfo(int lapsNumber, int trackLength)
    {
        totalLaps = lapsNumber;
        this.trackLength = trackLength;
    }


    public void RegisterDriver(string driverType, string name, int horsePower, double fuelAmount, string tyreType, double tyreHardness, double tyreGrip)
    {
        try
        {
            Console.WriteLine($"\nRegistering driver: {name}, Type: {driverType}, Car HP: {horsePower}, Fuel: {fuelAmount}, Tyre: {tyreType}");

            Tyre tyre;
            if (tyreType == "Ultrasoft")
            {
                tyre = new UltrasoftTyre(tyreHardness, tyreGrip);
                Console.WriteLine($"Created UltrasoftTyre with hardness: {tyreHardness} and grip: {tyreGrip}");
            }
            else if (tyreType == "Hard")
            {
                tyre = new HardTyre(tyreHardness);
                Console.WriteLine($"Created HardTyre with hardness: {tyreHardness} and grip: {tyreGrip}");
            }
            else
            {
                throw new ArgumentException($"Invalid tyre type: {tyreType}");
            }

            Car car = new Car(horsePower, fuelAmount, tyre);
            Console.WriteLine($"Created Car with horsepower: {horsePower} and fuel amount: {fuelAmount}");

            Driver driver;
            if (driverType == "Aggressive")
            {
                driver = new AggressiveDriver(name, car);
                Console.WriteLine($"Created AggressiveDriver named {name}");
            }
            else if (driverType == "Endurance")
            {
                driver = new EnduranceDriver(name, car);
                Console.WriteLine($"Created EnduranceDriver named {name}");
            }
            else
            {
                throw new ArgumentException($"Invalid driver type: {driverType}\n");
            }

            drivers.Add(driver);
            Console.WriteLine($"Driver {name} successfully registered.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to register driver: {ex.Message}");
        }
    }


    public void DriverBoxes(List<string> commandArgs)
    {
        string reasonToBox = commandArgs[0];
        string driverName = commandArgs[1];

        Driver driver = drivers.FirstOrDefault(d => d.Name == driverName);
        if (driver == null) return;

        driver.IncreaseTime(20); 

        if (reasonToBox == "Refuel")
        {
            double fuelAmount = double.Parse(commandArgs[2]);
            driver.Car.Refuel(fuelAmount);
        }
        else if (reasonToBox == "ChangeTyres")
        {
            string tyreType = commandArgs[2];
            double tyreHardness = double.Parse(commandArgs[3]);
            Tyre newTyre;

            if (tyreType == "Ultrasoft")
            {
                double grip = double.Parse(commandArgs[4]);
                newTyre = new UltrasoftTyre(tyreHardness, grip);
            }
            else
            {
                newTyre = new HardTyre(tyreHardness);
            }

            driver.Car.ChangeTyre(newTyre); 
        }
    }

    private void HandleOvertaking()
    {
        drivers = drivers.OrderBy(d => d.TotalTime).ToList();

        for (int j = 0; j < drivers.Count - 1; j++)
        {
            var currentDriver = drivers[j];
            var nextDriver = drivers[j + 1];

            if (currentDriver.TotalTime + 2 < nextDriver.TotalTime)
            {
                Console.WriteLine($"\n{currentDriver.Name} has overtaken {nextDriver.Name} on lap {currentLap}");
                var temp = drivers[j];
                drivers[j] = drivers[j + 1];
                drivers[j + 1] = temp;
            }
        }
    }

    private void PrintCurrentStandings()
    {
        for (int i = 0; i < drivers.Count; i++)
        {
            var driver = drivers[i];
            if (driver.Car.Tyre.Degradation < 30)
            {
                Console.WriteLine($"{driver.Name} Blown Tyre");
            }
            else
            {
                Console.WriteLine($"{i + 1} {driver.Name} {driver.TotalTime:F3}");
            }
        }
    }

    public void PrintFinalResults()
    {
        if (currentLap == totalLaps)
        {
            var winner = drivers.OrderBy(d => d.TotalTime).First();
            Console.WriteLine($"{winner.Name} wins the race for {winner.TotalTime:F3} seconds.");
        }
    }


    public string CompleteLaps(List<string> commandArgs)
    {
        int numberOfLaps = int.Parse(commandArgs[0]);
        Console.WriteLine($"\nAttempting to complete {numberOfLaps} laps.");

        if (currentLap + numberOfLaps > totalLaps)
        {
            Console.WriteLine($"There is no time! On lap {currentLap}.");
            return $"There is no time! On lap {currentLap}.";
        }

        for (int i = 0; i < numberOfLaps; i++)
        {
            currentLap++;
            Console.WriteLine($"Lap {currentLap}/{totalLaps} started.");

            foreach (var driver in drivers.ToList())
            {
                try
                {
                    double timeIncrease = 60.0 / (trackLength / driver.Speed);
                    driver.IncreaseTime(timeIncrease);
                    Console.WriteLine($"{driver.Name} time increased by {timeIncrease}.");

                    double fuelNeeded = trackLength * driver.FuelConsumptionPerKm;
                    driver.Car.ConsumeFuel(fuelNeeded);
                    Console.WriteLine($"{driver.Name} consumed {fuelNeeded} fuel.");

                    driver.Car.Tyre.Degrade();
                    Console.WriteLine($"{driver.Name}'s tyre degraded.");
                }
                catch (InvalidOperationException ex)
                {
                    if (ex.Message == "Blown Tyre")
                    {
                        Console.WriteLine($"{driver.Name} has a blown tyre.");
                    }
                    drivers.Remove(driver);
                }
            }

            HandleOvertaking();
            PrintCurrentStandings();
        }

        Console.WriteLine("Completed all requested laps.\n");
        return $"Completed {numberOfLaps} laps.";
    }


    public string GetLeaderboard()
    {
        var leaderboard = $"Lap {currentLap}/{totalLaps}\n";
        var sortedDrivers = drivers.OrderBy(d => d.TotalTime).ToList();

        for (int i = 0; i < sortedDrivers.Count; i++)
        {
            var driver = sortedDrivers[i];
            leaderboard += $"{i + 1} {driver.Name} {driver.TotalTime:F3}\n";
        }

        return leaderboard;
    }

    public void ChangeWeather(List<string> commandArgs)
    {
        weather = commandArgs[0];
    }


}