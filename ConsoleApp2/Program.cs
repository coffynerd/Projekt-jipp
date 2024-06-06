using System;
using DokumentArchiwum.Builders;
using DokumentArchiwum.Interfaces;
using DokumentArchiwum.Models;
using DokumentArchiwum.Services;

class Program
{
    static void Main(string[] args)
    {
        IArchiwum archiwum = new Archiwum();
        Uzytkownik zalogowanyUzytkownik = null;

        while (true)
        {
            if (zalogowanyUzytkownik == null)
            {
                zalogowanyUzytkownik = EkranLogowania(archiwum);
                if (zalogowanyUzytkownik == null)
                {
                    Console.WriteLine("Nieprawidłowa nazwa użytkownika.");
                    continue;
                }
            }

            Console.WriteLine($"Zalogowany jako: {zalogowanyUzytkownik.NazwaUzytkownika}");
            Console.WriteLine("1. Dodaj dokument");
            Console.WriteLine("2. Usuń dokument");
            Console.WriteLine("3. Modyfikuj dokument");
            Console.WriteLine("4. Szukaj dokumentu po tytule");
            Console.WriteLine("5. Szukaj dokumentów po roku");
            Console.WriteLine("6. Szukaj dokumentów po miejscu przechowywania");
            Console.WriteLine("7. Wyświetl wszystkie dokumenty");
            Console.WriteLine("8. Wyświetl wszystkich użytkowników");
            Console.WriteLine("9. Dodaj użytkownika");
            Console.WriteLine("0. Wyloguj");
            Console.Write("Wybierz opcję: ");

            int wybor = int.Parse(Console.ReadLine());
            switch (wybor)
            {
                case 1:
                    DodajDokument(archiwum, zalogowanyUzytkownik);
                    break;
                case 2:
                    UsunDokument(archiwum);
                    break;
                case 3:
                    ModyfikujDokument(archiwum, zalogowanyUzytkownik);
                    break;
                case 4:
                    SzukajDokumentuPoTytule(archiwum);
                    break;
                case 5:
                    SzukajDokumentowPoRoku(archiwum);
                    break;
                case 6:
                    SzukajDokumentowPoMiejscu(archiwum);
                    break;
                case 7:
                    WyswietlWszystkieDokumenty(archiwum);
                    break;
                case 8:
                    WyswietlWszystkichUzytkownikow(archiwum);
                    break;
                case 9:
                    DodajUzytkownika(archiwum);
                    break;
                case 0:
                    zalogowanyUzytkownik = null;
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    break;
            }
        }
    }

    static Uzytkownik EkranLogowania(IArchiwum archiwum)
    {
        Console.Write("Podaj nazwę użytkownika: ");
        string nazwaUzytkownika = Console.ReadLine();
        return archiwum.SzukajUzytkownikaPoNazwie(nazwaUzytkownika);
    }

    static void DodajDokument(IArchiwum archiwum, Uzytkownik uzytkownik)
    {
        DokumentBuilder builder = new DokumentBuilder();

        Console.Write("Podaj tytuł: ");
        builder.UstawTytul(Console.ReadLine());

        Console.Write("Podaj rok: ");
        builder.UstawRok(int.Parse(Console.ReadLine()));

        Console.Write("Podaj kategorię: ");
        builder.UstawKategoria(Console.ReadLine());

        Console.Write("Podaj miejsce przechowywania: ");
        builder.UstawMiejscePrzechowywania(Console.ReadLine());

        Console.Write("Podaj liczbę egzemplarzy: ");
        builder.UstawLiczbaEgzemplarzy(int.Parse(Console.ReadLine()));

        Dokument dokument = builder.Zbuduj();
        dokument.DodajZmiane(new HistoriaZmian("Dokument dodany", DateTime.Now, uzytkownik.NazwaUzytkownika));
        archiwum.DodajDokument(dokument);
        Console.WriteLine("Dokument dodany.");
    }

    static void UsunDokument(IArchiwum archiwum)
    {
        Console.Write("Podaj UUID dokumentu do usunięcia: ");
        Guid uuid = Guid.Parse(Console.ReadLine());
        archiwum.UsunDokument(uuid);
        Console.WriteLine("Dokument usunięty.");
    }

