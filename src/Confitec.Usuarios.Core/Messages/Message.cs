using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Usuarios.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
