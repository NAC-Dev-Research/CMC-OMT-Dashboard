using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCOMTDashboard.Models
{
    [Table("cc_activities", Schema = "public")]
    public class CCActivities
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        [Column("name")]
        public string ActivityName { get; set; }
        [Column("code")]
        public string ActivityCode { get; set; }
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}