﻿using SUS.MvcFramework;
using System.Threading.Tasks;

namespace BattleCards
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateHostAsync(new Startup());
        }
    }
}