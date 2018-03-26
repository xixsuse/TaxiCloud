using System;
using System.ComponentModel.DataAnnotations;

namespace TaxiCloud.Core.Repository.Model
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }
        public string Message { get; set; }
        public string InternalId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}