﻿using SUS.MvcFramework;
using System.Threading.Tasks;

namespace Git
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateHostAsync(new Startup());
        }
    }
}
