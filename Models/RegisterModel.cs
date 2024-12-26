using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet_WebAPI_doc.Models
{
    public class RegisterModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}