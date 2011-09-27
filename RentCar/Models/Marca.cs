using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class Marca : Entidade
    {
        [Required(ErrorMessageResourceType = typeof(MyResources.Resource), ErrorMessageResourceName = "Required")]
        public string Nome { get; set; }

        public byte[] Logo { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}