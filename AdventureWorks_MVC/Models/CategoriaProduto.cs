using System;
using System.ComponentModel.DataAnnotations;
using AdventureWorks_MVC.Base.Model;

namespace AdventureWorks_MVC.Models
{
    public class CategoriaProduto : Entidade
    {

        [Required(ErrorMessageResourceType = typeof(AdventureWorksResource.Models), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(AdventureWorksResource.Models), Name = "CategoriaProduto_Nome")]
        public string Nome { get; set; }

        [Display(ResourceType = typeof(AdventureWorksResource.Models), Name = "DataModificacao")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DataModificacao { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}