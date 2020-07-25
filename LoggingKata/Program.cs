using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Security.Permissions;
using System.Runtime.CompilerServices;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells in Alabama that are the furthest from one another.

            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath); 

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable locA = null; //The Two ITrackable variables.
            ITrackable locB = null;
            double distance = 0; //The var to store distance and the max distance.
            double maxDistance = 0;

            for (int i = 0; i < locations.Length; i++ ) //The first for loop to run through the locations Array created above.
            {
                GeoCoordinate cordA = new GeoCoordinate 
                {
                    Latitude = locations[i].Location.Latitude,
                    Longitude = locations[i].Location.Longitude
                }; //this loop chooses the location for the first TacoBell.
                

                for (int j = 0; j < locations.Length; j++) //This loop runs through the locations Array again.
                {
                    GeoCoordinate cordB = new GeoCoordinate 
                    {
                        Latitude = locations[j].Location.Latitude,
                        Longitude = locations[j].Location.Longitude
                    }; //this selects the location of the second TacoBell
                    

                    distance = cordA.GetDistanceTo(cordB); //this sets distance to the method GetDistanceTo
                    //It finds the distance between the two locations A and B.

                    if (maxDistance < distance) //this tests to see if distance is the max distance.
                    {
                        locA = locations[i]; //if it is, it changes the locations to the onse selcted above.
                        locB = locations[j];
                        maxDistance = distance; // and changes max distance to the new distance, if farther.
                    }
                }
            }

            Console.WriteLine($"The two locations are {locA.Name} and {locB.Name}. The Distamce between these locations is {Math.Ceiling(maxDistance)}");
            //Here we print the result to the Console.
            
        }
    }
}
