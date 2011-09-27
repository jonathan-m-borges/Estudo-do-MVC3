using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using MyResources;
using RentCar.Validation;

namespace RentCar.Models
{

    [Bind(Exclude = "Status, Locacoes")]
    public class Carro : Entidade
    {
        public Carro()
        {
            Status = StatusCarro.Liberado;
            Locacoes = new List<Locacao>();
        }

        [Required]
        [RegularExpression("^[A-Z]{3}-\\d{4}$")]
        [Remote("ValidarPlaca", "Carros", AdditionalFields = "Id")]
        public string Placa { get; set; }

        [Required]
        [Display(Name = "Número de portas")]
        public int NumeroPortas { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        [ScaffoldColumn(true)]
        [UIHint("DropDownListBox")]
        public Marca Marca { get; set; }

        public string Cor { get; set; }

        [ScaffoldColumn(false)]
        public StatusCarro Status { get; private set; }

        [ScaffoldColumn(false)]
        public List<Locacao> Locacoes { get; private set; }

        public void Alugar(Locacao locacao)
        {
            if (!PodeAlugar(locacao)) return;
            Status = StatusCarro.Alugado;
            Locacoes.Add(locacao);
        }

        private bool PodeAlugar(Locacao locacao)
        {
            return Locacoes.Count(x => !(x.Inicio > locacao.Fim || x.Fim < locacao.Inicio)) == 0;
        }

        public void Devolver(Locacao locacao)
        {
            if (Locacoes.Remove(locacao))
                Status = StatusCarro.Liberado;
        }

    }

    public enum StatusCarro { Alugado, Liberado }
}