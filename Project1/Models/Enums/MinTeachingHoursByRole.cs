using System.ComponentModel.DataAnnotations;

namespace Project1.Models.Enums
{
    public record MinTeachingHoursByRole
    {
        [Key]
        public RoleType Role { get; set; }
        public decimal MinNoOfHours { get; set; }
    }
}
