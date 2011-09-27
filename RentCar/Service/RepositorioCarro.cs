using System;
using System.Collections.Generic;
using System.Linq;
using RentCar.Models;

namespace RentCar.Service
{
    public class RepositorioCarro : Repositorio<Carro>
    {
        public List<Carro> BuscarPorMarca(int idMarca)
        {
            return Lista.Where(x => x.Marca != null && x.Marca.Id == idMarca).ToList();
        }

    }
}