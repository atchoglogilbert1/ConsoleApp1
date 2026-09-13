using System;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

class Program
{

    public static Dictionary<int, int> Read (String filename, bool log = false)
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
            return (data);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Failed to find file: " + filename);
            return new Dictionary<int, int>();
        }
    }


    public static void Main(string[] args)
    {
        const string fileName = "data.csv";
        Dictionary<int,int> data = Read (fileName, false);


         
    
    }
}