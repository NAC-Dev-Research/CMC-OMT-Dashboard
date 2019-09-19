using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCOMTDashboard.Models
{
    [Table("equipments", Schema = "public")]
    public class Equipment
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        [Column("cc_equipment_id")]
        public Nullable<int> CCEquipmentID { get; set; }
        [Column("number")]
        public string Number { get; set; }
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("created_by_id")]
        public Nullable<int> CreatedByID { get; set; }
        [Column("equipment_type_id")]
        public Nullable<int> EquipmentTypeID { get; set; }
        [Column("dt_type")]
        public string DTType { get; set; }
        [Column("operator")]
        public string Operator { get; set; }
        [Column("initial_vf")]
        public string InitialVF { get; set; }
        [Column("condition")]
        public string Condition { get; set; }
        [Column("deadline")]
        public Nullable<DateTime> Deadline { get; set; }
    }
}