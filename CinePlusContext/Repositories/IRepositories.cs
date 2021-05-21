using System.Collections.Generic;
using System.Threading.Tasks;
using CinePlus.Entities;

/// <summary>
/// Set of repositories interfaces that implement IRepository<T> for any Entity.
/// </summary>
namespace CinePlus.Context.Repositories
{
    public interface IArtistRepository : IRepository<Artist>
    {
    }

    public interface IDirectorRepository : IRepository<Director>
    {
    }

    public interface IFilmRepository : IRepository<Film>
    {
    }

    public interface IMemberRepository : IRepository<Member>
    {
    }
    public interface IMemberPurchaseRepository : IRepository<MemberPurchase>
    {
    }
    public interface INormalPurchaseRepository : IRepository<NormalPurchase>
    {
    }
    public interface IPerformerRepository : IRepository<Performer>
    {
    }
    public interface IRoomRepository : IRepository<Room>
    {
    }
    public interface ISeatRepository : IRepository<Seat>
    {
    }

    public interface IShowingRepository : IRepository<Showing>
    {
    }

    public interface IUserRepository : IRepository<User>
    {
    }

}