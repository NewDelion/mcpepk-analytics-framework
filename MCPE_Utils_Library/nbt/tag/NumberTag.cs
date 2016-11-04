using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPE_Utils_Library.nbt.tag
{
    public interface INumberTag<T>
    {
        T getData();
        void setData(T data);
    }
}
