using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CargoBase
{
    class TruckContext : DbContext
    {
        public TruckContext() : base("Truck") { }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<TypeTruck> TypeTrucks { get; set; }
    }
    //грузовики
    public class Truck
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Power { get; set; }
        public int Consumption { get; set; }
        public int Mileage { get; set; }
        public byte[] Photo { get; set; }
        public int? TypeTruckId { get; set; }
        public virtual TypeTruck TypeTruck { get; set; }
        public int? MarkId { get; set; }
        public virtual Mark Mark { get; set; }
        public override string ToString()
        {
            return Model;
        }
    }
    //тип грузовика
    public class TypeTruck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Truck> Trucks { get; set; }
        public TypeTruck()
        {
            Trucks = new List<Truck>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
    //марка грузовика
    public class Mark
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Truck> Trucks { get; set; }
        public Mark()
        {
            Trucks = new List<Truck>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
    //шофёры
    public class Driver
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime DayOfBirth { get; set; }
        public int Experience { get; set; }
        public byte[] Photo { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
        public Driver()
        {
            Flights = new List<Flight>();
        }

        public override string ToString()
        {
            return Surname + " " + Name + " " + Patronymic;
        }
    }
    //города
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
        public City()
        {
            Flights = new List<Flight>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
    //заказчики
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurnameDir { get; set; }
        public string NameDir { get; set; }
        public string PatronageDir { get; set; }
        public string Telephone { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
        public Customer()
        {
            Flights = new List<Flight>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
    public class Flight
    {
        public int Id { get; set; }
        public string Cargo { get; set; }
        public DateTime Date { get; set; }
        public Decimal Price { get; set; }
        public int? TruckId { get; set; }
        public virtual Truck Truck { get; set; }
        public int? DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        public int? CityId { get; set; }
        public virtual City City { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public override string ToString()
        {
            return Date.ToString() + " " + Cargo + " " + Driver + " " + City + " " + Customer;
        }
    }
}
