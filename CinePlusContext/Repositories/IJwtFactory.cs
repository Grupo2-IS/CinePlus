
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using CinePlus.Entities;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace CinePlus.Context.Repositories
{
    public interface IJwtFactory
    {

        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);

        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);

    }
}
