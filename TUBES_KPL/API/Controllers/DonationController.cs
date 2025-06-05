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

        public DonationController(IGenericService<Donation> donationService)
        {
            _donationService = donationService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Donation>> GetAll()
        {
            var donations = _donationService.GetAll();
            if (donations == null)
            {
                throw new InvalidOperationException("Data masih kosong.");
            }
            return donations;
        }

        [HttpPost]
        public ActionResult Add([FromBody] Donation donation)
        {
            if (donation == null)
            {
                throw new ArgumentNullException(nameof(donation), "Input tidak boleh kosong.");
            }

            _donationService.Add(donation);
            return Ok(new { message = "Donation added successfully" });
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("id tidak valid.", nameof(id));
            }

            _donationService.Remove(id);
            return Ok(new { message = "Donation removed" });
        }
    }
}
