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
<<<<<<< HEAD
        void GenerateNewBoard(out int[,] borad, DifficultyLevel Level);

        void InitializeBoard(out int[,] board);


=======
        int[,] GetBoard(string difficulty);
        int[,] CloneBorad();
>>>>>>> 56301c45d9e64c002b8f6b05440fb3f9ad799768
    }
}
