using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Utilities
{
    public enum ModeCellForegroundColor
    {
        Normal,
        ReadOnly,
        Error,
    }

    /// <summary>
    /// Represents the background color modes for cells.
    /// </summary>
    public enum ModeCellBackgroundColor
    {
        /// <summary>
        /// Mode without focus of textbox or not related to a textbox row/column/submatrix.
        /// </summary>
        Without,

        /// <summary>
        /// Mode when the focus is on the current textbox.
        /// </summary>
        Focus,

        /// <summary>
        /// Mode when the textbox is in a row/column/sub-matrix that is related to a textbox in focus.
        /// </summary>
        Related,

        /// <summary>
        /// Mode of a textbox that is not in focus and is not related to a row/column/submatrix
        /// but the value in it is the same as the textbox in focus.
        /// </summary>
        SameValue,

        /// <summary>
        /// Mode of a textbox that is not in focus and is also related to a row/column/submatrix
        /// but the value in it is the same as the textbox in focus.
        /// </summary>
        SameValueError,
    }

}
