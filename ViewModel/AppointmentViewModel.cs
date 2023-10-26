using System.ComponentModel.DataAnnotations;
using OptiApp.Models;

namespace OptiApp.ViewModel;

public class AppointmentViewModel
{
        public string PatientId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string SelectedService { get; set; }
        public DateTime SelectedDate { get; set; }
        public TimeSpan SelectedTime { get; set; }
}