﻿using Microsoft.AspNetCore.Identity;

namespace CourierManagementSystem.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Role { get; set; } // Admin, Staff, Customer
    }
}

