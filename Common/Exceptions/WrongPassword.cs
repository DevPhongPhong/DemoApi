using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class WrongPassword : Exception
    {
        public override string Message => "Sai mật khẩu";
    }
}
