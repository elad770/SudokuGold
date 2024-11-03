using BoardGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SudokuGame.UserControls.Interfaces
{
    public interface IGameBoardProvider
    {

        void InitializeBoard(/*out int[,] board*/);

        void GenerateNewBoard(/*out int[,] borad*/);
        //int[,] CloneBorad();

    }
}
