using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUBES_KPL.API.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public string DonorName { get; set; }
        public decimal Amount { get; set; }
    }
}
