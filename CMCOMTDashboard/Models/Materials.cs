using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCOMTDashboard.Models
{
    [Table("materials", Schema = "public")]
    public class Materials
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        [Column("activity_id")]
        public Nullable<int> ActivityID { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("distance")]
        public Nullable<decimal> Distance { get; set; }
        [Column("location_id")]
        public Nullable<int> LocationID { get; set; }
        [Column("equipment_type_id")]
        public Nullable<int> EquipmentTypeID { get; set; }
        [Column("origin_id")]
        public Nullable<int> OriginID { get; set; }
        [Column("destination_id")]
        public Nullable<int> DestinationID { get; set; }
        [Column("specific_id")]
        public Nullable<int> SpecificID { get; set; }
        [Column("combination")]
        public string Combination { get; set; }
    }
}