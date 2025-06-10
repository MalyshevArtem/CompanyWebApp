using DataAccess.Models;
using CompanyWebAppUI.Models;

namespace CompanyWebAppUI.ViewModels
{
    public class CourseViewModel
    {
        private Course _course;

        public CourseViewModel(Course course)
        {
            _course = course;
        }

        public CourseModel GetCourseModel()
        {
            return new CourseModel()
            {
                StartDate = _course.StartDate != null ? _course.StartDate.Value.ToShortDateString() : null,
                EndDate = _course.EndDate != null ? _course.EndDate.Value.ToShortDateString() : null,
                Description = _course.Description,
                Name = _course.Name,
                Type = _course.Type,
            };
        }
    }
}
