using APIABADI.Model;
using Microsoft.AspNetCore.Mvc;

namespace APIABADI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Artikel_Controller : Controller
    {
        public static List<ArtikelModel> artikels = new List<ArtikelModel>
        {
            //new Artikel { Id = 1, Judul = "Pengelohan Sampah", Isi = "Cara Mengolah Sampah Plastik", TanggalPublikasi = DateTime.Now }
        };

        // GET: api/ArtikelApi
        [HttpGet]
        public ActionResult<IEnumerable<ArtikelModel>> GetAll()
        {
            return Ok(artikels);
        }

        // GET: api/ArtikelApi/1
        [HttpGet("{id}")]
        public ActionResult<ArtikelModel> GetById(int id)
        {
            var artikel = artikels.FirstOrDefault(a => a.Id == id);
            if (artikel == null)
                return NotFound();

            return Ok(artikel);
        }

        // POST: api/ArtikelApi
        [HttpPost]
        public ActionResult<ArtikelModel> Create(ArtikelModel artikel)
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

        // DELETE: api/ArtikelApi/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // PRECONDITION
            var artikel = artikels.FirstOrDefault(a => a.Id == id);
            if (artikel == null)
                return NotFound("Artikel tidak ditemukan.");

            artikels.Remove(artikel);

            // POSTCONDITION
            if (artikels.Any(a => a.Id == id))
                return StatusCode(500, "Artikel gagal dihapus.");

            return NoContent();
        }


        // PUT: api/ArtikelApi/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, ArtikelModel updatedArtikel)
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
