﻿using System.Threading.Tasks;

namespace DndParser
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // For the sake of the scope of this project, these two are hard coded into the DnDParser.
            // string baseUrl = "https://www.dnd5eapi.co";
            // string startPoint = "/api/2014/ability-scores";

            // Schema Validator URL: https://www.jsonschemavalidator.net/

            Console.WriteLine("Beginning DnD Parser");
            await Parser.StartParser(0);
        }
    }
}