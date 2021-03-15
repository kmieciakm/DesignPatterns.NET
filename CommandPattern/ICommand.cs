using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern
{
    /// <summary>
    /// Base command.
    /// </summary>
    public interface ICommand
    {
        void Execute();
    }
}
