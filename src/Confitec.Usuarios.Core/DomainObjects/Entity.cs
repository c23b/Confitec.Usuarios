using Confitec.Usuarios.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Usuarios.DomainObjects
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        private List<Event> _notificacoes;
        public IReadOnlyCollection<Event> Notificacoes => _notificacoes?.AsReadOnly();

        //public void AdicionarEvento(Event evento)
        //{
        //    _notificacoes = _notificacoes ?? new List<Event>();
        //    _notificacoes.Add(evento);
        //}

        //public void RemoverEvento(Event eventItem)
        //{
        //    _notificacoes?.Remove(eventItem);
        //}

        //public void LimparEventos()
        //{
        //    _notificacoes?.Clear();
        //}
    }
}
