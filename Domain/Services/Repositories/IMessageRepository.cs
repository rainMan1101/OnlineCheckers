using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueTypes;

namespace Domain.Services.Repositories
{
    public interface IMessageRepository
    {
        IEnumerable<CMessage> GetAll(CGame game, IEnumerable<CUser> users, Int32 starId, Int32 count);

        void IsExist(Guid userId);

        void Add(CMessage message);
    }
}
