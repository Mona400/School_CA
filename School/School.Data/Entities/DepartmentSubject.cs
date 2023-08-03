using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities
{
    public class DepartmentSubject
    {

        [Key]
        public int DID { get; set; }
        [Key]
        public int SubID { get; set; }
        [ForeignKey("DID")]
        [InverseProperty("DepartmentSubjects")]
        public virtual Department? Department { get; set; }
        [ForeignKey("SubID")]
        [InverseProperty("DepartmentSubjects")]
        public virtual Subject? Subject { get; set; }
    }
}
