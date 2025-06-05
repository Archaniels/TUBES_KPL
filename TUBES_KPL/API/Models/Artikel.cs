using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUBES_KPL.API.Models
{
    public class Artikel
    {
        public int Id { get; set; }
        public string Judul { get; set; }
        public string Isi { get; set; }
        public DateTime TanggalPublikasi { get; set; }
    }
}
