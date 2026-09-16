using System;
using System.Collections.Generic;
using System.Text;

namespace Historia.LogicaNegocios.IRepositorios
{
    public interface IRepositorio
    {

        public interface IRepositorio<T> where T : class
        {
            void Add(T nuevo);

            List<T> FindAll();

            T FindById(int id);

            void Remove(T ToEliminar);

            void Update(T ToEditar);
        }
    }
}
