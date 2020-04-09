﻿using PointOfSales.DataCenter.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataCenter.Domain.Entities
{
    public class ProductCategory : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Tenant { get; set; }
    }
}