using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

/*

====================================================================================================
Tugas besar konstruksi perangkat lunak dikerjakan berkelompok dengan aturan sebagai berikut: 
====================================================================================================

• Mengkonstruksi sebagian fitur dari perangkat lunak dengan bahasa pemrograman C# dan .NET. 
  Pada tugas besar CLO2 ini, fitur aplikasi diperbolehkan apabila masih terpisah (tidak terhubung 
  antara satu halaman dengan halaman yang lain) dan tidak memiliki tampilan GUI windows Form. 

• Setiap kelompok bertanggung jawab untuk menerapkan hal-hal sebagai berikut: (10% total nilai) 
  1. Mempunyai github atau remote repository yang berisi source code project  
  2. Mengumpulkan laporan tugas besar yang berisi beberapa hal yang akan disebutkan pada bagian selanjutnya 

• Setiap anggota kelompok bertanggung jawab untuk menerapkan hal berikut: (70% total nilai, penilaian dilakukan per individu/mahasiswa) 
  1. Menggunakan github atau version control lainnya untuk konstruksi (dibuktikan dengan adanya branch pribadi dan commit ke branch utama/main/master) 
  2. Melakukan unit testing pada modul/kode yang dibuat 
  3. Melakukan performance testing pada bagian kode (halaman aplikasi) yang dibuat 
  4. Menggunakan teknik konstruksi Defensive programming/design by contract (DbC) 
  5. Menerapkan dua teknik konstruksi di bawah ini pada bagian yang dibuat (masing-masing teknik berikut hanya boleh digunakan oleh maksimal dua mahasiswa): 
     - Automata 
     - Table-driven construction 
     - Parameterization/generics 
     - Runtime configuration 
     - Code reuse/library 
     - API 

• Pada LMS nanti akan disediakan halaman submit tugas besar dokumen laporan tugas besar: 
  1. Deskripsi singkat mengenai aplikasi tugas besar 
  2. Daftar anggota kelompok beserta teknik konstruksi yang dipilih (nilai per individu) 
  3. Unit testing yang dilakukan beserta penjelasan kode singkat untuk unit testing yang dilakukan (nilai per individu) 
  4. Penjelasan dan hasil performance testing (nilai per individu) 
  5. Implementasi design by contract (nilai per individu) 

• Sesi presentasi dan tanya jawab akan dilakukan pada pertemuan ke-11. Pada sesi ini, tanya jawab tiap anggota kelompok akan diberikan pertanyaan 
  yang relevan dengan bagian yang dikerjakan. Setiap anggota akan diminta menjelaskan bagian yang dikerjakan, termasuk source code yang telah dibuat.
  (20% total nilai, penilaian per individu) 



====================================================================================================
                                           PROGRAM
====================================================================================================

 */

public class Product
{
    private int id;
    private String nama;
    private int harga;
    private String kategori;
    private String dateAdded;

    public Product(int id, String nama, int harga, String kategori, String dateAdded)
    {
        ValidasiProduct.ValidasiID(id);
        ValidasiProduct.ValidasiID(nama);
        ValidasiProduct.ValidasiID(harga);
        ValidasiProduct.ValidasiID(kategori);

        this.id = id;
        this.nama = nama;
        this.harga = harga;
        this.kategori = kategori;
        this.dateAdded = dateAdded;
    }
}

public class ProductDatabase
{
    public void AddProduct()
    {

    }

    public void RemoveProduct()
    { 
    
    }

    public void UpdateProduct()
    { 
    
    }

    public void GetProduct()
    {

    }
}

public class ValidasiProduct
{
    public void ValidasiID(int id)
    {

    }

    public void ValidasiNama(String nama)
    {

    }

    public void ValidasiHarga(int harga)
    {

    }

    public void ValidasiKategori(String kategori)
    {

    }
}

public class Program
{
    public static void Main(String[] args)
    {
        
    }
}