    static void ModyfikujDokument(IArchiwum archiwum, Uzytkownik uzytkownik)
    {
        Console.Write("Podaj UUID dokumentu do modyfikacji: ");
        Guid uuid = Guid.Parse(Console.ReadLine());

        DokumentBuilder builder = new DokumentBuilder();

        Console.Write("Podaj nowy tytuł: ");
        builder.UstawTytul(Console.ReadLine());

        Console.Write("Podaj nowy rok: ");
        builder.UstawRok(int.Parse(Console.ReadLine()));

        Console.Write("Podaj nową kategorię: ");
        builder.UstawKategoria(Console.ReadLine());

        Console.Write("Podaj nowe miejsce przechowywania: ");
        builder.UstawMiejscePrzechowywania(Console.ReadLine());

        Console.Write("Podaj nową liczbę egzemplarzy: ");
        builder.UstawLiczbaEgzemplarzy(int.Parse(Console.ReadLine()));

        Dokument nowyDokument = builder.Zbuduj();
        archiwum.ModyfikujDokument(uuid, nowyDokument, uzytkownik.NazwaUzytkownika);
        Console.WriteLine("Dokument zmodyfikowany.");
    }

    static void SzukajDokumentuPoTytule(IArchiwum archiwum)
    {
        Console.Write("Podaj tytuł: ");
        string tytul = Console.ReadLine();
        var dokument = archiwum.SzukajDokumentuPoTytule(tytul);
        if (dokument != null)
        {
            WyswietlDokument(dokument);
        }
        else
        {
            Console.WriteLine("Dokument nie znaleziony.");
        }
    }

    static void SzukajDokumentowPoRoku(IArchiwum archiwum)
    {
        Console.Write("Podaj rok: ");
        int rok = int.Parse(Console.ReadLine());
        var dokumenty = archiwum.SzukajDokumentowPoRoku(rok);
        foreach (var dokument in dokumenty)
        {
            WyswietlDokument(dokument);
        }
    }

    static void SzukajDokumentowPoMiejscu(IArchiwum archiwum)
    {
        Console.Write("Podaj miejsce przechowywania: ");
        string miejsce = Console.ReadLine();
        var dokumenty = archiwum.SzukajDokumentowPoMiejscu(miejsce);
        foreach (var dokument in dokumenty)
        {
            WyswietlDokument(dokument);
        }
    }

    static void WyswietlWszystkieDokumenty(IArchiwum archiwum)
    {
        var dokumenty = archiwum.PobierzWszystkieDokumenty();
        foreach (var dokument in dokumenty)
        {
            WyswietlDokument(dokument);
        }
    }

    static void WyswietlWszystkichUzytkownikow(IArchiwum archiwum)
    {
        var uzytkownicy = archiwum.PobierzWszystkichUzytkownikow();
        foreach (var uzytkownik in uzytkownicy)
        {
            Console.WriteLine($"Imię: {uzytkownik.Imie}, Nazwisko: {uzytkownik.Nazwisko}, UUID: {uzytkownik.UUID}, Nazwa Użytkownika: {uzytkownik.NazwaUzytkownika}");
        }
    }

    static void DodajUzytkownika(IArchiwum archiwum)
    {
        Console.Write("Podaj imię: ");
        string imie = Console.ReadLine();

        Console.Write("Podaj nazwisko: ");
        string nazwisko = Console.ReadLine();

        Console.Write("Podaj nazwę użytkownika: ");
        string nazwaUzytkownika = Console.ReadLine();

        Uzytkownik nowyUzytkownik = new Uzytkownik(imie, nazwisko, nazwaUzytkownika);
        archiwum.DodajUzytkownika(nowyUzytkownik);
        Console.WriteLine("Użytkownik dodany.");
    }

    static void WyswietlDokument(Dokument dokument)
    {
        Console.WriteLine($"Tytuł: {dokument.Tytul}, Rok: {dokument.Rok}, UUID: {dokument.UUID}, Kategoria: {dokument.Kategoria}, Miejsce Przechowywania: {dokument.MiejscePrzechowywania}, Liczba Egzemplarzy: {dokument.LiczbaEgzemplarzy}");
        Console.WriteLine("Historia zmian:");
        foreach (var zmiana in dokument.HistoriaZmian)
        {
            Console.WriteLine($"- {zmiana.Opis}, Data: {zmiana.DataZmiany}, Użytkownik: {zmiana.Uzytkownik}");
        }
    }
}
