using System;
using Xunit;
using CinePlus.Context.Repositories;
using CinePlus.Entities;
using CinePlus.Context;
using System.Collections.Generic;
using CinePlusServices.Controllers;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
namespace Unit_Tests
{
    public class RoomsControllerTest
    {
        [Fact]
        public async Task GetAllTest()
        {
            CinePlusDb db = new CinePlusDb();
            Room room1 = new Room
            {
                RoomID = 1,
                RoomName = "A",
            };
            Room room2= new Room
            {
                RoomID = 2,
                RoomName = "B",
            }; 
            Room room3 = new Room
            {
                RoomID = 3,
                RoomName = "C",
            };

            RoomRepository repo = new RoomRepository(db);
            await repo.CreateAsync(room1);
            await repo.CreateAsync(room2);
            await repo.CreateAsync(room3);

            RoomsController controller = new RoomsController(repo);

            var ans = controller.GetAll().Result.ToList();

            Assert.Equal(3, ans.Count);
        }

        [Fact]
        public async Task GetRoom()
        {
            CinePlusDb db = new CinePlusDb();
            Room room1 = new Room
            {
                RoomID = 1,
                RoomName = "A",
            };
            Room room2 = new Room
            {
                RoomID = 2,
                RoomName = "B",
            };
            Room room3 = new Room
            {
                RoomID = 3,
                RoomName = "C",
            };

            RoomRepository repo = new RoomRepository(db);
            await repo.CreateAsync(room1);
            await repo.CreateAsync(room2);
            await repo.CreateAsync(room3);

            RoomsController controller = new RoomsController(repo);

            var ans1 = controller.GetRoom(9).Result as StatusCodeResult;
            var ans2 = controller.GetRoom(2).Result as ObjectResult;

            Assert.Equal(404, ans1.StatusCode);
            Assert.Equal(200, ans2.StatusCode);

        }

    }
}
