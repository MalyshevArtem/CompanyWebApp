using DataAccess.Models;
using CompanyWebAppUI.Models;

namespace CompanyWebAppUI.ViewModels
{
    public class SickLeaveViewModel
    {
        private readonly SickLeave _sickLeave;
        public SickLeaveViewModel(SickLeave sickLeave)
        {
            _sickLeave = sickLeave;
        }

        public SickLeaveModel GetSickLeaveModel()
        {
            return new SickLeaveModel()
            {
                DocId = _sickLeave.DocId,
                DocDate = _sickLeave.DocDate != null ? _sickLeave.DocDate.Value.ToShortDateString() : null,
                StartDate = _sickLeave.StartDate != null ? _sickLeave.StartDate.Value.ToShortDateString() : null,
                EndDate = _sickLeave.EndDate != null ? _sickLeave.EndDate.Value.ToShortDateString() : null,
                Name = _sickLeave.Name,
            };
        }
    }
}
