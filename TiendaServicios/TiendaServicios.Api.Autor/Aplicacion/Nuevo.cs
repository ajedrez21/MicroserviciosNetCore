using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento{ get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            // hace una referencia a la clase contextoautor
            public readonly ContextoAutor _contexto;
            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    FechaNacimiento = request.FechaNacimiento,
                    Apellido = request.Apellido
                };
                //agrego al contexto de autor libro el nuevo autor que se envio
                _contexto.AutorLibro.Add(autorLibro);
                //confirmar el cuardado del objeto dentro de la bd, va a realizar la accion en bd y va a devolver si fue confirmado la transaccion
                var valor = await _contexto.SaveChangesAsync();

                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("NO SE PUDO INSERTAR EL AUTOR DEL LIBRO");
            }
        }
    }
}
