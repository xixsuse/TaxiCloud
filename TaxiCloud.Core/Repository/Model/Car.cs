using System;
using System.ComponentModel.DataAnnotations;
using TaxiCloud.Core.Api.Yandex.Model;

// ReSharper disable UnusedMember.Global

namespace TaxiCloud.Core.Repository.Model
{
    public class Car
    {
        [Key]
        public string Id { get; set; }
        public int Services { get; set; }
        public int Category { get; set; }
        public string Signal { get; set; }
        public string Number { get; set; }
        public DateTime DateCreate { get; set; }
        public string Transmission { get; set; }
        public string Mark { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseSeries { get; set; }
        public string LicenseDocument { get; set; }
        public int Distance { get; set; }
        public int ChairCount { get; set; }
        public int BusterCount { get; set; }

        public override string ToString()
        {
            return $"{Mark} {ModelName} [{Number}]";
        }

        public bool HasChanges(YandexCarModel car)
        {
            return
                BusterCount != car.BusterCount ||
                Category != car.Category ||
                ChairCount != car.ChairCount ||
                Color != car.Color ||
                DateCreate != car.DateCreate ||
                Distance != car.Distance || Id != car.YandexId ||
                LicenseDocument != car.LicenseDocument ||
                LicenseNumber != car.LicenseNumber ||
                LicenseSeries != car.LicenseSeries ||
                Mark != car.Mark ||
                ModelName != car.ModelName ||
                Number != car.Number ||
                Services != car.Services ||
                Signal != car.Signal ||
                Status != car.Status ||
                Transmission != car.Transmission ||
                Year != car.Year;
        }

        public void UpdateFromYandexCar(YandexCarModel car)
        {
            BusterCount = car.BusterCount;
            Category = car.Category;
            ChairCount = car.ChairCount;
            Color = car.Color;
            DateCreate = car.DateCreate;
            Distance = car.Distance;
            Id = car.YandexId;
            LicenseDocument = car.LicenseDocument;
            LicenseNumber = car.LicenseNumber;
            LicenseSeries = car.LicenseSeries;
            Mark = car.Mark;
            ModelName = car.ModelName;
            Number = car.Number;
            Services = car.Services;
            Signal = car.Signal;
            Status = car.Status;
            Transmission = car.Transmission;
            Year = car.Year;
        }

        public static Car CreateFromYandexCar(YandexCarModel car)
        {
            return new Car
            {
                BusterCount = car.BusterCount,
                Category = car.Category,
                ChairCount = car.ChairCount,
                Color = car.Color,
                DateCreate = car.DateCreate,
                Distance = car.Distance,
                Id = car.YandexId,
                LicenseDocument = car.LicenseDocument,
                LicenseNumber = car.LicenseNumber,
                LicenseSeries = car.LicenseSeries,
                Mark = car.Mark,
                ModelName = car.ModelName,
                Number = car.Number,
                Services = car.Services,
                Signal = car.Signal,
                Status = car.Status,
                Transmission = car.Transmission,
                Year = car.Year
            };
        }
    }
}
