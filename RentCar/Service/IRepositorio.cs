using System;
using System.Collections.Generic;
using RentCar.Models;

namespace RentCar.Service
{
    public interface IRepositorio<T> where T : Entidade
    {
        List<T> BuscarTodos();
        T Salvar(T entidade);
        T BuscarPorId(int id);
        void Deletar(int id);
        List<T> BuscarPorFiltro(Func<T, bool> filtro);
    }
}