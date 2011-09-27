namespace AdventureWorks_MVC.Base.Model
{
    public class Pagina
    {
        public Pagina(int numeroDaPagina, int registrosPorPagina, int totalDeRegistrosEncontrados = 0)
        {
            NumeroDaPagina = numeroDaPagina;
            RegistrosPorPagina = registrosPorPagina;
            TotalDeRegistrosEncontrados = totalDeRegistrosEncontrados;
        }

        public int NumeroDaPagina { get; set; }

        public int RegistrosPorPagina { get; set; }

        public int TotalDeRegistrosEncontrados { get; set; }

    }
}