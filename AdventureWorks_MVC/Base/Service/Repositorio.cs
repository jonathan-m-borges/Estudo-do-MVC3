using System;
using System.Collections.Generic;
using System.Linq;
using AdventureWorks_MVC.Base.Model;

namespace AdventureWorks_MVC.Base.Service
{
    public class Repositorio<T> : IRepositorio<T> where T : Entidade
    {
        protected readonly IList<T> Lista = new List<T>();

        public T Salvar(T entidade)
        {
            if (entidade.Id == 0)
                entidade.Id = GerarId();
            else
                Excluir(entidade.Id);

            Lista.Add(entidade);
            return entidade;
        }

        public bool Excluir(int id)
        {
            var entidadeADeletar = BuscarPorId(id, false);
            if (entidadeADeletar == null)
                return false;
            Lista.Remove(entidadeADeletar);
            return true;
        }

        public T BuscarPorId(int id)
        {
            return BuscarPorId(id, true);
        }

        private T BuscarPorId(int id, bool clone = true)
        {
            var obj = Lista.FirstOrDefault(entidade => entidade.Id == id);
            if (!clone) return obj;
            return obj != null ? (T)obj.Clone() : null;
        }

        public ListaPaginada<T> BuscarTodos(Func<T, bool> filtro = null, Func<T, object> ordem = null, Pagina pagina = null)
        {
            var lista = filtro == null ? Lista : Lista.Where(filtro).ToList();

            if (ordem == null)
                ordem = x => x.Id;

            lista = lista.OrderBy(ordem).ToList();

            if (pagina != null)
            {
                lista = lista.Skip((pagina.NumeroDaPagina - 1) * pagina.RegistrosPorPagina).Take(pagina.RegistrosPorPagina).ToList();
                pagina.TotalDeRegistrosEncontrados = ContarTodos(filtro);
            }
            else
            {
                var total = ContarTodos(filtro);
                pagina = new Pagina(1, total, total);
            }

            return new ListaPaginada<T>(lista, pagina);
        }

        public int ContarTodos(Func<T, bool> filtro = null)
        {
            return filtro != null ? Lista.Count(filtro) : Lista.Count;
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
    }
}