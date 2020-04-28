using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Circuit.Models.Circuits
{
    public interface IClonable<T>
    {
        T Clone();
    }
}
