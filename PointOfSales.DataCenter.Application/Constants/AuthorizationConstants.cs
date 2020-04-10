using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataCenter.Application.Constants
{
    public class AuthorizationConstants
    {
        public enum Roles
        {
            Administrator,
            Manager,
            Operator
        }
        public const Roles baseRole = Roles.Operator;

        public const string default_username = "iammukeshm";
        public const string default_email = "iammukeshm@gmail.com";
        public const string default_password = "Pa$$w0rd.";
        public const Roles default_role = Roles.Administrator;
    }
}
