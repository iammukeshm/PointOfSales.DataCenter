using PointOfSales.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.Domain.Entities.People
{
    public class Person : AuditableEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NickName { get; set; }
        public string MobileNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] Image { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
