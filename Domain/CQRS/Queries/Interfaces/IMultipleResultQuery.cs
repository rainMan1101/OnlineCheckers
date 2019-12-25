using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Queries
{
    public interface IMultipleResultQuery<TContext, TResult>
    {
        IEnumerable<TResult> Ask(TContext context);
    }

    public interface IMultipleResultQuery<TResult>
    {
        IEnumerable<TResult> Ask();
    }
}
