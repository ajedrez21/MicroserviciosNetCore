using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Consulta
    {
        public class ListarAutor : IRequest<List<AutorLibro>>
        {
                    
        }

        public class Manejador : IRequestHandler<ListarAutor, List<AutorLibro>>
        {
            private readonly ContextoAutor _contexto;
            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }
            public async Task<List<AutorLibro>> Handle(ListarAutor request, CancellationToken cancellationToken)
            {
               var autores = await _contexto.AutorLibro.ToListAsync();

               return autores;
            }
        }
    }
}
