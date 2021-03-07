using System;
using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void Create(string startPoint, string endPoint, string departureTime, string imagePath, int seats,
            string description);

        bool IsDepartureTimeValid(string departureTime);

        AllTripsViewModel GetAllTrips();

        TripViewModel GetDetailsForTrip(string tripId);
    }
}
