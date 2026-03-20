using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace rubiks_cube_simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cube cube = new Cube(true);
            // Cube cube2 = new Cube("./tmp/test.json");
            //
            // while (!cube.compare(cube2))
            // {
            //     cube2.doAlgo("RUR'URUUR'");
            //     cube2.print();
            // }

            Cube cube = new Cube(true);
            Cube cube2 = new Cube(true);
            Cube? cube3 = new Cube();
            Console.WriteLine(cube.compare(cube2));
            cube.doAlgo("RUR'URUUR'");
            cube.print();
            string h = JsonSerializer.Serialize(cube);
            Console.WriteLine(h);
            using (StreamWriter f = new StreamWriter("./test.json"))
            {
                f.Write(h);
            }

            foreach (var item in cube.blocks)
            {
                Console.WriteLine(item?.GetType());
            }

            cube3 = JsonSerializer.Deserialize<Cube>(h);
            h = JsonSerializer.Serialize(cube3);
            Console.WriteLine(h);
            using (StreamWriter f = new StreamWriter("./test2.json"))
            {
                f.Write(h);
            }
            if (cube3 != null)
            {
                foreach (var item in cube3.blocks)
                {
                    Console.WriteLine(item?.GetType());
                }
                cube3.print();
            }

            // Cube? cube;
            // using (StreamReader r = new StreamReader("./tmp/test.json"))
            // {
            //     string h = r.ReadToEnd();
            //     cube = JsonSerializer.Deserialize<Cube>(h);
            // }
            // if (cube != null)
            // {
            //     cube.print();
            // }
        }
    }
}
