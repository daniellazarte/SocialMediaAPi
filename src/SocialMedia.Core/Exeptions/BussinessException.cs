using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Exeptions
{
    public class BussinessException: Exception
    {
        public BussinessException()
        {

        }
        public BussinessException(string message): base(message)
        {

        }
    }
}
