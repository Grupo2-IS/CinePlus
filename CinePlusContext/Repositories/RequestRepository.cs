using System.Collections.Generic;
using System.Link;
using System.Threading.Tasks;
using CinePlus.Entities;
using CinePlus.Context;
using System;

namespace CinePlus.Context.Repositories
{
    public interface RequestRepository:IRequestRepository
    {
             private CinePlusDb dbContext;

             public RequestRepository(CinePlusDb base_d_datos)
             {

                this.dbContext=base_d_datos; 
             }

             public async Task<int> GetEntradasPorDia(int dia, int mes , int año)
             {
                 NormalPurchaseRepository reponormalpurchase=new NormalPurchaseRepository(this.dbContext);
                var entradas =await reponormalpurchase.RetrieveAllAsync();
                var query= entradas.Select(Count(s=>new NormalPurchase())).Where(e=>e.ShowingStart.Day==dia
                &&e.ShowingStart.Month==mes&&e.ShowingStart.Year==año)

                  MemberPurchaseRepository repomemberpurchase=new MemberPurchaseRepository(this.dbContext);
                var entradasdesocios =await repomemberpurchase.RetrieveAllAsync();
                var querysocios= entradasdesocios.Select(Count(r=>new MemberPurchase())).Where(y=>y.ShowingStart.Day==dia
                &&y.ShowingStart.Month==mes&&y.ShowingStart.Year==año)

                return query+querysocios;

             }
              
              public async Task<int>  GetEntradasPorMes(int mes,int año)
             {
                 NormalPurchaseRepository reponormalpurchase=new NormalPurchaseRepository(this.dbContext);
                var entradas =await reponormalpurchase.RetrieveAllAsync();
                var query= entradas.Select(Count(s=>new NormalPurchase())).Where(e=>e.ShowingStart.Month==mes
                && e.ShowingStart.Year=año)

                  MemberPurchaseRepository repomemberpurchase=new MemberPurchaseRepository(this.dbContext);
                var entradasdesocios =await repomemberpurchase.RetrieveAllAsync();
                var querysocios= entradasdesocios.Select(Count(r=>new MemberPurchase())).Where(y=>y.ShowingStart.Month==mes
                &&y.ShowingStart.Year==año)

                return query+querysocios;

             }
             

             public async Task<int> GetEntradasPoraño(int year )
             {
               NormalPurchaseRepository reponormalpurchase=new NormalPurchaseRepository(this.dbContext);
                var entradas =await reponormalpurchase.RetrieveAllAsync();
                var query= entradas.Select(Count(s=>new NormalPurchase())).Where(e=>e.ShowingStart.Year==year)

                  MemberPurchaseRepository repomemberpurchase=new MemberPurchaseRepository(this.dbContext);
                var entradasdesocios =await repomemberpurchase.RetrieveAllAsync();
                var querysocios= entradasdesocios.Select(Count(r=>new MemberPurchase())).Where(y=>y.ShowingStart.Year==year)

                return query+querysocios;
             }

            public async Task<int> GetEntradasPorPelicula(int idfilm)
             {
               NormalPurchaseRepository reponormalpurchase=new NormalPurchaseRepository(this.dbContext);
                var entradas =await reponormalpurchase.RetrieveAllAsync();
                var query= entradas.Select(Count(s=>new NormalPurchase())).Where(e=>e.FilmID==idfilm)

                  MemberPurchaseRepository repomemberpurchase=new MemberPurchaseRepository(this.dbContext);
                var entradasdesocios =await repomemberpurchase.RetrieveAllAsync();
                var querysocios= entradasdesocios.Select(Count(r=>new MemberPurchase())).Where(y=>y.FilmID==idfilm)

                return query+querysocios;
             }
             public async Task<int> GetEntradasPorGenero(string genero)
             {
               NormalPurchaseRepository reponormalpurchase=new NormalPurchaseRepository(this.dbContext);
                var entradas =await reponormalpurchase.RetrieveAllAsync();
                var query= entradas.Select(Count(s=>new NormalPurchase())).Where(e=>e.FilmID.genero==genero)

                  MemberPurchaseRepository repomemberpurchase=new MemberPurchaseRepository(this.dbContext);
                var entradasdesocios =await repomemberpurchase.RetrieveAllAsync();
                var querysocios= entradasdesocios.Select(Count(r=>new MemberPurchase())).Where(y=>y.FilmID.genero==genero)

                return query+querysocios;
             }
            


      

    }
}