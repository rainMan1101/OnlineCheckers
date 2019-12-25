using System;

namespace Domain.Entities
{
    public class CUser
    {
        public Guid Id { get; }

        public String Name { get; }

        public EColor Color { get; }

        public CUser(Guid id, String name)
        {
            Id = id;
            Name = name;
        }

        public CUser(CUser user)
        {
            Id = user.Id;
            Name = user.Name;
        }
    }
}
