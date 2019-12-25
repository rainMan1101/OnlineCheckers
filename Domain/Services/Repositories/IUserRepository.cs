using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services.Repositories
{
    public interface IUserRepository
    {
        CUser Get(Guid Id);

        void Add(CUser user);

        void Delete(Guid id);

        void MarkToDelete(Guid id); 

        void DeleteMarksUsers();
    }
}
