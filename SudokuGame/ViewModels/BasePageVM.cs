using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    [AddINotifyPropertyChangedInterface]
    public class BasePageVM
    {
        public Action ComebackToMainWindow { get; set; }

        public BasePageVM(Action comebackPage)
        {
            ComebackToMainWindow = comebackPage;
        }
       // public void ComebackToMainWindow() => ComebackPage();
    }
}
