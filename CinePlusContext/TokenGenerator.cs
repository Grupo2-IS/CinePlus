using System;
using CinePlus.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft;
using CinePlus.Context.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Linq;



namespace CinePlus.Context
{
    public class TokenGenerator

    {
        public static async Task<string> GenerateJwt(

          ClaimsIdentity identity,

          IJwtFactory jwtFactory,

          string userName,

          JwtOptions jwtOptions,

          JsonSerializerSettings serializerSettings)
        {

            var response = new

            {

                id = identity.Claims.Single(c => c.Type == "id").Value,

                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),

                expires_in = (int)jwtOptions.ValidFor.TotalSeconds

            };

            return JsonConvert.SerializeObject(response, serializerSettings);

        }

    }
}