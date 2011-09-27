using System;
using System.ComponentModel;
using System.Web.Mvc;
using AdventureWorks_MVC.Base.Service;
using Microsoft.Practices.ServiceLocation;

namespace AdventureWorks_MVC.Base.Binder
{
    public class EntidadeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (!string.IsNullOrEmpty(bindingContext.ModelName))
            {
                var idEntidade = ObterIdEntidade(bindingContext);
                if (idEntidade > 0)
                    return BuscarEntidadeDoRepositorio(bindingContext, idEntidade);
            }

            return base.BindModel(controllerContext, bindingContext);
        }

        private static int ObterIdEntidade(ModelBindingContext bindingContext)
        {
            var idInt = 0;

            var idEntidade = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (idEntidade != null)
            {
                idInt = (int)idEntidade.ConvertTo(typeof(int));
            }

            return idInt;
        }

        private static object BuscarEntidadeDoRepositorio(ModelBindingContext bindingContext, int idEntidade)
        {
            var type = typeof(IRepositorio<>);
            type = type.MakeGenericType(bindingContext.Model.GetType());
            dynamic repositorio = ServiceLocator.Current.GetInstance(type);

            var entidade = repositorio.BuscarPorId(idEntidade);
            return entidade;
        }

        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            Console.Write(propertyDescriptor);
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}