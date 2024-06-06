using System;

namespace DokumentArchiwum.Models
{
    public class Uzytkownik
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public Guid UUID { get; set; }
        public string NazwaUzytkownika { get; set; }

        public Uzytkownik(string imie, string nazwisko, string nazwaUzytkownika)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            NazwaUzytkownika = nazwaUzytkownika;
            UUID = Guid.NewGuid();
        }
    }
}
