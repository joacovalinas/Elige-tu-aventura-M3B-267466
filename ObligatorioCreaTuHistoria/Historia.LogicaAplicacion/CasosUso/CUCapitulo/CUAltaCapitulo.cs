using DTOs.DTO.DTOsCapitulo;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUCapitulo;
using EscogeAventura.LogicaNegocios.CustomExceptions.Genericas;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using EscogeAventura.LogicaNegocios.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaAplicacion.CasosUso.CUCapitulo
{
    public class CUAltaCapitulo : ICUAltaCapitulo
    {

        private IRepositorioCapitulo _repoCapitulo;
        private IRepositorioHistoria _repoHistoria;

        public CUAltaCapitulo(IRepositorioCapitulo repoCapitulo, IRepositorioHistoria repoHistoria)
        {
            _repoCapitulo = repoCapitulo;
            _repoHistoria = repoHistoria;
        }


        public void Ejecutar(DTOAltaCapitulo dto)
        {

            {
                if (dto == null)
                    throw new DominioException("Los datos del capítulo son obligatorios.");

                // 1) Historia
                Historia historia = _repoHistoria.FindById(dto.HistoriaId);
                if (historia == null)
                    throw new DominioException("La historia no existe.");

                if(dto.Titulo == historia.Capitulos.FirstOrDefault()?.Titulo)
                    throw new DominioException("Ya existe un capítulo con ese título en esta historia.");

                // 2) Antecedente: se busca entre los capítulos de ESTA historia, así que
                //    un id de otra historia (o inexistente) no aparece. Esto cubre la regla
                //    "no se comparten capítulos entre historias".
                Capitulo antecedente = historia.Capitulos.FirstOrDefault(c => c.Id == dto.AntecedenteId);
                if (antecedente == null)
                    throw new DominioException("El capítulo antecedente no existe en esta historia.");

                // 3) Solo se puede continuar a partir de capítulos intermedios
                Intermedio origen = antecedente as Intermedio;
                if (origen == null)
                    throw new DominioException("Solo se pueden agregar capítulos a continuación de capítulos intermedios.");

                // 4) Texto de la opción
                if (string.IsNullOrWhiteSpace(dto.TextoOpcion))
                    throw new DatoVacioException("El texto de la opción es obligatorio.");

                // 5) Crear el capítulo según el tipo
                Capitulo nuevo;
                if (dto.TipoCapitulo == "Final")
                {
                    nuevo = new Final
                    {
                        Titulo = dto.Titulo?.Trim(),
                        Cuerpo = dto.Cuerpo?.Trim(),
                        CantidadDeVecesAlcanzado = 0
                    };
                }
                else
                {
                    nuevo = new Intermedio
                    {
                        Titulo = dto.Titulo?.Trim(),
                        Cuerpo = dto.Cuerpo?.Trim()
                    };
                }

                // Valida título y cuerpo (el Validar() que ya tenés en Capitulo/Intermedio/Final)
                nuevo.Validar();

                // 6) Crear la opción que une el antecedente con el capítulo nuevo
                Opcion opcion = new Opcion
                {
                    Texto = dto.TextoOpcion.Trim(),
                    CapituloOrigen = origen,
                    CapituloDestino = nuevo
                };

                // 7) Recién acá, con todo validado, se enlaza y se guarda.
                //    Si alguna validación anterior falla, la historia queda intacta.
                nuevo.Historia = historia;
          
                historia.Capitulos.Add(nuevo);
                origen.Opciones.Add(opcion);

                _repoHistoria.Actualizar(historia);

                // RF05: acá registrarías la Auditoria (acción "Alta de capítulo", historia, fecha, adminId).
            }

        }




    }
}
