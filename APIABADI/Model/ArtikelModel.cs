namespace APIABADI.Model
{
    public class ArtikelModel
    {

        public int Id { get; set; }
        public string Judul { get; set; }
        public string Isi { get; set; }
        public DateTime TanggalPublikasi { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Judul) &&
                   !string.IsNullOrWhiteSpace(Isi) &&
                   TanggalPublikasi <= DateTime.Now;
        }
    }
}
