using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations;

namespace DMCProject1.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
