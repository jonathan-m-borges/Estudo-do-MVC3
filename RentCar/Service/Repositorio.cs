using System;
using System.Collections.Generic;
using System.Linq;
using RentCar.Models;

namespace RentCar.Service
{
    public class Repositorio<T> where T : Entidade
    {
        protected readonly List<T> Lista = new List<T>();

        public virtual List<T> BuscarTodos()
        {
            return Lista;
        }

        public T Salvar(T entidade)
        {
            if (entidade.Id == 0)
            {
                entidade.Id = GerarId();
                Lista.Add(entidade);
            }

            return entidade;
        }

        public T BuscarPorId(int id)
        {
            return Lista.FirstOrDefault(entidade => entidade.Id == id);
        }

        public void Deletar(int id)
        {
            var entidadeADeletar = BuscarPorId(id);
            Lista.Remove(entidadeADeletar);
        }

        private int GerarId()
        {
            if (Lista.Count > 0)
            {
                var ultimaEntidade = Lista[Lista.Count - 1];
                return ultimaEntidade.Id + 1;
            }

            return 1;
        }

        public List<T> BuscarPorFiltro(Func<T, bool> filtro)
        {
            return Lista.Where(filtro).ToList();
        }

    }
}
