using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataCenter.Domain.Settings
{
    public class SmtpSettings
    {
        public string FromMailAddress { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port{ get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
    }
}
