using System;
using System.Collections.Generic;
using System.Linq;
using DokumentArchiwum.Interfaces;
using DokumentArchiwum.Models;
using Newtonsoft.Json;
using System.IO;

namespace DokumentArchiwum.Services
{
    public class Archiwum : IArchiwum
    {
        private List<Dokument> dokumenty;
        private List<Uzytkownik> uzytkownicy;
        private const string FilePath = "archiwum.json";

        public Archiwum()
        {
            dokumenty = new List<Dokument>();
            uzytkownicy = new List<Uzytkownik>();
            WczytajDane();
            if (!uzytkownicy.Any(u => u.NazwaUzytkownika == "root"))
            {
                DodajUzytkownika(new Uzytkownik("root", "", "root"));
            }
        }

        public void DodajDokument(Dokument dokument)
        {
            dokumenty.Add(dokument);
            ZapiszDane();
        }

        public void UsunDokument(Guid uuid)
        {
            var dokument = dokumenty.FirstOrDefault(d => d.UUID == uuid);
            if (dokument != null)
            {
                dokumenty.Remove(dokument);
                ZapiszDane();
            }
        }

        public void ModyfikujDokument(Guid uuid, Dokument nowyDokument, string nazwaUzytkownika)
        {
            var dokument = dokumenty.FirstOrDefault(d => d.UUID == uuid);
            if (dokument != null)
            {
                dokument.Tytul = nowyDokument.Tytul;
                dokument.Rok = nowyDokument.Rok;
                dokument.Kategoria = nowyDokument.Kategoria;
                dokument.MiejscePrzechowywania = nowyDokument.MiejscePrzechowywania;
                dokument.LiczbaEgzemplarzy = nowyDokument.LiczbaEgzemplarzy;
                dokument.DodajZmiane(new HistoriaZmian("Dokument zmodyfikowany", DateTime.Now, nazwaUzytkownika));
                ZapiszDane();
            }
        }

        public Dokument SzukajDokumentuPoTytule(string tytul)
        {
            return dokumenty.FirstOrDefault(d => d.Tytul.Equals(tytul, StringComparison.OrdinalIgnoreCase));
        }

        public List<Dokument> SzukajDokumentowPoRoku(int rok)
        {
            return dokumenty.Where(d => d.Rok == rok).ToList();
        }

        public List<Dokument> SzukajDokumentowPoMiejscu(string miejsce)
        {
            return dokumenty.Where(d => d.MiejscePrzechowywania.Equals(miejsce, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Dokument> PobierzWszystkieDokumenty()
        {
            return dokumenty;
        }

        public List<Uzytkownik> PobierzWszystkichUzytkownikow()
        {
            return uzytkownicy;
        }

        public void DodajUzytkownika(Uzytkownik uzytkownik)
        {
            uzytkownicy.Add(uzytkownik);
            ZapiszDane();
        }

        public Uzytkownik SzukajUzytkownikaPoNazwie(string nazwaUzytkownika)
        {
            return uzytkownicy.FirstOrDefault(u => u.NazwaUzytkownika.Equals(nazwaUzytkownika, StringComparison.OrdinalIgnoreCase));
        }

        private void ZapiszDane()
        {
            var dane = new { Dokumenty = dokumenty, Uzytkownicy = uzytkownicy };
            string json = JsonConvert.SerializeObject(dane, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        private void WczytajDane()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                var dane = JsonConvert.DeserializeObject<Dane>(json);
                dokumenty = dane.Dokumenty;
                uzytkownicy = dane.Uzytkownicy;
            }
        }

        private class Dane
        {
            public List<Dokument> Dokumenty { get; set; }
            public List<Uzytkownik> Uzytkownicy { get; set; }
        }
    }
}
