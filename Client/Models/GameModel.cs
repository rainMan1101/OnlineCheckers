using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.Models
{
    public class CGameModel : INotifyPropertyChanged
    {
        private DateTime _startMoving;

        private EGameState _state;


        public CGameModel(String name)
        {
            Name = name;
            _state = EGameState.Freeze;
        }


        public String Name { get; }

        public EGameState State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartMoving
        {
            get => _startMoving;
            set
            {
                _startMoving = value;
                OnPropertyChanged();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
