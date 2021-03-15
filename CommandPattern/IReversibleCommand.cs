using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern
{
    /// <summary>
    /// Specifies a command that can be rolled back.
    /// </summary>
    public interface IReversibleCommand : ICommand
    {
        void Undo();
    }
}
