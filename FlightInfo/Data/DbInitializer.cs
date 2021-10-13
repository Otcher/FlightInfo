using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightInfo.Models;

namespace FlightInfo.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FlightInfoContext context)
        {
            context.Database.EnsureCreated();

            if (context.Country.Any())
            {
                return;
            }

            var countries = new Country[]
            {
                new Country{ Name="Israel" },
                new Country{ Name="France" },
                new Country{ Name="Japan" },
                new Country{ Name="United States" },
                new Country{ Name="Iran" },
                new Country{ Name="Egypt" }
            };
            foreach (Country c in countries)
            {
                context.Country.Add(c);
            }
            context.SaveChanges();

            var cities = new City[]
            {
                new City{ Name="Rishon Leziyon", Country=countries[0] },
                new City{ Name="Paris", Country=countries[1] },
                new City{ Name="Tokyo", Country=countries[2] },
                new City{ Name="Las Vegas", Country=countries[3] },
                new City{ Name="Tehran", Country=countries[4] },
                new City{ Name="Cairo", Country=countries[5] }
            };
            
            var airports = new Airport[]
            {
                new Airport{ Name="Ben Gurion Airport", City=cities[0], Latitude=32.005532, Longtitude=34.88541120000002 },
                new Airport{ Name="Paris Charles de Gaulle Airport", City=cities[1], Latitude=49.009724, Longtitude=2.547778 },
                new Airport{ Name="Tokyo Haneda Airport", City=cities[2], Latitude=35.553333, Longtitude=139.781113 },
                new Airport{ Name="McCarran International Airport (LAS)", City=cities[3], Latitude=36.086010, Longtitude=-115.153969 },
                new Airport{ Name="Tehran Imam Khomeini International Airport", City=cities[4], Latitude=35.68998819999999, Longtitude=51.311240999999995 },
                new Airport{ Name="Cairo International Airport", City=cities[5], Latitude=30.033333, Longtitude=31.233334 }
            };

            for (int i = 0; i < cities.Length; ++i)
            {
                cities[i].Airport = airports[i];
                context.City.Add(cities[i]);
                context.Airport.Add(airports[i]);
            }
            context.SaveChanges();

            var planes = new Plane[]
            {
                new Plane{ Manufacturer="Boeing ", Model="Boeing 737",Capacity=600, CruiseSpeed=836 },
                new Plane{ Manufacturer="Airbus", Model="Airbus A320",Capacity=200, CruiseSpeed=871 },
                new Plane{ Manufacturer="Boeing", Model="Boeing 787", Capacity=290, CruiseSpeed=954 }
            };
            foreach (Plane p in planes)
            {
                context.Plane.Add(p);
            }
            context.SaveChanges();

            var users = new User[]
            {
                new User{ Name= "oz", Password="123" },
                new User{ Name= "yarin", Password="123" },
                new User{ Name= "nadav", Password="123" },
                new User{ Name= "omri", Password="123" }
            };
            foreach (User u in users)
            {
                context.User.Add(u);
            }
            context.SaveChanges();

            var pilots = new Pilot[]
            {
                new Pilot{ FirstName="John", LastName="Marcus", Birthdate=new DateTime(1995, 1, 17) , Qualification=new List<Plane>{ planes[0] } },
                new Pilot{ FirstName="Elizabeth", LastName="Fredie", Birthdate=new DateTime(1980, 3, 19) , Qualification=new List<Plane>{planes[1]} },
                new Pilot{ FirstName="David", LastName="Denis", Birthdate=new DateTime(1985, 8, 23) , Qualification=new List<Plane>{planes[0], planes[2]} }
            };
            foreach (Pilot p in pilots)
            {
                context.Person.Add(p);
            }
            context.SaveChanges();

            var passengers = new Passenger[]
            {
                new Passenger{ FirstName="Layla", LastName="Charlie", Birthdate=new DateTime(1965, 4, 26) },
                new Passenger{ FirstName="Patrick", LastName="Loyal", Birthdate=new DateTime(1972, 7, 11) },
                new Passenger{ FirstName="Aaron", LastName="Dan", Birthdate=new DateTime(1989, 8, 14) },
                new Passenger{ FirstName="Fran", LastName="Cis", Birthdate=new DateTime(1969, 2, 17) },
                new Passenger{ FirstName="Jacob", LastName="Sha", Birthdate=new DateTime(1965, 3, 23) },
                new Passenger{ FirstName="Sara", LastName="Darly", Birthdate=new DateTime(1973, 6, 22) },
                new Passenger{ FirstName="Nerkis", LastName="Rina", Birthdate=new DateTime(1977, 6, 13) },
                new Passenger{ FirstName="Flay", LastName="Ronny", Birthdate=new DateTime(1974, 4, 17) },
                new Passenger{ FirstName="Gony", LastName="Fnur", Birthdate=new DateTime(1999, 10, 16) },
                new Passenger{ FirstName="Jimmy", LastName="Cehov", Birthdate=new DateTime(2001, 11, 16) },
                new Passenger{ FirstName="Berny", LastName="Chorus", Birthdate=new DateTime(2000, 3, 8) },
                new Passenger{ FirstName="Danny", LastName="Kazanov", Birthdate=new DateTime(1996, 11, 9) },
                new Passenger{ FirstName="Larry", LastName="Flory", Birthdate=new DateTime(1995, 4, 3) },
                new Passenger{ FirstName="Jhonson", LastName="Dina", Birthdate=new DateTime(1988, 2, 2) },
                new Passenger{ FirstName="Ran", LastName="Nash", Birthdate=new DateTime(1978, 7, 1) }
            };
            foreach (Passenger p in passengers)
            {
                context.Person.Add(p);
            }
            context.SaveChanges();

            var flights = new Flight[]
            {
                new Flight{ FlightNumber="c321", Origin=airports[1],
                            Destination=airports[2], Pilot=pilots[0],
                            DepartureTime=new DateTime(2021, 4, 6),
                            PassengerManifest=new List<Passenger>{ passengers[1] }, Plane=planes[0] },
                new Flight
                {
                    FlightNumber = "a90",
                    Origin = airports[0],
                    Destination = airports[1],
                    Pilot = pilots[1],
                    DepartureTime = new DateTime(2021, 8, 7),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[2] },
                new Flight
                {
                    FlightNumber = "hh42",
                    Origin = airports[2],
                    Destination = airports[3],
                    Pilot = pilots[1],
                    DepartureTime = new DateTime(2021, 7, 2),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[1] },
                new Flight
                {
                    FlightNumber = "o9",
                    Origin = airports[3],
                    Destination = airports[4],
                    Pilot = pilots[2],
                    DepartureTime = new DateTime(2021, 2, 3),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[1] },
                new Flight
                {
                    FlightNumber = "a9",
                    Origin = airports[3],
                    Destination = airports[5],
                    Pilot = pilots[0],
                    DepartureTime = new DateTime(2021, 3, 6),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[2] },
                new Flight
                {
                    FlightNumber = "a1",
                    Origin = airports[5],
                    Destination = airports[1],
                    Pilot = pilots[0],
                    DepartureTime = new DateTime(2021, 7, 9),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[1] },
                new Flight
                {
                    FlightNumber = "b22",
                    Origin = airports[5],
                    Destination = airports[0],
                    Pilot = pilots[2],
                    DepartureTime = new DateTime(2021, 8, 9),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[0] },
                new Flight
                {
                    FlightNumber = "d92",
                    Origin = airports[3],
                    Destination = airports[4],
                    Pilot = pilots[1],
                    DepartureTime = new DateTime(2021, 1, 9),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[0] },
                new Flight
                {
                    FlightNumber = "nm2",
                    Origin = airports[3],
                    Destination = airports[2],
                    Pilot = pilots[2],
                    DepartureTime = new DateTime(2021, 2, 2),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[2] },
                new Flight
                {
                    FlightNumber = "vv3",
                    Origin = airports[4],
                    Destination = airports[5],
                    Pilot = pilots[1],
                    DepartureTime = new DateTime(2021, 6, 13),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[1] },
                new Flight
                {
                    FlightNumber = "s1",
                    Origin = airports[4],
                    Destination = airports[1],
                    Pilot = pilots[0],
                    DepartureTime = new DateTime(2021, 5, 25),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[1] }
                ,new Flight
                {
                    FlightNumber = "d3",
                    Origin = airports[2],
                    Destination = airports[4],
                    Pilot = pilots[0],
                    DepartureTime = new DateTime(2021, 4, 2),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[0] }
                ,new Flight
                {
                    FlightNumber = "gf2",
                    Origin = airports[4],
                    Destination = airports[3],
                    Pilot = pilots[2],
                    DepartureTime = new DateTime(2021, 3, 1),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[0] }
                ,new Flight
                {
                    FlightNumber = "s99",
                    Origin = airports[1],
                    Destination = airports[0],
                    Pilot = pilots[2],
                    DepartureTime = new DateTime(2021, 2, 27),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[1] }
                ,new Flight
                {
                    FlightNumber = "f12",
                    Origin = airports[2],
                    Destination = airports[1],
                    Pilot = pilots[1],
                    DepartureTime = new DateTime(2021, 7, 23),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[2] }
                ,new Flight
                {
                    FlightNumber = "d43",
                    Origin = airports[5],
                    Destination = airports[0],
                    Pilot = pilots[1],
                    DepartureTime = new DateTime(2021, 5, 3),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[1] }
                ,new Flight
                {
                    FlightNumber = "c3",
                    Origin = airports[3],
                    Destination = airports[5],
                    Pilot = pilots[0],
                    DepartureTime = new DateTime(2021, 4, 2),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[0] }
                ,new Flight
                {
                    FlightNumber = "b19",
                    Origin = airports[4],
                    Destination = airports[2],
                    Pilot = pilots[2],
                    DepartureTime = new DateTime(2021, 3, 9),
                    PassengerManifest = new List<Passenger> { passengers[0], passengers[1] }, Plane = planes[0] }
            };
            foreach (Flight f in flights)
            {
                context.Flight.Add(f);
            }
            context.SaveChanges();
        }
    }
}
