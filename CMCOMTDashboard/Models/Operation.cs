using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCOMTDashboard.Models
{
    [Table("operations", Schema = "public")]
    public class Operation
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        [Column("equipment_id")]
        public Nullable<int> EquipmentID { get; set; }
        [Column("employee_id")]
        public Nullable<int> EmployeeID { get; set; }
        [Column("created_by_id")]
        public Nullable<int> CreatedByID { get; set; }
        [Column("date")]
        public DateTime DateOfOperation { get; set; }
        [Column("shift")]
        public Nullable<int> Shift { get; set; }
        [Column("overtime")]
        public Nullable<Boolean> Overtime { get; set; }
        [Column("inserted_at")]
        public DateTime InsertedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("supervisor_id")]
        public Nullable<int> SupervisorID { get; set; }
        [Column("equipment_type_id")]
        public Nullable<int> EquipmentTypeID { get; set; }
        [Column("smr_number")]
        public string SMRNumber { get; set; }
        [Column("smr_start")]
        public string SMRStart { get; set; }
        [Column("smr_end_reg")]
        public string SMREndReg { get; set; }
        [Column("smr_end_ot")]
        public string SMREndOT { get; set; }
        [Column("odometer_start")]
        public string OdometerStart { get; set; }
        [Column("odometer_end_reg")]
        public string OdometerEndReg { get; set; }
        [Column("odometer_end_ot")]
        public string OdometerEndOT { get; set; }
        [Column("eu_combination")]
        public string EUCombination { get; set; }
        [Column("em_combination")]
        public string EMCombination { get; set; }
        [Column("ne_combination")]
        public string NECombination { get; set; }
    }
}