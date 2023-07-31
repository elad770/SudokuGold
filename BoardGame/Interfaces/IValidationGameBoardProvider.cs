using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame.UserControls.Interfaces
{
    public interface IValidationGameBoardProvider
    {
        /// <summary>
        /// Validate a value by an index in the board, the checks if the value in the current position (index) is valid in terms of the row, column and 3x3 submatrix.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="indexes">The row and column indexes of the value in the board.</param>
        /// <returns>true If the value is a valid value according to the row column and submatrix in board; otherwise, false.</returns>
        bool IsBoardValid(int[] parms);

        /// <summary>
        /// Returns the valid move index for the specified row or column.
        /// </summary>
        /// <param name="row_or_col">The index of the row or column.</param>
        /// <returns>The valid move index based on the provided row or column index.</returns>
        // int GetMoveValidInBorad(int row_or_col);
        //bool IsBoardValid(int[] parms);


        List<int[]> GetIndexesLocationError(int[] prmas);

    }
}
