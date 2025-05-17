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
            return _donationService.GetAll();
        }

        //[HttpPost]
        //public ActionResult Add([FromBody] Donation donation)
        //{
        //    _donationService.Add(donation);
        //    return Ok(new { message = "Donation added successfully" });
        //}

        //[HttpDelete("{index}")]
        //public IActionResult Remove(int index)
        //{
        //    var removed = _donationService.Remove(index);
        //    if (!removed)
        //    {
        //        return NotFound( new { message = $"Donation {index} not found" });
        //    }
        //}

        //}
    }
}
