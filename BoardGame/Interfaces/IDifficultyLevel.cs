using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Interfaces
{

    public enum DifficultyLevel
    {
        Impossible,
        Extreme,
        Hard,
        Medium,
        Easy,
        VeryEasy
    }

    public interface IDifficultyLevel
    {
        DifficultyLevel Level { get; }
    }
}
