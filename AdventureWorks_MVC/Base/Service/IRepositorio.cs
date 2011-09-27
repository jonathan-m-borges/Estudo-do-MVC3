using System;
using AdventureWorks_MVC.Base.Model;

namespace AdventureWorks_MVC.Base.Service
{
    public interface IRepositorio<T> where T : Entidade
    {
        T Salvar(T entidade);
        bool Excluir(int id);
        T BuscarPorId(int id);
        ListaPaginada<T> BuscarTodos(Func<T, bool> filtro = null, Func<T, object> ordem = null, Pagina pagina = null);
        int ContarTodos(Func<T, bool> filtro = null);
    }
}