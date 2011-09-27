using System;
using AdventureWorks_MVC.Base.Service;
using AdventureWorks_MVC.Models;

namespace AdventureWorks_MVC.Service
{
    public static class RepositorioDados
    {
        public static void PopularDados()
        {
            for (var i = 1; i <= 35; i++)
            {
                var categoria = new CategoriaProduto
                                           {
                                               Nome = string.Format("Categoria {0}", i),
                                               DataModificacao = DateTime.Now
                                           };
                RepositorioFacade.Instance.RepositorioCategoriaProduto.Salvar(categoria);

                for (var j = 1; j < 15; j++)
                {
                    var indice = i * 15 + j;
                    RepositorioFacade.Instance.RepositorioModeloProduto.Salvar(
                        new ModeloProduto
                        {
                            Nome = string.Format("Produto {0}", indice),
                            Descricao = string.Format("Descrição do Produto {0}", indice),
                            Categoria = categoria,
                            DataModificacao = DateTime.Now
                        });
                }
            }
        }

    }
}