using CompanyWebAppUI.Models;
using DataAccess.Models;

namespace CompanyWebAppUI.ViewModels
{
    public class VacationViewModel
    {
        private Vacation _vacation;

        public VacationViewModel(Vacation vacation)
        {
            _vacation = vacation;
        }

        public VacationModel GetVacationModel()
        {
            return new VacationModel()
            {
                DocId = _vacation.DocId,
                DocDate = _vacation.DocDate != null ? _vacation.DocDate.Value.ToShortDateString() : null,
                StartDate = _vacation.StartDate != null ? _vacation.StartDate.Value.ToShortDateString() : null,
                EndDate = _vacation.EndDate != null ? _vacation.EndDate.Value.ToShortDateString() : null,
                Name = _vacation.Name,
                Hours = _vacation.Hours,
            };
        }
    }
}
