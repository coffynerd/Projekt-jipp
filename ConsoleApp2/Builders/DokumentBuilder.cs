using DokumentArchiwum.Models;

namespace DokumentArchiwum.Builders
{
    public class DokumentBuilder
    {
        private string tytul;
        private int rok;
        private string kategoria;
        private string miejscePrzechowywania;
        private int liczbaEgzemplarzy;

        public DokumentBuilder UstawTytul(string tytul)
        {
            this.tytul = tytul;
            return this;
        }

        public DokumentBuilder UstawRok(int rok)
        {
            this.rok = rok;
            return this;
        }

        public DokumentBuilder UstawKategoria(string kategoria)
        {
            this.kategoria = kategoria;
            return this;
        }

        public DokumentBuilder UstawMiejscePrzechowywania(string miejscePrzechowywania)
        {
            this.miejscePrzechowywania = miejscePrzechowywania;
            return this;
        }

        public DokumentBuilder UstawLiczbaEgzemplarzy(int liczbaEgzemplarzy)
        {
            this.liczbaEgzemplarzy = liczbaEgzemplarzy;
            return this;
        }

        public Dokument Zbuduj()
        {
            return new Dokument(tytul, rok, kategoria, miejscePrzechowywania, liczbaEgzemplarzy);
        }
    }
}
