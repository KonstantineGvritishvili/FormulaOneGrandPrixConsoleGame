using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static void Main(string[] args)
    {
        RaceTower raceTower = new RaceTower();
        raceTower.SetTrackInfo(10, 5000);

        raceTower.RegisterDriver("Aggressive", "FirstDriver", 200, 100, "Ultrasoft", 30, 5);
        raceTower.RegisterDriver("Endurance", "SecondDriver", 180, 110, "Hard", 50, 3);
        raceTower.RegisterDriver("Endurance", "ThirdDriver", 190, 120, "Hard", 40, 4);

        raceTower.CompleteLaps(new List<string> { "10" });
        raceTower.PrintFinalResults();
    }

}