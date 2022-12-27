using BuildingData;
using BuildingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuildingConsole.ConsoleInterface
{
    internal class Application
    {
        public enum MenuCommand
        {
            AddBuilding = 1,
            EditMaxBuildings,
            PrintBuildings,
            FindBuilding,
            DeleteBuilding,
            Exit
        }

        public enum FindParameter
        {
            Address = 1,
            Type
        }

        public enum DeleteParameter
        {
            Id = 1,
            Address
        }


        public enum AcceptCase
        {
            Yes = 1,
            No,
            Cancel
        }

        private Interface appInterface = new Interface();

        BuildingsRepo buildings = new BuildingsRepo();

        private void AddBuilding()
        {
            if (buildings.Count == buildings.MaxBuildings)
            {
                appInterface.PrintError("The maximum number of buildings has been reached");
                return;
            }
            appInterface.ReadNewBuilding(out uint? buildingType, out uint? floorsNumber, out string? address, out float? square, out DateOnly? constructionDate);

            Building building = new Building();

            if (buildingType != null) building.type = (BuildingType)buildingType;
            if (floorsNumber != null) building.floorsNumber = (uint)floorsNumber;
            if (address != null && address.Length > 0) building.address = address;
            if (square != null) building.square = (float)square;
            if (constructionDate != null) building.constructionDate = (DateOnly)constructionDate;

            buildings.Add(building);
        }

        public void EditMaxBuildings()
        {
            try
            {
                uint maxBuildings = appInterface.ReadMaxOfBuildings(buildings.MaxBuildings);
                buildings.MaxBuildings = maxBuildings;
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine($"Error: {argumentException.Message}");
            }
        }

        public void PrintBuildings()
        {
            appInterface.ShowObjects(buildings.Buildings);
        }

        public void FindBuilding()
        {
            switch (appInterface.ReadFindParameter())
            {
                case (uint)FindParameter.Address:
                    string address = appInterface.ReadAddress();
                    appInterface.ShowObjects(buildings.Find((building) =>
                    {
                        var regex = new Regex($"{address}");
                        return regex.IsMatch(building.address);
                    }));

                    break;
                case (uint)FindParameter.Type:
                    uint buildingType = (uint)appInterface.ReadBuildingType() - 1;
                    appInterface.ShowObjects(buildings.Find((building) =>
                    {
                        return (uint)building.type == buildingType;
                    }));
                    break;
            }
        }

        public void DeleteBuilding()
        {

            List<Building> deletingBuildings = new();


            switch ((DeleteParameter)appInterface.ReadDeleteParameter())
            {
                case DeleteParameter.Id:
                    Console.WriteLine("Enter id of building to delete");
                    Guid guid = (Guid)appInterface.ReadBuildingId();
                    Building? deletingBuilding = buildings.FindById(guid);
                    if (deletingBuilding == null)
                    {
                        Console.WriteLine("Error: Not found building with such id");
                        return;
                    }
                    deletingBuildings.Add(deletingBuilding);

                    break;
                case DeleteParameter.Address:
                    Console.WriteLine("Enter address of building to delete");
                    string address = appInterface.ReadAddress();
                    deletingBuildings = buildings.Find((building) => building.address == address);
                    break;
            }

            Console.WriteLine("Deleting buildings:");
            appInterface.ShowObjects(deletingBuildings);

            switch ((AcceptCase)appInterface.ReadAcception("Remove this building?"))
            {
                case AcceptCase.Yes:
                    foreach (var deletingBuilding in deletingBuildings)
                    {
                        buildings.Delete(deletingBuilding.Id);
                    }
                    break;
                case AcceptCase.No:
                    break;
                case AcceptCase.Cancel:
                    break;
            }
        }

        public void Start()
        {
            bool exit = false;

            while (!exit)
            {

                switch (appInterface.ReadMenuCommand())
                {
                    case (uint)MenuCommand.AddBuilding:
                        AddBuilding();
                        break;

                    case (uint)MenuCommand.EditMaxBuildings:
                        EditMaxBuildings();
                        break;

                    case (uint)MenuCommand.PrintBuildings:
                        PrintBuildings();
                        break;

                    case (uint)MenuCommand.FindBuilding:
                        FindBuilding();
                        break;

                    case (uint)MenuCommand.DeleteBuilding:
                        DeleteBuilding();
                        break;

                    case (uint)MenuCommand.Exit:
                        exit = true;
                        break;
                }
            }

        }
    }
}

