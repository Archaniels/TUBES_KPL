using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUBES_KPL.API.Models;

namespace TUBES_KPL.API.Controllers
{
    // Menandakan bahwa controller ini adalah API controller
    [ApiController]
    // Menentukan route dasar: api/ArtikelApi
    [Route("api/[controller]")]
    public class ArtikelApiController : ControllerBase
    {
        // Simulasi penyimpanan data artikel dalam memory (in-memory storage)
        public static List<Artikel> artikels = new List<Artikel>();

        // GET: api/ArtikelApi
        // Mengembalikan seluruh daftar artikel
        [HttpGet]
        public ActionResult<IEnumerable<Artikel>> GetAll()
        {
            return Ok(artikels);
        }

        // GET: api/ArtikelApi/{id}
        // Mengambil satu artikel berdasarkan ID
        [HttpGet("{id}")]
        public ActionResult<Artikel> GetById(int id)
        {
            var artikel = artikels.FirstOrDefault(a => a.Id == id);

            // Jika tidak ditemukan, kembalikan 404
            if (artikel == null)
                return NotFound();

            return Ok(artikel);
        }

        // POST: api/ArtikelApi
        // Menambahkan artikel baru
        [HttpPost]
        public ActionResult<Artikel> Create(Artikel artikel)
        {
            // PRECONDITION: Judul dan isi tidak boleh kosong
            if (string.IsNullOrWhiteSpace(artikel.Judul) || string.IsNullOrWhiteSpace(artikel.Isi))
                return BadRequest("Judul dan Isi artikel tidak boleh kosong.");

            // Generate ID otomatis dengan mencari ID tertinggi saat ini
            artikel.Id = artikels.DefaultIfEmpty().Max(a => a?.Id ?? 0) + 1;
            artikel.TanggalPublikasi = DateTime.Now;
            artikels.Add(artikel);

            // POSTCONDITION: Pastikan artikel berhasil ditambahkan
            if (!artikels.Contains(artikel))
                return StatusCode(500, "Artikel gagal ditambahkan.");

            // Mengembalikan response Created (201)
            return CreatedAtAction(nameof(GetById), new { id = artikel.Id }, artikel);
        }

        // PUT: api/ArtikelApi/{id}
        // Memperbarui artikel berdasarkan ID
        [HttpPut("{id}")]
        public IActionResult Update(int id, Artikel updatedArtikel)
        {
            // PRECONDITION: Validasi input
            if (string.IsNullOrWhiteSpace(updatedArtikel.Judul) || string.IsNullOrWhiteSpace(updatedArtikel.Isi))
                return BadRequest("Judul dan Isi tidak boleh kosong.");

            // Cari artikel yang akan diperbarui
            var artikel = artikels.FirstOrDefault(a => a.Id == id);
            if (artikel == null)
                return NotFound();

            // Update data artikel
            artikel.Judul = updatedArtikel.Judul;
            artikel.Isi = updatedArtikel.Isi;

            // POSTCONDITION: Pastikan artikel benar-benar diperbarui
            var updated = artikels.FirstOrDefault(a => a.Id == id);
            if (updated == null || updated.Judul != updatedArtikel.Judul || updated.Isi != updatedArtikel.Isi)
                return StatusCode(500, "Gagal memperbarui artikel.");

            // Tidak mengembalikan body, hanya status code 204 (No Content)
            return NoContent();
        }
    }
}
