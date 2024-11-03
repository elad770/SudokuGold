using SudokuGame.UserControls.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BoardGame.Interfaces
{
    public interface ICombinedGameBoardProvider : IGameBoardProvider, IValidationGameBoardProvider
    {
       int[,] Borad { get; set; }

    }

}
