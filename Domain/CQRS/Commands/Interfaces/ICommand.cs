using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Commands
{
    public interface ICommand<TContext>
    {
        void Execute(TContext context);
    }
}
