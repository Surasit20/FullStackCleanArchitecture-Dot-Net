using FullStackCleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCleanArchitecture.Domain.Entities
{
    [Table("Employee")]
    public class Employee : BaseAuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(@"EmployeeId", Order = 1)]
        [Required]
        public int EmployeeId { get; set; } // EmployeeId (Primary key)

        [Column(@"FirstName", Order = 2)]
        [Required]
        [MaxLength(300)]
        public string FirstName { get; set; } // FirstName (length: 300)

        [Column(@"LastName", Order = 3)]
        [Required]
        [MaxLength(300)]
        public string LastName { get; set; } // LastName (length: 300)

        [Column(@"FullName", Order = 4)]
        [Required]
        [MaxLength(300)]
        public string? FullName { get; set; } // FullName (length: 300)

    }
}
