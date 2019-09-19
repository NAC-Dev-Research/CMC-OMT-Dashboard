using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCOMTDashboard.Models
{
    [Table("settings", Schema = "public")]
    public class Settings
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("value")]
        public string Value { get; set; }
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("start_time")]
        public Nullable<DateTime> StartTime { get; set; }
        [Column("end_time")]
        public Nullable<DateTime> EndTime { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("dt_type")]
        public string DTType { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("material")]
        public string Material { get; set; }
        [Column("combination")]
        public string Combination { get; set; }
    }
}