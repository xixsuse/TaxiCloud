using System;
// ReSharper disable UnusedMember.Global
namespace TaxiCloud.Core.Api.Yandex.Model
{
    public class YandexCarModel
    {
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
        public string[] Tariff { get; set; }
        public int Distance { get; set; }
        public int ChairCount { get; set; }
        public int BusterCount { get; set; }

        public string YandexId { get; set; }

        public override string ToString()
        {
            return $"{Mark} {ModelName} [{Number}]";
        }
    }
}