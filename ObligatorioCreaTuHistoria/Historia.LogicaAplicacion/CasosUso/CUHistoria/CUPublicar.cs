using DTOs.DTO.DTOsHistoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.Enums;
using EscogeAventura.LogicaNegocios.IRepositorios;
using EscogeAventura.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUHistoria
{
    public class CUPublicar : ICUPublicar
    {
        private IRepositorioHistoria _repoHistorias;
        private ICUAltaAuditoria cuAltaAuditoria;
        public CUPublicar(IRepositorioHistoria historias, ICUAltaAuditoria AltaAudi)
        {
            _repoHistorias = historias;
            cuAltaAuditoria = AltaAudi;

        }
        public void Ejecutar(DTOHistoria dto, int usuId)
        {
               Historia  historia = _repoHistorias.FindById(dto.Id);

            foreach (Intermedio capitulo in historia.Capitulos.OfType<Intermedio>())
            {
                // Todo intermedio con al menos una opción
                if (capitulo.Opciones == null || capitulo.Opciones.Count == 0)
                {
                    throw new DominioException($"El capítulo '{capitulo.Titulo}' no tiene opciones.");
                }

                // Toda opción hacia otro capítulo (intermedio o final)
                foreach (Opcion opcion in capitulo.Opciones)
                {
                    if (opcion.CapituloDestino == null)
                        throw new DominioException($"Una opción del capítulo '{capitulo.Titulo}' no lleva a ningún capítulo.");
                    else if (opcion.CapituloDestino.Id == capitulo.Id)
                        throw new DominioException($"Una opción del capítulo '{capitulo.Titulo}' lleva al mismo capítulo.");
                }
            }




            historia.Estado = EstadoHistoria.Publicado;
                _repoHistorias.Actualizar(historia);
            cuAltaAuditoria.Ejecutar("publicar Historia", historia.Id, usuId);
        }
    }
}
