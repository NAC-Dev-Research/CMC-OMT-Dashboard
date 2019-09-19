using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCOMTDashboard.Models
{
    [Table("activities", Schema = "public")]
    public class Activities
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        [Column("cc_activity_id")]
        public Nullable<int> CCActivityID { get; set; }
        [Column("main")]
        public string MainActivity { get; set; }
        [Column("specific")]
        public string SpecificActivity { get; set; }
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("equipment_type_id")]
        public Nullable<int> EquipmentTypeID { get; set; }
        [Column("general_activity")]
        public string GeneralACtivity { get; set; }
    }
}