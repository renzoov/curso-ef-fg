using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCorePeliculas.Entidades.Seeding
{
    public static class SeedingModuloConsulta
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var accion = new Genero { Identificador = 1, Nombre = "Acción" };
            var animacion = new Genero { Identificador = 2, Nombre = "Animación" };
            var comedia = new Genero { Identificador = 3, Nombre = "Comedia" };
            var cienciaFiccion = new Genero { Identificador = 4, Nombre = "Ciencia Ficción" };
            var drama = new Genero { Identificador = 5, Nombre = "Drama" };

            modelBuilder.Entity<Genero>().HasData(accion, animacion, comedia, cienciaFiccion, drama);

            var tomHolland = new Actor { Id = 1, Nombre = "Tom Holland", FechaNacimiento = new DateTime(1996, 6, 1) };
            var samuelLJackson = new Actor { Id = 2, Nombre = "Samuel L. Jackson", FechaNacimiento = new DateTime(1948, 12, 21) };
            var robertDowneyJr = new Actor { Id = 3, Nombre = "Robert Downey Jr.", FechaNacimiento = new DateTime(1965, 4, 4) };
            var chrisEvans = new Actor { Id = 4, Nombre = "Chris Evans", FechaNacimiento = new DateTime(1981, 6, 13) };
            var laRoca = new Actor { Id = 5, Nombre = "Dwayne Johnson", FechaNacimiento = new DateTime(1972, 5, 2) };
            var auliCarvalho = new Actor { Id = 6, Nombre = "Auli'i Cravalho", FechaNacimiento = new DateTime(2000, 11, 22) };
            var scarlettJohansson = new Actor { Id = 7, Nombre = "Scarlett Johansson", FechaNacimiento = new DateTime(1984, 11, 22) };
            var keanuReeves = new Actor { Id = 8, Nombre = "Keanu Reeves", FechaNacimiento = new DateTime(1964, 9, 2) };

            modelBuilder.Entity<Actor>().HasData(tomHolland, samuelLJackson,
                robertDowneyJr, chrisEvans, laRoca, auliCarvalho, scarlettJohansson, keanuReeves);

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var agora = new Cine { Id = 1, Nombre = "Ágora", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.9388777, 18.4839232)) };
            var sambil = new Cine { Id = 2, Nombre = "Sambil", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.9118801, 18.482621)) };
            var megacentro = new Cine { Id = 3, Nombre = "Megacentro", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.856089, 18.506934)) };
            var acropolis = new Cine { Id = 4, Nombre = "Acropolis", Ubicacion = geometryFactory.CreatePoint(new Coordinate(-69.942166, 18.482787)) };

            var agoraCineOferta = new CineOferta { Id = 1, CineId = agora.Id, FechaInicio = DateTime.Today, FechaFin = DateTime.Today.AddDays(30), PorcentajeDescuento = 10 };

            var salaDeCine2DAgora = new SalaDeCine { Id = 1, CineId = agora.Id, Precio = 220, TipoSalaDeCine = TipoSalaDeCine.DosDimensiones };
            var salaDeCine3DAgora = new SalaDeCine { Id = 2, CineId = agora.Id, Precio = 320, TipoSalaDeCine = TipoSalaDeCine.TresDimensiones };
            var salaDeCine2DSambil = new SalaDeCine { Id = 3, CineId = sambil.Id, Precio = 200, TipoSalaDeCine = TipoSalaDeCine.DosDimensiones };
            var salaDeCine3DSambil = new SalaDeCine { Id = 4, CineId = sambil.Id, Precio = 290, TipoSalaDeCine = TipoSalaDeCine.TresDimensiones };
            var salaDeCine2DMegacentro = new SalaDeCine { Id = 5, CineId = megacentro.Id, Precio = 250, TipoSalaDeCine = TipoSalaDeCine.DosDimensiones };
            var salaDeCine3DMegacentro = new SalaDeCine { Id = 6, CineId = megacentro.Id, Precio = 330, TipoSalaDeCine = TipoSalaDeCine.TresDimensiones };
            var salaDeCineCXCMegacentro = new SalaDeCine { Id = 7, CineId = megacentro.Id, Precio = 450, TipoSalaDeCine = TipoSalaDeCine.CXC };
            var salaDeCine2DAcropolis = new SalaDeCine { Id = 8, CineId = acropolis.Id, Precio = 250, TipoSalaDeCine = TipoSalaDeCine.DosDimensiones };

            var acropolisCineOferta = new CineOferta { Id = 2, CineId = acropolis.Id, FechaInicio = DateTime.Today, FechaFin = DateTime.Today.AddDays(30), PorcentajeDescuento = 15 };

            modelBuilder.Entity<Cine>().HasData(agora, sambil, megacentro, acropolis);
            modelBuilder.Entity<CineOferta>().HasData(agoraCineOferta, acropolisCineOferta);
            modelBuilder.Entity<SalaDeCine>().HasData(salaDeCine2DAgora, salaDeCine3DAgora, salaDeCine2DSambil, salaDeCine3DSambil, salaDeCine2DMegacentro, salaDeCine3DMegacentro, salaDeCineCXCMegacentro, salaDeCine2DAcropolis);

            var avengers = new Pelicula { Id = 1, Titulo = "Avengers", EnCartelera = false, FechaEstreno = new DateTime(2012, 4, 11), PosterURL = "https://upload.wikimedia.org/wikipedia/en/8/8a/The_Avengers_%282012_film%29_poster.jpg" };

            var entidadGeneroPelicula = "GeneroPelicula";
            var generoIdPropiedad = "GenerosIdentificador";
            var peliculaIdPropiedad = "PeliculasId";

            var entidadSalaDeCinePelicula = "PeliculaSalaDeCine";
            var salaDeCineIdPropiedad = "SalasDeCineId";

            modelBuilder.Entity(entidadGeneroPelicula).HasData(
                new Dictionary<string, object> { [generoIdPropiedad] = accion.Identificador, [peliculaIdPropiedad] = avengers.Id },
                new Dictionary<string, object> { [generoIdPropiedad] = cienciaFiccion.Identificador, [peliculaIdPropiedad] = avengers.Id });

            var coco = new Pelicula()
            {
                Id = 2,
                Titulo = "Coco",
                EnCartelera = false,
                FechaEstreno = new DateTime(2017, 10, 22),
                PosterURL = "https://upload.wikimedia.org/wikipedia/en/9/98/Coco_%282017_film%29_poster.jpg"
            };

            modelBuilder.Entity(entidadGeneroPelicula).HasData(
                new Dictionary<string, object> { [generoIdPropiedad] = animacion.Identificador, [peliculaIdPropiedad] = coco.Id });


            var noWayHome = new Pelicula()
            {
                Id = 3,
                Titulo = "Spider-Man: No Way Home",
                EnCartelera = false,
                FechaEstreno = new DateTime(2021, 12, 17),
                PosterURL = "https://upload.wikimedia.org/wikipedia/en/0/00/Spider-Man_No_Way_Home_poster.jpg"
            };

            modelBuilder.Entity(entidadGeneroPelicula).HasData(
                new Dictionary<string, object> { [generoIdPropiedad] = accion.Identificador, [peliculaIdPropiedad] = noWayHome.Id },
                new Dictionary<string, object> { [generoIdPropiedad] = comedia.Identificador, [peliculaIdPropiedad] = noWayHome.Id },
                new Dictionary<string, object> { [generoIdPropiedad] = cienciaFiccion.Identificador, [peliculaIdPropiedad] = noWayHome.Id });

            var farFromHome = new Pelicula()
            {
                Id = 4,
                Titulo = "Spider-Man: Far From Home",
                EnCartelera = false,
                FechaEstreno = new DateTime(2019, 7, 2),
                PosterURL = "https://upload.wikimedia.org/wikipedia/en/0/00/Spider-Man_No_Way_Home_poster.jpg"
            };

            modelBuilder.Entity(entidadGeneroPelicula).HasData(
                new Dictionary<string, object> { [generoIdPropiedad] = accion.Identificador, [peliculaIdPropiedad] = farFromHome.Id },
                new Dictionary<string, object> { [generoIdPropiedad] = comedia.Identificador, [peliculaIdPropiedad] = farFromHome.Id },
                new Dictionary<string, object> { [generoIdPropiedad] = cienciaFiccion.Identificador, [peliculaIdPropiedad] = farFromHome.Id });

            var theMatrixResurrections = new Pelicula()
            {
                Id = 5,
                Titulo = "The Matrix Resurrections",
                EnCartelera = true,
                FechaEstreno = DateTime.Today,
                PosterURL = "https://upload.wikimedia.org/wikipedia/en/5/50/The_Matrix_Resurrections.jpg"
            };

            modelBuilder.Entity(entidadGeneroPelicula).HasData(
                new Dictionary<string, object> { [generoIdPropiedad] = accion.Identificador, [peliculaIdPropiedad] = theMatrixResurrections.Id },
                new Dictionary<string, object> { [generoIdPropiedad] = drama.Identificador, [peliculaIdPropiedad] = theMatrixResurrections.Id },
                new Dictionary<string, object> { [generoIdPropiedad] = cienciaFiccion.Identificador, [peliculaIdPropiedad] = theMatrixResurrections.Id });

            modelBuilder.Entity(entidadSalaDeCinePelicula).HasData(
                new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCine2DSambil.Id, [peliculaIdPropiedad] = theMatrixResurrections.Id },
                new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCine3DSambil.Id, [peliculaIdPropiedad] = theMatrixResurrections.Id },
                new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCine2DAgora.Id, [peliculaIdPropiedad] = theMatrixResurrections.Id },
                new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCine3DAgora.Id, [peliculaIdPropiedad] = theMatrixResurrections.Id },
                new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCine2DMegacentro.Id, [peliculaIdPropiedad] = theMatrixResurrections.Id },
                new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCine3DMegacentro.Id, [peliculaIdPropiedad] = theMatrixResurrections.Id },
                new Dictionary<string, object> { [salaDeCineIdPropiedad] = salaDeCineCXCMegacentro.Id, [peliculaIdPropiedad] = theMatrixResurrections.Id });

            var keanuReevesMatrix = new PeliculaActor
            {
                ActorId = keanuReeves.Id,
                PeliculaId = theMatrixResurrections.Id,
                Orden = 1,
                Personaje = "Neo"
            };

            var avengersChrisEvans = new PeliculaActor
            {
                ActorId = chrisEvans.Id,
                PeliculaId = avengers.Id,
                Orden = 1,
                Personaje = "Capitán América"
            };

            var avengersRobertDowney = new PeliculaActor
            {
                ActorId = robertDowneyJr.Id,
                PeliculaId = avengers.Id,
                Orden = 2,
                Personaje = "Iron Man"
            };

            var avengersScarlettJohansson = new PeliculaActor
            {
                ActorId = scarlettJohansson.Id,
                PeliculaId = avengers.Id,
                Orden = 3,
                Personaje = "Black Widow"
            };

            var tomHollandFFH = new PeliculaActor
            {
                ActorId = tomHolland.Id,
                PeliculaId = farFromHome.Id,
                Orden = 1,
                Personaje = "Peter Parker"
            };

            var tomHollandNWH = new PeliculaActor
            {
                ActorId = tomHolland.Id,
                PeliculaId = noWayHome.Id,
                Orden = 1,
                Personaje = "Peter Parker"
            };

            var samuelJacksonFFH = new PeliculaActor
            {
                ActorId = samuelLJackson.Id,
                PeliculaId = farFromHome.Id,
                Orden = 2,
                Personaje = "Samuel L. Jackson"
            };

            modelBuilder.Entity<Pelicula>().HasData(avengers, coco, noWayHome, farFromHome, theMatrixResurrections);

            modelBuilder.Entity<PeliculaActor>().HasData(samuelJacksonFFH, tomHollandFFH, tomHollandNWH, avengersRobertDowney,
                avengersScarlettJohansson, avengersChrisEvans, keanuReevesMatrix);
        }
    }
}
