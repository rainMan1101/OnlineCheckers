using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Queries
{
    public interface ISingleResultQuery<TContext, TResult>
    {
        TResult Ask(TContext context);
    }

    public interface ISingleResultQuery<TResult>
    {
        TResult Ask();
    }
}
