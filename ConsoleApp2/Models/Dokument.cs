using System;
using System.Collections.Generic;

namespace DokumentArchiwum.Models
{
    public class Dokument
    {
        public string Tytul { get; set; }
        public int Rok { get; set; }
        public Guid UUID { get; set; }
        public string Kategoria { get; set; }
        public string MiejscePrzechowywania { get; set; }
        public int LiczbaEgzemplarzy { get; set; }
        public List<HistoriaZmian> HistoriaZmian { get; set; }

        public Dokument(string tytul, int rok, string kategoria, string miejscePrzechowywania, int liczbaEgzemplarzy)
        {
            Tytul = tytul;
            Rok = rok;
            UUID = Guid.NewGuid();
            Kategoria = kategoria;
            MiejscePrzechowywania = miejscePrzechowywania;
            LiczbaEgzemplarzy = liczbaEgzemplarzy;
            HistoriaZmian = new List<HistoriaZmian>();
        }

        public void DodajZmiane(HistoriaZmian zmiana)
        {
            HistoriaZmian.Add(zmiana);
        }
    }

    public class HistoriaZmian
    {
        public string Opis { get; set; }
        public DateTime DataZmiany { get; set; }
        public string Uzytkownik { get; set; }

        public HistoriaZmian(string opis, DateTime dataZmiany, string uzytkownik)
        {
            Opis = opis;
            DataZmiany = dataZmiany;
            Uzytkownik = uzytkownik;
        }
    }
}
