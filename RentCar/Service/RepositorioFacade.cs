using RentCar.Models;

namespace RentCar.Service
{
    public class RepositorioFacade
    {
        private static RepositorioFacade instance;

        public RepositorioMarca RepositorioMarca { get; private set; }
        public RepositorioCarro RepositorioCarro { get; private set; }

        private RepositorioFacade()
        {
            RepositorioMarca = new RepositorioMarca();
            RepositorioCarro = new RepositorioCarro();
        }

        public static RepositorioFacade Instance
        {
            get
            {
                return instance ?? (instance = new RepositorioFacade());
            }
        }

    }
}