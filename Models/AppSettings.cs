﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apical
{
    public class Settings
    {
        public Credentials Credentials { get; set; }
    }
    public class Cred
    {
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
