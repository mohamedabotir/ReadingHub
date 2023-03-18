using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cores.Patternes
{
    internal class EmptyObject<T>
    {
        public T? Empty() {
            return  default;
        }
    }
}
