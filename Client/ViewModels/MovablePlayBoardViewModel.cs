using Domain.Entities;
using OnlineCheckers.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.ViewModels
{
    public class CMovablePlayBoardViewModel
    {
        public CPlayerModel PlayerModel { get; }

        public IEnumerable<CChecker> Checkers { get; }


        public CMovablePlayBoardViewModel(CPlayerModel playerModel, IEnumerable<CChecker> checkers)
        {
            PlayerModel = playerModel;
            Checkers = checkers;
        }

    }
}
