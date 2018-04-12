using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPV_Entidades.GeneralWS
{
    public class OE_EnviarCorreo
    {
        public String From { get; set; }
        public String To { get; set; }
        public String Cc { get; set; }
        public String Bcc { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Server { get; set; }
        public Int32 Port { get; set; }
    }
}
