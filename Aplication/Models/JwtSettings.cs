﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Aplication.Models
{
    public class JwtSettings
    {
        public string? Secret { get; set; }

        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public double DurationInMinutes { get; set; }
        public TimeSpan ExpireTime { get; set; }



    }
}
