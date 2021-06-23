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
    public class SeatControllerTest
    {
        [Fact]
        public async Task GetSeatsTest()
        {
            CinePlusDb db = new CinePlusDb();
            Seat seat1 = new Seat { SeatID = 1, RoomID = 1, Row = 1, Column = 2};
            Seat seat2 = new Seat { SeatID = 2, RoomID = 1, Row = 1, Column = 1 };
            Seat seat3 = new Seat { SeatID = 3, RoomID = 2, Row = 2, Column = 2 };

            SeatRepository repo = new SeatRepository(db);
            await repo.CreateAsync(seat1);
            await repo.CreateAsync(seat2);
            await repo.CreateAsync(seat3);

            SeatsController controller = new SeatsController(repo);

            var ans = controller.GetSeats().Result.ToList();

            Assert.Equal(3, ans.Count);

        }

        [Fact]
        public async Task GetSeatTest()
        {
            CinePlusDb db = new CinePlusDb();
            Seat seat1 = new Seat { SeatID = 1, RoomID = 1, Row = 1, Column = 2 };
            Seat seat2 = new Seat { SeatID = 2, RoomID = 1, Row = 1, Column = 1 };
            Seat seat3 = new Seat { SeatID = 3, RoomID = 2, Row = 2, Column = 2 };

            SeatRepository repo = new SeatRepository(db);
            await repo.CreateAsync(seat1);
            await repo.CreateAsync(seat2);
            await repo.CreateAsync(seat3);

            SeatsController controller = new SeatsController(repo);

            var ans1 = controller.GetSeat(10, 10).Result as StatusCodeResult;
            var ans2 = controller.GetSeat(2, 1).Result as ObjectResult;

            Assert.Equal(404, ans1.StatusCode);
            Assert.Equal(200, ans2.StatusCode);
        }
        
    
    }
}
