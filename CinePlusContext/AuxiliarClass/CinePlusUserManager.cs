using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using CinePlus.Entities;
using CinePlus.Context;
using System;

namespace CinePlus.Context
{
 public class  CinePlusUserManager: UserManager<User>
 {
    public CinePlusUserManager(IuserStore<User> store):base(store)
    {
    }
//faltan argumentos
    public static CinePlusUserManager Create(IdentityFactoryOptions<CinePlusUserManager>options, IOwinsContext context)
    {
        var appDbContext = Context.Get<CinePlusDb>();
        var appUsermanager = new CinePlusUserManager(new UserStore<User>(appDbContext)); //faltan coistas
        
        return appUsermanager;

    }
 
 }
}