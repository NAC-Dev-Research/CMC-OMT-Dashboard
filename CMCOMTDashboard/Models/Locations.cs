using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCOMTDashboard.Models
{
    [Table("locations", Schema = "public")]
    public class Locations
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        [Column("name")]
        public string LocationName { get; set; }
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("equipment_type")]
        public string EquipmentType { get; set; }
        [Column("condition")]
        public string Condition { get; set; }
        [Column("deadline")]
        public Nullable<DateTime> Deadline { get; set; }
    }
}