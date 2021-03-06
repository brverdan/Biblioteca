﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Account
{
    public interface IContaIdentityManager
    {
        Task<SignInResult> Login(string email, string password);
        Task Logout();
    }
}
