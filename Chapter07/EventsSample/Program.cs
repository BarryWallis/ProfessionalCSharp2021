// See https://aka.ms/new-console-template for more information

using EventsSample;

CarDealer carDealer = new();
Consumer sebastian = new("Sebastian");
carDealer.NewCarCreated += sebastian.NewCarIsHere;
carDealer.CreateNewCar("Williams");

Consumer max = new("Max");
carDealer.NewCarCreated += max.NewCarIsHere;
carDealer.CreateNewCar("Aston Martin");
carDealer.NewCarCreated -= sebastian.NewCarIsHere;
carDealer.CreateNewCar("Ferrari");
