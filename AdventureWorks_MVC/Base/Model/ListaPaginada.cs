using System.Collections.Generic;

namespace AdventureWorks_MVC.Base.Model
{
    public class ListaPaginada<T> where T : Entidade
    {
        public ListaPaginada(IList<T> lista, Pagina pagina)
        {
            Lista = lista;
            Pagina = pagina;
        }

        public IList<T> Lista { get; private set; }

        public Pagina Pagina { get; private set; }

    }
}