using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilevineLibrary
{
    public enum OpType
    {
        Client,
        Project,
        Operating,
        Trust,
        Timefee,
        Address
    }

    public enum OpCommand
    {
        Insert,
        Update,
        Delete
    }
}
