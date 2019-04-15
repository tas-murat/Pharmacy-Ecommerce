using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eczane.WebUI.Tools
{
    interface IMessage
    {
        string name { get; set; }
        string email { get; set; }
        string subject { get; set; }
        string message { get; set; }

    }
}
