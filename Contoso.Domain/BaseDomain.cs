﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contoso.Domain
{
    public class BaseDomain
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
