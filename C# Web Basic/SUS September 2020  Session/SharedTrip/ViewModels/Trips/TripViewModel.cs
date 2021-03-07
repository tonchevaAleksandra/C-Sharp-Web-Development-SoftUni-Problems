using System;
using System.Globalization;

namespace SharedTrip.ViewModels.Trips
{
    public class TripViewModel
    {
        public string Id { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public DateTime DepartureTimeAsDateTime { get; set; }
        public string DepartureTime => this.DepartureTimeAsDateTime.ToString(CultureInfo.GetCultureInfo("bg-BG"));
        public string DepartureDetails => this.DepartureTimeAsDateTime.ToString("s");
        public int Seats { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}
