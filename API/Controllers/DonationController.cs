using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TUBES_KPL.API.Models;
using TUBES_KPL.API.Services;

namespace TUBES_KPL.API.Controllers
{
    [ApiController]
    [Route("api/donations")]
    public class DonationController : ControllerBase
    {
        private readonly IGenericService<Donation> _donationService;

        public DonationController()
        {
            _donationService = new GenericService<Donation>();
        }

        [HttpPost]
        public IActionResult MakeDonation([FromBody] Donation donation)
        {
            _donationService.Add(donation);
            return Ok(new { message = "Donasi Berhasil", donation });
        }

        [HttpGet]
        public IActionResult GetAllDonations()
        {
            return Ok(_donationService.GetAll());
        }
    }
}
