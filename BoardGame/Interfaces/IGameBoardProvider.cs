using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.UserControls.Interfaces
{
    public interface IGameBoardProvider
    {
        int[,] GetBoard(string difficulty);
    }
}
