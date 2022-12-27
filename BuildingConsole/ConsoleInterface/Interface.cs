using BuildingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingConsole.ConsoleInterface
{
    internal class Interface
    {

        private uint? ReadUInt(bool required = true)
        {
            bool isValid = false;
            uint? value = null;

            do
            {
                isValid = uint.TryParse(Console.ReadLine(), out uint readValue);
                if (required && !isValid) Console.WriteLine("Error: Enter positive integer value");
                if (isValid) value = readValue;
            } while (required && !isValid);
            return value;
        }

        private string? ReadString()
        {
            return Console.ReadLine();
        }

        private DateOnly? ReadDate(bool required = true)
        {
            bool isValid = false;
            DateOnly? value = null;

            do
            {
                isValid = DateOnly.TryParse(Console.ReadLine(), out DateOnly readValue);
                if (required && !isValid) Console.WriteLine("Error: Enter currect date");
                if (isValid) value = readValue;
            } while (required && !isValid);
            return value;
        }

        private float? ReadUFloat(bool required = true)
        {
            bool isValid = false;
            float? value = null;

            do
            {
                isValid = float.TryParse(Console.ReadLine(), out float readValue);
                if (isValid && value < 0.0) isValid = false;
                if (required && !isValid) Console.WriteLine("Error: Enter positive float value");
                if (isValid) value = readValue;
            } while (required && !isValid);

            return value;
        }
        public Guid? ReadBuildingId(bool required = true)
        {

            bool isValid = false;
            Guid? value = null;

            do
            {
                Console.Write("Enter building id: ");
                isValid = Guid.TryParse(Console.ReadLine(), out Guid readValue);
                if (required && !isValid) Console.WriteLine("Error: Enter the correct id");
                if (isValid) value = readValue;
            } while (required && !isValid);
            return value;
        }

        public uint ReadMenuCommand()
        {

            uint? menuCommand = 0;
            bool isValid = false;

            while (!isValid)
            {
                Console.WriteLine("Menu:");

                Console.WriteLine("1 - Add building");
                Console.WriteLine("2 - Set maximum count of buildings");
                Console.WriteLine("3 - Show buildings");
                Console.WriteLine("4 - Find building");
                Console.WriteLine("5 - Delete building");
                Console.WriteLine("6 - Exit");

                Console.Write("Command: ");

                menuCommand = ReadUInt();

                if (menuCommand > 0 && menuCommand < 8) isValid = true;
                else Console.WriteLine("Enter value beetween 1 and 8");
            }

            return (uint)menuCommand;

        }

        public uint ReadMaxOfBuildings(uint maximumCountOfBuildings = 0)
        {

            Console.Write($"Enter maximum count of buildings(current: {maximumCountOfBuildings}): ");

            return (uint)ReadUInt();

        }

        public uint? ReadBuildingType(bool required = true)
        {
            bool isValid = false;
            uint? value = null;
            do
            {
                Console.WriteLine("1 - House");
                Console.WriteLine("2 - Office");
                Console.WriteLine("3 - Wire house");

                value = ReadUInt(required);
                if (value > 0 && value < 4) isValid = true;
                if (value != null && !isValid) Console.WriteLine("Enter value beetween 1 and 3");

            } while ((required || (value != null && !isValid)) && !isValid);

            return value;
        }

        public void ReadNewBuilding(out uint? buildingType, out uint? floorsNumber, out string? address, out float? square, out DateOnly? constructionDate)
        {
            Console.WriteLine("New building:");

            Console.WriteLine("Building type(default: House): ");
            buildingType = ReadBuildingType(false);
            if (buildingType != null) buildingType--;

            Console.Write("Building address(default: Street): ");
            address = ReadString();

            Console.Write("Building floors number(default: 1): ");
            floorsNumber = ReadUInt(false);

            Console.Write("Building square(default: 1.0): ");
            square = ReadUFloat(false);

            Console.Write("Building construction date(default: Today): ");
            constructionDate = ReadDate(false);
        }

        public void ShowObjects(List<Building> buildings)
        {

            foreach (var building in buildings)
            {
                ShowBuilding(building);
            }

        }

        public void ShowBuilding(Building building)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine(building);
            Console.WriteLine("---------------------------");
        }

        public uint ReadFindParameter()
        {
            bool isValid = false;
            uint value = 0;
            while (!isValid)
            {
                Console.WriteLine("1 - Address");
                Console.WriteLine("2 - Type");

                value = (uint)ReadUInt();
                if (value > 0 && value < 3) isValid = true;
                else Console.WriteLine("Enter value beetween 1 and 2");
            }

            return value;
        }

        public uint ReadDeleteParameter()
        {
            bool isValid = false;
            uint value = 0;

            while (!isValid)
            {
                Console.WriteLine("1 - Delete by id");
                Console.WriteLine("2 - Delete by address");

                value = (uint)ReadUInt();

                if (value > 0 && value < 3) isValid = true;
                else Console.WriteLine("Enter value beetween 1 and 2");

            }
            return value;
        }

        public string ReadAddress()
        {
            Console.Write("Enter address: ");
            return ReadString();
        }

        public uint ReadAcception(string question)
        {
            Console.WriteLine(question);
            Console.WriteLine("1 - Yes");
            Console.WriteLine("2 - No");
            Console.WriteLine("3 - Cancel");

            bool isValid = false;
            uint value = 0;

            while (!isValid)
            {
                value = (uint)ReadUInt();

                if (value > 0 && value < 4) isValid = true;
                else Console.WriteLine("Enter value beetween 1 and 3");
            }

            return value;
        }

        public void PrintError(string message)
        {
            Console.WriteLine($"Error: {message}");
        }


    }
}
