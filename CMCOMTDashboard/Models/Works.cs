using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCOMTDashboard.Models
{
    [Table("works", Schema = "public")]
    public class Works
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        [Column("operation_id")]
        public Nullable<int> OperationID { get; set; }
        [Column("created_by_id")]
        public Nullable<int> CreatedByID { get; set; }
        [Column("activity_id")]
        public Nullable<int> ActivityID { get; set; }
        [Column("location_id")]
        public Nullable<int> LocationID { get; set; }
        [Column("material_id")]
        public Nullable<int> MaterialID { get; set; }
        [Column("origin_id")]
        public Nullable<int> OriginID { get; set; }
        [Column("destination_id")]
        public Nullable<int> DestinationID { get; set; }
        [Column("loading_equipment_id")]
        public Nullable<int> LoadingEquipmentID { get; set; }
        [Column("receiving_equipment_id")]
        public Nullable<int> ReceivingEquipmentID { get; set; }
        [Column("order")]
        public Nullable<int> Order { get; set; }
        [Column("trip_count")]
        public Nullable<int> TripCount { get; set; }
        [Column("total_minutes")]
        public Nullable<int> TotalMinutes { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("stationary_equipment_id")]
        public Nullable<int> StationaryEquipmentID { get; set; }
        [Column("num_feed")]
        public string NumFeed { get; set; }
        [Column("num_product")]
        public string NumProduct { get; set; }
        [Column("remarks")]
        public string Remarks { get; set; }
        [Column("ot_trip_count")]
        public Nullable<int> OTTripCount { get; set; }
        [Column("ot_total_minutes")]
        public Nullable<int> OTTotalMinutes { get; set; }
    }
}