using System;
using System.Collections.Generic;
using DokumentArchiwum.Models;

namespace DokumentArchiwum.Interfaces
{
    public interface IArchiwum
    {
        void DodajDokument(Dokument dokument);
        void UsunDokument(Guid uuid);
        void ModyfikujDokument(Guid uuid, Dokument nowyDokument, string nazwaUzytkownika);
        Dokument SzukajDokumentuPoTytule(string tytul);
        List<Dokument> SzukajDokumentowPoRoku(int rok);
        List<Dokument> SzukajDokumentowPoMiejscu(string miejsce);
        List<Dokument> PobierzWszystkieDokumenty();
        List<Uzytkownik> PobierzWszystkichUzytkownikow();
        void DodajUzytkownika(Uzytkownik uzytkownik);
        Uzytkownik SzukajUzytkownikaPoNazwie(string nazwaUzytkownika);
    }
}
