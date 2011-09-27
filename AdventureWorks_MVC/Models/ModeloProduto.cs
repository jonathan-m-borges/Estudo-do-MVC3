using System;
using System.ComponentModel.DataAnnotations;
using AdventureWorks_MVC.Base.Model;

namespace AdventureWorks_MVC.Models
{
    //[Bind(Exclude = "Categoria")]
    public class ModeloProduto : Entidade
    {
        [Required(ErrorMessageResourceType = typeof(AdventureWorksResource.Models), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(AdventureWorksResource.Models), Name = "ModeloProduto_Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(AdventureWorksResource.Models), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(AdventureWorksResource.Models), Name = "ModeloProduto_Descricao")]
        public string Descricao { get; set; }

        [Required(ErrorMessageResourceType = typeof(AdventureWorksResource.Models), ErrorMessageResourceName = "Required")]
        [Display(ResourceType = typeof(AdventureWorksResource.Models), Name = "Categoria")]
        public CategoriaProduto Categoria { get; set; }

        [Display(ResourceType = typeof(AdventureWorksResource.Models), Name = "DataModificacao")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DataModificacao { get; set; }

    }

}