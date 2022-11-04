using Confitec.Usuarios.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Usuarios.Core.Data
{
    public interface IRepository<T> : IDisposable where  T : Entity 
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
