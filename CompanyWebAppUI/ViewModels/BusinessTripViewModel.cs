using DataAccess.Models;
using CompanyWebAppUI.Models;

namespace CompanyWebAppUI.ViewModels
{
    public class BusinessTripViewModel
    {
        private readonly BusinessTrip _trip;

        public BusinessTripViewModel(BusinessTrip trip)
        {
            _trip = trip;
        }

        public BusinessTripModel GetBusinessTripModel()
        {
            return new BusinessTripModel()
            {
                StartDate = _trip.StartDate != null ? _trip.StartDate.Value.ToShortDateString() : null,
                EndDate = _trip.EndDate != null ? _trip.EndDate.Value.ToShortDateString() : null,
                DocDate = _trip.DocDate != null ? _trip.DocDate.Value.ToShortDateString() : null,
                DocId = _trip.DocId,
                Destination = _trip.Destination,
                Description = _trip.Description,
            };
        }
    }
}
