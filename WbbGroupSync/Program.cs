using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WbbGroupSync
{
    class Program
    {
        static void Main(string[] args)
        {
            WbbGroupSync.StartWbbGroupSync();

            WbbGroupSync.AddUserToFaction(6817, 1, 2);

            Console.ReadLine();

            WbbGroupSync.AddUserToFaction(6817, 1, 3);

            Console.ReadLine();

            WbbGroupSync.AddUserToFaction(6817, 1, 0);

            Console.ReadLine();
        }
    }
}
