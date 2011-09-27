using System;
using System.Collections.Generic;
using AdventureWorks_MVC.Base.Model;

namespace AdventureWorks_MVC.Base.Service
{
    public class RepositorioFacade
    {

        private static RepositorioFacade instance;

        private readonly Dictionary<Type, object> repositorios;

        public static RepositorioFacade Instance
        {
            get
            {
                return instance ?? (instance = new RepositorioFacade());
            }
        }

        private RepositorioFacade()
        {
            repositorios = new Dictionary<Type, object>();
        }

        public object GetRepositorio(Type type)
        {
            if (!repositorios.ContainsKey(type))
            {
                var repositorio = Activator.CreateInstance(typeof(Repositorio<>).MakeGenericType(new[] { type }));
                repositorios.Add(type, repositorio);
            }

            return repositorios[type];
        }

    }
}