using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;
using CinePlus.Context;
using System;
using System.Linq;

namespace CinePlus.Context.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private CinePlusDb dbContext;

        public RequestRepository(CinePlusDb base_d_datos)
        {

            this.dbContext = base_d_datos;
        }

        public async Task<int> GetEntradasPorDia(int dia, int mes, int año)
        {
            PurchaseRepository reponormalpurchase = new PurchaseRepository(this.dbContext);
            var entradas = await reponormalpurchase.RetrieveAllAsync();
            var query = entradas.Where(s => s.ShowingStart.Day == dia
             && s.ShowingStart.Month == mes && s.ShowingStart.Year == año);

            PurchaseRepository repomemberpurchase = new PurchaseRepository(this.dbContext);
            var entradasdesocios = await repomemberpurchase.RetrieveAllAsync();
            var querysocios = entradasdesocios.Where(y => y.ShowingStart.Day == dia
             && y.ShowingStart.Month == mes && y.ShowingStart.Year == año);

            int Compras = query.Count();
            int ComprasSocios = querysocios.Count();
            int TotalDCompras = Compras + ComprasSocios;
            return TotalDCompras;

        }

        public async Task<int> GetEntradasPorMes(int mes, int año)
        {
            PurchaseRepository repopurchase = new PurchaseRepository(this.dbContext);
            var entradas = await repopurchase.RetrieveAllAsync();
            var query = entradas.Where(s => s.ShowingStart.Month == mes
             && s.ShowingStart.Year == año);

            int compras = query.Count();
            return compras;

        }


        public async Task<int> GetEntradasPoraño(int year)
        {
            PurchaseRepository reponormalpurchase = new PurchaseRepository(this.dbContext);
            var entradas = await reponormalpurchase.RetrieveAllAsync();
            var query = entradas.Where(s => s.ShowingStart.Year == year);

            PurchaseRepository repomemberpurchase = new PurchaseRepository(this.dbContext);
            var entradasdesocios = await repomemberpurchase.RetrieveAllAsync();
            var querysocios = entradasdesocios.Where(r => r.ShowingStart.Year == year);

            int Compras = query.Count();
            int ComprasSocios = querysocios.Count();
            int TotalDCompras = Compras + ComprasSocios;
            return TotalDCompras;
        }

        public async Task<int> GetEntradasPorPelicula(int idfilm)
        {
            PurchaseRepository reponormalpurchase = new PurchaseRepository(this.dbContext);
            var entradas = await reponormalpurchase.RetrieveAllAsync();
            var query = entradas.Where(s => s.FilmID == idfilm);

            PurchaseRepository repomemberpurchase = new PurchaseRepository(this.dbContext);
            var entradasdesocios = await repomemberpurchase.RetrieveAllAsync();
            var querysocios = entradasdesocios.Where(r => r.FilmID == idfilm);

            int Compras = query.Count();
            int ComprasSocios = querysocios.Count();
            int TotalDCompras = Compras + ComprasSocios;
            return TotalDCompras;
        }



        public async Task<IEnumerable<Film>> GetFilmsRating()
        {
            FilmRepository repofilm = new FilmRepository(this.dbContext);
            var peliculas = await repofilm.RetrieveAllAsync();
            var query = peliculas.OrderBy(o => o.Rating);

            return query;
        }



    }
}