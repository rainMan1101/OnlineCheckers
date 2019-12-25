using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System;
using OnlineCheckers.Client.Models.Enums;

namespace OnlineCheckers.Client.Models
{
    public class CUserModel
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public EColor Color { get; set; }

        public CUserModel(Guid id, String name)
        {
            Id = id;
            Name = name;
        }
    }
}
