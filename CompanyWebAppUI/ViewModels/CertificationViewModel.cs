using DataAccess.Models;
using CompanyWebAppUI.Models;

namespace CompanyWebAppUI.ViewModels
{
    public class CertificationViewModel
    {
        private Certification _certification;

        public CertificationViewModel(Certification certification)
        {
            _certification = certification;
        }

        public CertificationModel GetCertificationModel()
        {
            return new CertificationModel()
            {
                Date = _certification.Date != null ? _certification.Date.Value.ToShortDateString() : null,
                Name = _certification.Name,
                Description = _certification.Description,
            };
        }
    }
}
