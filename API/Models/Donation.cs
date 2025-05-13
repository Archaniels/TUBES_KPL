using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUBES_KPL.API.Models
{
    public class Donation
    {
        public string Id { get; set; }
        public string DonorName { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public Donation(string id, string donorName, double amount, DateTime date)
        {
            Id = id;
            DonorName = donorName;
            Amount = amount;
            Date = date;
        }
    }
}
