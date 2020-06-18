using System;
using System.Collections.Generic;
using System.Text;

namespace apical.Models.Authorization
{
    public class AuthResponseModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTimeOffset InsertDate { get; set; }
        public DateTimeOffset LastLogin { get; set; }
    }
}
