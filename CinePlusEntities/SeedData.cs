using CinePlus_.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinePlus_.Models
{
    public class SeedData
    {

         public static async Task AddRoles(IServiceProvider serviceProvider, IConfiguration configuration)
       
 {
            RoleManager<IdentityRole> roleManager =
  serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
var rol = new List<IdentityRole> { new IdentityRole("Admin"), new IdentityRole("Partner") };
            for (int i = 0; i < rol.Count; i++)
            {
                if (await roleManager.FindByNameAsync(rol[i].Name) == null)
                {
                    await roleManager.CreateAsync(rol[i]);
                }
            }

        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CinePlusDBContext(serviceProvider.GetRequiredService<DbContextOptions<CinePlusDBContext>>()))
            {

                //#############################################################################################################################
                //#########################-------------Películas----------------################################################################
                //#############################################################################################################################
                Pelicula p1 = new Pelicula
                {
                    Titulo = "Titanic",
                    Genero = "Drama",
                    Time = new DateTime(1000)
                };
                Pelicula p2 = new Pelicula
                {
                    Titulo = "Harry Potter y la piedra filosofal",
                    Genero = "Ciencia y ficcion",
                    Time = new DateTime(1000)
                };
                Pelicula p3 = new Pelicula
                {
                    Titulo = "Saw 1",
                    Genero = "Terror",
                    Time = new DateTime(1000)
                };
                Pelicula p4 = new Pelicula
                {
                    Titulo = "Piratas del Caribe",
                    Genero = "Ciencia y ficcion",
                    Time = new DateTime(1000)
                };
                Pelicula p5 = new Pelicula
                {
                    Titulo = "Duro de matar",
                    Genero = "Accion",
                    Time = new DateTime(1000)
                };
                Pelicula p6 = new Pelicula
                {
                    Titulo = "Maestros de la estafa",
                    Genero = "Accion",
                    Time = new DateTime(1000)
                };
                Pelicula p7 = new Pelicula
                {
                    Titulo = "El Padrino",
                    Genero = "Accion",
                    Time = new DateTime(1000)
                };
                Pelicula p8 = new Pelicula
                {
                    Titulo = "Doble Vida",
                    Genero = "Comedia",
                    Time = new DateTime(1000)
                };
                Pelicula p9 = new Pelicula
                {
                    Titulo = "Aladin",
                    Genero = "Fantasia",
                    Time = new DateTime(1000)
                };
                Pelicula p10 = new Pelicula
                {
                    Titulo = "Mision Imposible",
                    Genero = "Accion",
                    Time = new DateTime(1000)
                };

                if (!context.Pelicula.Any())
                {
                    context.Pelicula.AddRange(
                    p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);


                    context.SaveChanges();
                }
                //#############################################################################################################################
                //#########################-------------Sala----------------###################################################################
                //#############################################################################################################################
                Sala s1 = new Sala
                {
                    
                };
                Sala s2 = new Sala
                {
                   
                };
                Sala s3 = new Sala
                {
                   
                };
                Sala s4 = new Sala
                {
                   
                };

                if (!context.Sala.Any())
                {
                    context.Sala.AddRange(
                    s1,s2,s3,s4);


                    context.SaveChanges();
                }

                //#############################################################################################################################
                //#########################-------------Cartelera----------------###################################################################
                //#############################################################################################################################
                CartelDeTransmision ct1 = new CartelDeTransmision
                {
                    Pelicula = p1,
                    IdPelicula = p1.Titulo,
                    IdSala = s1.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 16, 0, 0),
                    AsientosReservados = new TipoDAsiento [10,10]
                    
                };
                CartelDeTransmision ct2 = new CartelDeTransmision
                {

                    Pelicula = p2,
                    IdPelicula = p2.Titulo,
                    IdSala = s2.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 16, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };
                CartelDeTransmision ct3 = new CartelDeTransmision
                {

                    Pelicula = p3,
                    IdPelicula = p3.Titulo,
                    IdSala = s3.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 16, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };
                CartelDeTransmision ct4 = new CartelDeTransmision
                {

                    Pelicula = p4,
                    IdPelicula = p4.Titulo,
                    IdSala = s4.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 16, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };
                CartelDeTransmision ct5 = new CartelDeTransmision
                {

                    Pelicula = p5,
                    IdPelicula = p5.Titulo,
                    IdSala = s1.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 18, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };
                CartelDeTransmision ct6 = new CartelDeTransmision
                {
                    Pelicula = p6,
                    IdPelicula = p6.Titulo,
                    IdSala = s2.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 18, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };
                CartelDeTransmision ct7 = new CartelDeTransmision
                {
                    Pelicula = p7,
                    IdPelicula = p7.Titulo,
                    IdSala = s3.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 18, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };
                CartelDeTransmision ct8 = new CartelDeTransmision
                {
                    Pelicula = p8,
                    IdPelicula = p8.Titulo,
                    IdSala = s4.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 18, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };

                CartelDeTransmision ct9 = new CartelDeTransmision
                {
                    Pelicula = p1,
                    IdPelicula = p1.Titulo,
                    IdSala = s1.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 20, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };

                CartelDeTransmision ct10 = new CartelDeTransmision
                {
                    Pelicula = p2,
                    IdPelicula = p2.Titulo,
                    IdSala = s2.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 20, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };
                CartelDeTransmision ct11 = new CartelDeTransmision
                {
                    Pelicula = p3,
                    IdPelicula = p3.Titulo,
                    IdSala = s3.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 20, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };
                CartelDeTransmision ct12 = new CartelDeTransmision
                {
                    Pelicula = p4,
                    IdPelicula = p4.Titulo,
                    IdSala = s4.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 20, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };
                CartelDeTransmision ct13 = new CartelDeTransmision
                {
                    Pelicula = p5,
                    IdPelicula = p5.Titulo,
                    IdSala = s1.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 22, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };
                CartelDeTransmision ct14 = new CartelDeTransmision
                {
                    Pelicula = p6,
                    IdPelicula = p6.Titulo,
                    IdSala = s2.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 22, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };

                CartelDeTransmision ct15 = new CartelDeTransmision
                {
                    Pelicula = p9,
                    IdPelicula = p9.Titulo,
                    IdSala = s3.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 22, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]
                };
                CartelDeTransmision ct16 = new CartelDeTransmision
                {
                    Pelicula = p10,
                    IdPelicula = p10.Titulo,
                    IdSala = s4.IdSala,
                    DateTime = new DateTime(2021, 9, 26, 22, 0, 0),
                    AsientosReservados = new TipoDAsiento[10, 10]


                };


                if (!context.CartelDeTransmision.Any())
                {
                    context.CartelDeTransmision.AddRange(
                    ct1,ct2,ct3,ct4,ct5,ct6,ct7,ct8,ct9,ct10,ct11,ct12,ct13,ct14,ct15,ct16);


                    context.SaveChanges();
                }





            }

        }
    }
}