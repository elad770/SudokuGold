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
        void GenerateNewBoard(out int[,] borad, DifficultyLevel Level);

        void InitializeBoard(out int[,] board);


    }
}
