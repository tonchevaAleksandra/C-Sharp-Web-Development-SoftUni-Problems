using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SharedTrip.Data;
using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;

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
                DepartureTime = DateTime.Parse(departureTime, new CultureInfo("dd.MM.yyyy HH:mm")),
                Description = description,
                ImagePath = imagePath,
                Seats = (byte) seats,
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
            var trips = this.db.Trips.Select(x => new TripViewModel()
            {
                DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Description = x.Description,
                EndPoint = x.EndPoint,
                Id = x.Id,
                ImagePath = x.ImagePath,
                Seats = x.Seats,
                StartPoint = x.StartPoint
            }).ToList();

            return new AllTripsViewModel() {Trips = trips};
        }
    }
}
