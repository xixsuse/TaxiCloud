using System;
using System.ComponentModel.DataAnnotations;
using TaxiCloud.Core.Api.Yandex.Model;

namespace TaxiCloud.Core.Repository.Model
{
    public class Driver
    {
        [Key]
        public string Id { get; set; }

        public string YandexId { get; set; }
        public string Status { get; set; }
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

        public string CarId { get; set; }
        public float Balance { get; set; }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {Surname}";
        }

        public void UpdateFromYandexDriver(YandexDriverModel ydm)
        {
            Status = ydm.Status;
            YandexId = ydm.YandexId;
            Balance = ydm.Balance;
            CarId = ydm.CarId;
            DateCreate = ydm.Driver.DateCreate;
            DateStart = ydm.Driver.DateStart;
            FirstName = ydm.Driver.FirstName;
            LastName = ydm.Driver.LastName;
            LicenseDateEnd = ydm.Driver.LicenseDateEnd;
            LicenseDateOf = ydm.Driver.LicenseDateOf;
            LicenseNumber = ydm.Driver.LicenseNumber;
            LicenseSeries = ydm.Driver.LicenseSeries;
            PersonalPostTerminal = ydm.Driver.PersonalPostTerminal;
            Phones = ydm.Driver.Phones;
            RuleId = ydm.Driver.RuleId;
            Surname = ydm.Driver.Surname;
            WorkStatusType = ydm.Driver.WorkStatusType;
        }

        public bool HasChanges(YandexDriverModel ymd)
        {
            return
                YandexId != ymd.YandexId ||
                Status != ymd.Status ||
                Math.Abs(Balance - ymd.Balance) > 0.1 ||
                CarId != ymd.CarId ||
                DateCreate != ymd.Driver.DateCreate ||
                DateStart != ymd.Driver.DateStart ||
                FirstName != ymd.Driver.FirstName ||
                Id != ymd.Driver.InternalId ||
                LastName != ymd.Driver.LastName ||
                LicenseDateEnd != ymd.Driver.LicenseDateEnd ||
                LicenseDateOf != ymd.Driver.LicenseDateOf ||
                LicenseNumber != ymd.Driver.LicenseNumber ||
                LicenseSeries != ymd.Driver.LicenseSeries ||
                PersonalPostTerminal != ymd.Driver.PersonalPostTerminal ||
                Phones != ymd.Driver.Phones ||
                RuleId != ymd.Driver.RuleId ||
                Surname != ymd.Driver.Surname ||
                WorkStatusType != ymd.Driver.WorkStatusType;
        }

        public static Driver CreateFromYandexDriver(YandexDriverModel m)
        {
            Driver driver = new Driver
            {
                Status = m.Status,
                YandexId = m.YandexId,
                Balance = m.Balance,
                CarId = m.CarId,
                DateCreate = m.Driver.DateCreate,
                DateStart = m.Driver.DateStart,
                FirstName = m.Driver.FirstName,
                Id = m.Driver.InternalId,
                LastName = m.Driver.LastName,
                LicenseDateEnd = m.Driver.LicenseDateEnd,
                LicenseDateOf = m.Driver.LicenseDateOf,
                LicenseNumber = m.Driver.LicenseNumber,
                LicenseSeries = m.Driver.LicenseSeries,
                PersonalPostTerminal = m.Driver.PersonalPostTerminal,
                Phones = m.Driver.Phones,
                RuleId = m.Driver.RuleId,
                Surname = m.Driver.Surname,
                WorkStatusType = m.Driver.WorkStatusType
            };
            return driver;
        }
    }
}