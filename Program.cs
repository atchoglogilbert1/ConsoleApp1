using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

class Program
{
    // Function to convert CSV data -> Dictionary<int, int>
    public static Dictionary<int, int> Read(string filename, bool log = false)
    {
        try
        {
            string[] rows = File.ReadAllLines(filename);
            Dictionary<int, int> data = new Dictionary<int, int>();

            foreach (string row in rows.Skip(1))
            {
                string[] explosion = row.Split(',');

                data[int.Parse(explosion[0])] = int.Parse(explosion[1]);

                if (log)
                {
                    Console.WriteLine(explosion[0] + ":" + explosion[1]);
                }
            }

            return data;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Failed to find file: " + filename);
            return new Dictionary<int, int>();
        }
    }


    // Calculates the intercept:
    // β̂₀ = Ȳ - β̂₁x̄
    public static double intercept(Dictionary<int, int> data)
    {
        double xMean = data.Keys.Average();
        double yMean = data.Values.Average();

        double slopeValue = slope(data);

        return yMean - slopeValue * xMean;
    }


    // Calculates the slope:
    // β̂₁ = Σ(xᵢ - x̄)(Yᵢ - Ȳ) / Σ(xᵢ - x̄)²
    public static double slope(Dictionary<int, int> data)
    {
        double xMean = data.Keys.Average();
        double yMean = data.Values.Average();

        double numerator = 0;
        double denominator = 0;

        foreach (var pair in data)
        {
            double x = pair.Key;
            double y = pair.Value;

            numerator += (x - xMean) * (y - yMean);
            denominator += Math.Pow(x - xMean, 2);
        }

        return numerator / denominator;
    }


    public static void Main(string[] args)
    {
        // Developer constants
        const bool LOG = true; 

        const string fileName = "data.csv";

        Dictionary<int, int> data = Read(fileName, LOG);

        double slopeValue = slope(data);
        double interceptValue = intercept(data);

        if (LOG)
        {
            Console.WriteLine("Slope: " + slopeValue);
            Console.WriteLine("Intercept: " + interceptValue);
            Console.WriteLine(
                $"Regression equation: ŷ = {interceptValue} + {slopeValue}x"
            );
        }
    }
}