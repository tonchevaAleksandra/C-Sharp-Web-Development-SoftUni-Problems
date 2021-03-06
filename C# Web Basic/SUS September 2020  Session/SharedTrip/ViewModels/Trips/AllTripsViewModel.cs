using System.Collections.Generic;

namespace SharedTrip.ViewModels.Trips
{
    public class AllTripsViewModel
    {
        public ICollection<TripViewModel> Trips { get; set; }
    }
}
