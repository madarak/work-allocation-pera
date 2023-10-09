using System.ComponentModel.DataAnnotations;

namespace Project1.Models.Enums
{
    public record HoursPerCreditForActivity
    {
        [Key]
        public ActivityType Activity { get; set; }
        public double NoOfHoursPerCredit { get; set; }
        public double NoOfHoursPerEvent { get; set; }
    }
}
