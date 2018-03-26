using System;
// ReSharper disable UnusedMember.Global

namespace TaxiCloud.Core.Api.Yandex.Model
{
    public class YandexDriverModel
    {
        public YandexDriver Driver { get; set; }
        public string CarId { get; set; }
        public YandexCarModel Car { get; set; }
        public float Balance { get; set; }
        public float Limit { get; set; }

        public string YandexId { get; set; }
        public string Status { get; internal set; }

        public override string ToString()
        {
            return $"{Driver.LastName} {Driver.FirstName} {Driver.Surname}";
        }
    }

    public class YandexDriver
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Surname { get; set; }
        public DateTime DateCreate { get; set; }
        public bool PersonalPostTerminal { get; set; }
        public DateTime DateStart { get; set; }
        public string LicenseSeries { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime LicenseDateOf { get; set; }
        public DateTime LicenseDateEnd { get; set; }
        public int WorkStatusType { get; set; }
        public string RuleId { get; set; }
        public string Phones { get; set; }

        public string InternalId => LicenseNumber + LicenseSeries;
    }
}