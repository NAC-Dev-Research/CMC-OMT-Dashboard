using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMCOMTDashboard.Models
{
    public class TonnageByEquipment
    {
        public MajorActivityDetails DumpTruckData { get; set; }
        public MajorActivityDetails ExcavatorData { get; set; }
        public MajorActivityDetails AuxiliaryData { get; set; }
        public MajorActivityDetails StationaryData { get; set; }
        public MajorActivityDetails EnviData { get; set; }
        public MajorActivityDetails SVSTData { get; set; }
        public MajorActivityDetails NonEquipmentData { get; set; }
        public MajorActivityDetails RainfallData { get; set; }
    }
}