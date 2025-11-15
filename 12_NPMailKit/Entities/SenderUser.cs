using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_NPMailKit.Entities
{
    public class SenderUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public SenderUser()
        {
            
        }
        public SenderUser(string l, string p)
        {
            Login = l;
            Password = p;
        }
    }
}
