using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUBES_KPL.API.Models;

namespace TUBES_KPL.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtikelApiController : ControllerBase
    {
        public static List<Artikel> artikels = new List<Artikel>
        {
            //new Artikel { Id = 1, Judul = "Pengelohan Sampah", Isi = "Cara Mengolah Sampah Plastik", TanggalPublikasi = DateTime.Now }
        };

        // GET: api/ArtikelApi
        [HttpGet]
        public ActionResult<IEnumerable<Artikel>> GetAll()
        {
            return Ok(artikels);
        }

        // GET: api/ArtikelApi/1
        [HttpGet("{id}")]
        public ActionResult<Artikel> GetById(int id)
        {
            var artikel = artikels.FirstOrDefault(a => a.Id == id);
            if (artikel == null)
                return NotFound();

            return Ok(artikel);
        }

        // POST: api/ArtikelApi
        [HttpPost]
        public ActionResult<Artikel> Create(Artikel artikel)
        {
            // PRECONDITION
            if (string.IsNullOrWhiteSpace(artikel.Judul) || string.IsNullOrWhiteSpace(artikel.Isi))
                return BadRequest("Judul dan Isi artikel tidak boleh kosong.");

            artikel.Id = artikels.DefaultIfEmpty().Max(a => a?.Id ?? 0) + 1;
            artikel.TanggalPublikasi = DateTime.Now;
            artikels.Add(artikel);

            // POSTCONDITION
            if (!artikels.Contains(artikel))
                return StatusCode(500, "Artikel gagal ditambahkan.");

            return CreatedAtAction(nameof(GetById), new { id = artikel.Id }, artikel);
        }




        // PUT: api/ArtikelApi/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Artikel updatedArtikel)
        {
            // PRECONDITION
            if (string.IsNullOrWhiteSpace(updatedArtikel.Judul) || string.IsNullOrWhiteSpace(updatedArtikel.Isi))
                return BadRequest("Judul dan Isi tidak boleh kosong.");

            var artikel = artikels.FirstOrDefault(a => a.Id == id);
            if (artikel == null)
                return NotFound();

            artikel.Judul = updatedArtikel.Judul;
            artikel.Isi = updatedArtikel.Isi;

            // POSTCONDITION
            var updated = artikels.FirstOrDefault(a => a.Id == id);
            if (updated == null || updated.Judul != updatedArtikel.Judul || updated.Isi != updatedArtikel.Isi)
                return StatusCode(500, "Gagal memperbarui artikel.");

            return NoContent();
        }


    }
}
