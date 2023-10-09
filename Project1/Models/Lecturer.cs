using Project1.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using WorkAllocationApp.Utils;

namespace Project1.Models
{
    public class Lecturer
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LecturerId { get; set; }
        public string Name { get; set; }
        public RoleType Role { get; set; }
        public decimal MinTeachingHrs { get; set; }
    }
}
