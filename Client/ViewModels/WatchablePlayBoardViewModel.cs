using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.ViewModels
{
    public class CWatchablePlayBoardViewModel
    {
        public ObservableCollection<CChecker> Checkers { get; }

        public CWatchablePlayBoardViewModel(ObservableCollection<CChecker> checkers)
        {
            Checkers = checkers;
        }

    }
}
