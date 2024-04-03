using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PatientApp.Models
{
    [Table("Patient")]
    public partial class Patient
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        public string LastName { get; set; } = null!;
        [Column("PESEL")]
        [StringLength(11)]
        public string Pesel { get; set; } = null!;
        [StringLength(50)]
        public string City { get; set; } = null!;
        [StringLength(50)]
        public string Street { get; set; } = null!;
        [StringLength(10)]
        public string Zipcode { get; set; } = null!;
        [Column("isActive")]
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastEditedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeletedAt { get; set; }
    }
}
