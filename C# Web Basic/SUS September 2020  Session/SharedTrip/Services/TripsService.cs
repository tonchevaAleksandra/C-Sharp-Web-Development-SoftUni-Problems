using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SharedTrip.Data;
using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using SharedTrip.ViewModels.Users;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void Create(string startPoint, string endPoint, string departureTime, string imagePath, int seats, string description)
        {
            var trip = new Trip()
            {
                DepartureTime = DateTime.ParseExact(departureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Description = description,
                ImagePath = imagePath,
                Seats = (byte)seats,
                StartPoint = startPoint,
                EndPoint = endPoint
            };

            this.db.Trips.Add(trip);
            this.db.SaveChanges();
        }

        public bool IsDepartureTimeValid(string departureTime)
        {

            DateTime date;
            var isValid = DateTime.TryParseExact(departureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            return isValid;
        }

        public AllTripsViewModel GetAllTrips()
        {
            var trips = this.db.Trips.OrderBy(x=>x.StartPoint).ThenBy(x=>x.EndPoint).ToList().Where(x=>HasAvailableSeats(x.Id)).Select(x => new TripViewModel()
            {
                DepartureTimeAsDateTime = x.DepartureTime,
                Description = x.Description,
                EndPoint = x.EndPoint,
                Id = x.Id,
                ImagePath = x.ImagePath,
                Seats =(int)(x.Seats - x.UserTrips.Count),
                StartPoint = x.StartPoint
            }).ToList();

            return new AllTripsViewModel() { Trips = trips };
        }

        public TripViewModel GetDetailsForTrip(string tripId)
        {
            return this.db.Trips.ToList().Where(x => x.Id == tripId && HasAvailableSeats(tripId)).Select(x => new TripViewModel()
            {
                Description = x.Description,
                DepartureTimeAsDateTime = x.DepartureTime,
                EndPoint = x.EndPoint,
                Id = x.Id,
                ImagePath = x.ImagePath,
                Seats = (int)(x.Seats - x.UserTrips.Count),
                StartPoint = x.StartPoint
            }).FirstOrDefault();
        }

        public bool HasAvailableSeats(string tripId)
        {
            var trip = this.db.Trips.Where(x => x.Id == tripId)
                .Select(x => new
                {
                    x.Seats,
                    TakenSeats = x.UserTrips.Count,
                    x.UserTrips
                }).FirstOrDefault();

            return trip.Seats - trip.TakenSeats > 0;
        }

        public bool AddUserToTrip(string userId, string tripId)
        {
            if (this.db.Trips.Find(tripId).UserTrips.Any(x => x.UserId == userId))
            {
                return false;
            }

            var userTrip = new UserTrip()
            {
                UserId = userId,
                TripId = tripId
            };

            this.db.UserTrips.Add(userTrip);
            this.db.SaveChanges();

            return true;
        }

        public ICollection<UsersToTripViewModel> GetAllUsersToCurrentTrip(string tripId)
        {
            var users = this.db.UserTrips.Where(x => x.TripId == tripId).Select(x => new UsersToTripViewModel()
                {
                    Username = x.User.Username,
                    Email = x.User.Email
                })
                .OrderBy(x => x.Username)
                .ToList();

            return users;
        }
    }
}
