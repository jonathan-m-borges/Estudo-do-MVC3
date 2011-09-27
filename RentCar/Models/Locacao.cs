using System;
using System.ComponentModel.DataAnnotations;
using RentCar.Validation;

namespace RentCar.Models
{

    public class Locacao : Entidade
    {
        public Locacao()
        {
        }

        public Locacao(string pessoa, DateTime inicio, DateTime fim, Carro carro)
        {
            Pessoa = pessoa;
            Inicio = inicio;
            Fim = fim;
            Carro = carro;
        }

        [Required]
        public string Pessoa { get; set; }

        [Required]
        //TODO implementar custom attribute para validacao DateLessThan
        public DateTime Inicio { get; set; }

        [Required]
        [DateGreaterThan("Inicio", ErrorMessageResourceType = typeof(MyResources.Resource), ErrorMessageResourceName = "DateGreaterThan")]
        public DateTime Fim { get; set; }

        [Required]
        //TODO implementar DropDownListFor...
        public Carro Carro { get; set; }
    }
}