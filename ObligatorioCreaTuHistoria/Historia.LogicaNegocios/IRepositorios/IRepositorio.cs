using System;
using System.Collections.Generic;
using System.Text;

namespace EscogeAventura.LogicaNegocios.IRepositorios
{
    public interface IRepositorio
    {

        public interface IRepositorio<T> where T : class
        {
            void Add(T nuevo);
            void Update(T entidad);
            void Remove(T ToEliminar);

            List<T> FindAll();

            T FindById(int id);

            
        }
    }
}
