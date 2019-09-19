using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMCOMTDashboard.DAL;
using CMCOMTDashboard.Models;
using CMCOMTDashboard.Models.Cost;

namespace CMCOMTDashboard.Controllers
{
    public class ComputeCostController : Controller
    {
        CMCOMTDbContext _cmcOmtDb;
        CMCOMTDashboardEntities _cmcOmtSqlDb;
        public ComputeCostController()
        {
            _cmcOmtDb = new CMCOMTDbContext();
            _cmcOmtSqlDb = new CMCOMTDashboardEntities();
        }
        public List<TonnageByActivity> GenerateDEURList(int ccActivityID)
        {
            //Activity IDs equivalent
            //0 - All activity below
            //1 - Production Operation
            //2 - Construction
            //3 - Maintenance
            //4 - Standby Operation
            //5 - Sample Delivery
            //6 - Others
            //7 - Water Spraying

            System.Diagnostics.Debug.WriteLine("Generating final DEUR list");
            List<Activities> activitiesList = _cmcOmtDb.Activity.ToList();
            if (ccActivityID > 0)
                activitiesList = _cmcOmtDb.Activity.Where(s => s.CCActivityID == ccActivityID).ToList();
            List<Settings> settingList = _cmcOmtDb.Settings.Where(s => s.Code == "tonnage_factor").ToList();
            List<TonnageByActivity> finalList = new List<TonnageByActivity>();

            #region DEUR list generation
            //for now only get PRODUCTION OPERATION data <CCActivityID = 1>
            var DEURList = _cmcOmtDb.Works.ToList().Join(_cmcOmtDb.Materials.ToList(), work => work.MaterialID, mat => mat.ID, (work, mat) =>
                                            new
                                            {
                                                workMatID = work.MaterialID,
                                                workOpID = work.OperationID,
                                                workActID = work.ActivityID,
                                                workTripCount = work.TripCount,
                                                workTotalMins = work.TotalMinutes,
                                                workOTTripCount = work.OTTripCount,
                                                matID = mat.ID,
                                                matName = mat.Name,
                                                matDistance = mat.Distance,
                                                matActivityID = mat.ActivityID
                                            }).Join(_cmcOmtDb.Operation.ToList(), work => work.workOpID, op => op.ID, (work, op) =>
                                                new
                                                {
                                                    work.workMatID,
                                                    work.workOpID,
                                                    work.workActID,
                                                    work.workTripCount,
                                                    work.workTotalMins,
                                                    work.workOTTripCount,
                                                    work.matID,
                                                    work.matName,
                                                    work.matDistance,
                                                    work.matActivityID,
                                                    opID = op.ID,
                                                    opDate = op.DateOfOperation,
                                                    opEquipID = op.EquipmentID
                                                }).Join(activitiesList, work => work.matActivityID, act => act.ID, (work, act) =>
                                                    new
                                                    {
                                                        work.workMatID,
                                                        work.workOpID,
                                                        work.workActID,
                                                        work.workTripCount,
                                                        work.workTotalMins,
                                                        work.workOTTripCount,
                                                        work.matID,
                                                        work.matName,
                                                        work.matDistance,
                                                        work.matActivityID,
                                                        work.opID,
                                                        work.opDate,
                                                        work.opEquipID,
                                                        actID = act.ID,
                                                        actCCID = act.CCActivityID,
                                                        actMainAct = act.MainActivity,
                                                        actSpecificAct = act.SpecificActivity,
                                                        actGeneralAct = act.GeneralACtivity
                                                    }).Join(_cmcOmtDb.CCActivity.ToList(), work => work.actCCID, cc => cc.ID, (work, cc) =>
                                                        new
                                                        {
                                                            work.workMatID,
                                                            work.workOpID,
                                                            work.workActID,
                                                            work.workTripCount,
                                                            work.workOTTripCount,
                                                            work.workTotalMins,
                                                            work.matID,
                                                            work.matName,
                                                            work.matDistance,
                                                            work.matActivityID,
                                                            work.opID,
                                                            work.opDate,
                                                            work.opEquipID,
                                                            work.actID,
                                                            work.actCCID,
                                                            work.actMainAct,
                                                            work.actSpecificAct,
                                                            work.actGeneralAct,
                                                            ccActID = cc.ID,
                                                            ccActType = cc.ActivityName
                                                        }).Join(_cmcOmtDb.Equipment.ToList(), work => work.opEquipID, eqp => eqp.ID, (work, eqp) =>
                                                            new
                                                            {
                                                                work.workMatID,
                                                                work.workOpID,
                                                                work.workActID,
                                                                work.workTripCount,
                                                                work.workTotalMins,
                                                                work.workOTTripCount,
                                                                work.matID,
                                                                work.matName,
                                                                work.matDistance,
                                                                work.matActivityID,
                                                                work.opID,
                                                                work.opDate,
                                                                work.opEquipID,
                                                                work.actID,
                                                                work.actCCID,
                                                                work.actMainAct,
                                                                work.actSpecificAct,
                                                                work.actGeneralAct,
                                                                work.ccActID,
                                                                work.ccActType,
                                                                eqEquipID = eqp.ID,
                                                                eqEquipTypeID = eqp.EquipmentTypeID
                                                            }).Select(final =>
                                                                new
                                                                {
                                                                    WorkMatID = final.workMatID,
                                                                    WorkOpID = final.workOpID,
                                                                    WorkActID = final.workActID,
                                                                    WorkTripCount = final.workTripCount,
                                                                    WorkTotalMinutes = final.workTotalMins,
                                                                    WorkOTTripCount = final.workOTTripCount,
                                                                    MatID = final.matID,
                                                                    MatName = final.matName,
                                                                    MatDistance = final.matDistance,
                                                                    MatActivityID = final.matActivityID,
                                                                    OpID = final.opID,
                                                                    OpDate = final.opDate,
                                                                    OpEquipID = final.opEquipID,
                                                                    ActID = final.actID,
                                                                    ActMainActivity = final.actMainAct,
                                                                    ActSpecificActivity = final.actSpecificAct,
                                                                    ActGeneralActivity = final.actGeneralAct,
                                                                    CCActID = final.ccActID,
                                                                    CCActActivityType = final.ccActType,
                                                                    EqEquipID = final.eqEquipID,
                                                                    EqEquipTypeID = final.eqEquipTypeID
                                                                });
            #endregion

            foreach (var item in DEURList)
            {
                Settings settingMatch = settingList.Where(s => item.OpDate >= s.StartTime && item.OpDate <= s.EndTime).FirstOrDefault();
                if (settingMatch == null)
                    continue;

                TonnageByActivity curTonnageByActivity = new TonnageByActivity();

                curTonnageByActivity.MaterialID = item.MatID;
                curTonnageByActivity.MaterialName = item.MatName;
                curTonnageByActivity.EquipmentID = Convert.ToInt32(item.EqEquipTypeID);
                curTonnageByActivity.MaterialDateOfEntry = item.OpDate;
                curTonnageByActivity.TripCount = item.WorkTripCount;
                curTonnageByActivity.TonnageFactor = Convert.ToInt32(settingMatch.Value);
                curTonnageByActivity.Distance = Convert.ToDecimal(item.MatDistance);
                curTonnageByActivity.TonnageAmount = Convert.ToDecimal((item.WorkTripCount + item.WorkOTTripCount) * Convert.ToInt32(settingMatch.Value));

                curTonnageByActivity.TotalMinutes = item.WorkTotalMinutes;
                curTonnageByActivity.OriginLocation = "";
                curTonnageByActivity.DestinationLocation = "";
                curTonnageByActivity.MainActivity = item.ActMainActivity;
                curTonnageByActivity.SpecificActivity = item.ActSpecificActivity;
                curTonnageByActivity.GeneralActivity = item.ActGeneralActivity;
                curTonnageByActivity.ActivityType = item.CCActActivityType;

                finalList.Add(curTonnageByActivity);
            }

            return finalList;
        }

        public CostKPI ComputeCostVariables(List<TonnageByActivity> DEURList)
        {
            if (DEURList.Count == 0)
                return null;

            CostKPI finalValues = new CostKPI();
            List<TonnageByActivity> tonnageList = new List<TonnageByActivity>();
            List<TonnageByActivity> tempTonnageList = new List<TonnageByActivity>();
            List<TonnageByActivity> limoTonnageList = new List<TonnageByActivity>();
            List<TonnageByActivity> saproTonnageList = new List<TonnageByActivity>();

            tonnageList = DEURList.Where(s => s.TonnageAmount > 0).OrderByDescending(s => s.MaterialDateOfEntry).ToList();
            if (tonnageList.Count == 0)
                return null;
            DateTime? latestDate = tonnageList[0].MaterialDateOfEntry;

            string yearlyDate = "";
            //get year-to-date tonnage from January 1 - to the latest date of the latest year available
            tempTonnageList = tonnageList.Where(s => s.MaterialDateOfEntry.Value.Year == latestDate.Value.Year).ToList();
            limoTonnageList = tonnageList.Where(s => s.MaterialDateOfEntry.Value.Year == latestDate.Value.Year &&
                                        (s.MaterialName.ToUpper() == "LIMONITE" || s.MaterialName.ToUpper() == "LIMO" || s.MaterialName.ToUpper() == "SHIPPABLE LIMO")).ToList();
            saproTonnageList = tonnageList.Where(s => s.MaterialDateOfEntry.Value.Year == latestDate.Value.Year &&
                                        (s.MaterialName.ToUpper() == "SAPROLITE" || s.MaterialName.ToUpper() == "SAPRO" || s.MaterialName.ToUpper() == "SHIPPABLE SAPRO")).ToList();
            if (tempTonnageList.Count() > 0)
            {
                yearlyDate = tempTonnageList[0].MaterialDateOfEntry.Value.Year.ToString();
                finalValues.TotalLimoWMT = limoTonnageList.Select(s => s.TonnageAmount).Sum();
                finalValues.TotalSaproWMT = saproTonnageList.Select(s => s.TonnageAmount).Sum();
                finalValues.TotalManHour = Convert.ToDecimal((limoTonnageList.Select(s => s.TotalMinutes).Sum() /60) + (saproTonnageList.Select(s => s.TotalMinutes).Sum() /60));
                finalValues.AverageDistance = Convert.ToDecimal((limoTonnageList.Select(s => s.Distance).Sum() +
                                                      saproTonnageList.Select(s => s.Distance).Sum()) /
                                                      (limoTonnageList.Count() + saproTonnageList.Count()));

                finalValues.TotalBargeLimoWMT = limoTonnageList.Where(s => s.GeneralActivity.ToUpper() == "BARGELOADING/SHIPLOADING").Select(s => s.TonnageAmount).Sum();
                finalValues.TotalBargeSaproWMT = saproTonnageList.Where(s => s.GeneralActivity.ToUpper() == "BARGELOADING/SHIPLOADING").Select(s => s.TonnageAmount).Sum();

                finalValues.JanuarySaproWMT = saproTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 1).Select(s => s.TonnageAmount).Sum();
                finalValues.FebruarySaproWMT = saproTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 2).Select(s => s.TonnageAmount).Sum();
                finalValues.MarchSaproWMT = saproTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 3).Select(s => s.TonnageAmount).Sum();
                finalValues.MaySaproWMT = saproTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 4).Select(s => s.TonnageAmount).Sum();
                finalValues.AprilSaproWMT = saproTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 5).Select(s => s.TonnageAmount).Sum();
                finalValues.JuneSaproWMT = saproTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 6).Select(s => s.TonnageAmount).Sum();
                finalValues.JulySaproWMT = saproTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 7).Select(s => s.TonnageAmount).Sum();
                finalValues.AugustSaproWMT = saproTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 8).Select(s => s.TonnageAmount).Sum();
                finalValues.SeptemberSaproWMT = saproTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 9).Select(s => s.TonnageAmount).Sum();
                finalValues.OctoberSaproWMT = saproTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 10).Select(s => s.TonnageAmount).Sum();
                finalValues.NovemberSaproWMT = saproTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 11).Select(s => s.TonnageAmount).Sum();
                finalValues.DecemberSaproWMT = saproTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 12).Select(s => s.TonnageAmount).Sum();

                finalValues.JanuaryLimoWMT = limoTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 1).Select(s => s.TonnageAmount).Sum();
                finalValues.FebruaryLimoWMT = limoTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 2).Select(s => s.TonnageAmount).Sum();
                finalValues.MarchLimoWMT = limoTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 3).Select(s => s.TonnageAmount).Sum();
                finalValues.MayLimoWMT = limoTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 4).Select(s => s.TonnageAmount).Sum();
                finalValues.AprilLimoWMT = limoTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 5).Select(s => s.TonnageAmount).Sum();
                finalValues.JuneLimoWMT = limoTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 6).Select(s => s.TonnageAmount).Sum();
                finalValues.JulyLimoWMT = limoTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 7).Select(s => s.TonnageAmount).Sum();
                finalValues.AugustLimoWMT = limoTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 8).Select(s => s.TonnageAmount).Sum();
                finalValues.SeptemberLimoWMT = limoTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 9).Select(s => s.TonnageAmount).Sum();
                finalValues.OctoberLimoWMT = limoTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 10).Select(s => s.TonnageAmount).Sum();
                finalValues.NovemberLimoWMT = limoTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 11).Select(s => s.TonnageAmount).Sum();
                finalValues.DecemberLimoWMT = limoTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == 12).Select(s => s.TonnageAmount).Sum();
            }

            return finalValues;
        }

        public ProdCostPerOreMinedDetails ComputeProdCostPerOReMined(CostKPI value)
        {
            if (value == null)
                return null;

            List<CostSetting> costSetting = _cmcOmtSqlDb.CostSettings.ToList();
            int curMonth = System.DateTime.Now.Month - 1;
            decimal prodCost = costSetting[curMonth].ProductionCost;

            ProdCostPerOreMinedDetails finalValues = new ProdCostPerOreMinedDetails();

            finalValues.TotalOreMined = value.TotalLimoWMT + value.TotalSaproWMT;
            finalValues.ProductionCost = prodCost;
            finalValues.ProductionCostPerOreMined = prodCost / (value.TotalLimoWMT + value.TotalSaproWMT);

            finalValues.JanuaryProdCost = costSetting[0].ProductionCost;
            finalValues.FebruaryProdCost = costSetting[1].ProductionCost;
            finalValues.MarchProdCost = costSetting[2].ProductionCost;
            finalValues.MayProdCost = costSetting[3].ProductionCost;
            finalValues.AprilProdCost = costSetting[4].ProductionCost;
            finalValues.JuneProdCost = costSetting[5].ProductionCost;
            finalValues.JulyProdCost = costSetting[6].ProductionCost;
            finalValues.AugustProdCost = costSetting[7].ProductionCost;
            finalValues.SeptemberProdCost = costSetting[8].ProductionCost;
            finalValues.OctoberProdCost = costSetting[9].ProductionCost;
            finalValues.NovemberProdCost = costSetting[10].ProductionCost;
            finalValues.DecemberProdCost = costSetting[11].ProductionCost;
            
            finalValues.JanuaryOreMined = value.JanuaryLimoWMT + value.JanuarySaproWMT;
            finalValues.FebruaryOreMined = value.FebruaryLimoWMT + value.FebruarySaproWMT;
            finalValues.MarchOreMined = value.MarchLimoWMT + value.MarchSaproWMT;
            finalValues.MayOreMined = value.AprilLimoWMT + value.AprilSaproWMT;
            finalValues.AprilOreMined = value.MayLimoWMT + value.MaySaproWMT;
            finalValues.JuneOreMined = value.JuneLimoWMT + value.JuneSaproWMT;
            finalValues.JulyOreMined = value.JulyLimoWMT + value.JulySaproWMT;
            finalValues.AugustOreMined = value.AugustLimoWMT + value.AugustSaproWMT;
            finalValues.SeptemberOreMined = value.SeptemberLimoWMT + value.SeptemberSaproWMT;
            finalValues.OctoberOreMined = value.OctoberLimoWMT + value.OctoberSaproWMT;
            finalValues.NovemberOreMined = value.NovemberLimoWMT + value.NovemberSaproWMT;
            finalValues.DecemberOreMined = value.DecemberLimoWMT + value.DecemberSaproWMT;
            
            finalValues.JanuaryTotalCost = ComputeTotalCost(costSetting[0].ProductionCost, (value.JanuaryLimoWMT + value.JanuarySaproWMT));
            finalValues.FebruaryTotalCost = ComputeTotalCost(costSetting[1].ProductionCost, (value.FebruaryLimoWMT + value.FebruarySaproWMT));
            finalValues.MarchTotalCost = ComputeTotalCost(costSetting[2].ProductionCost, (value.MarchLimoWMT + value.MarchSaproWMT));
            finalValues.MayTotalCost = ComputeTotalCost(costSetting[3].ProductionCost, (value.AprilLimoWMT + value.AprilSaproWMT));
            finalValues.AprilTotalCost = ComputeTotalCost(costSetting[4].ProductionCost, (value.MayLimoWMT + value.MaySaproWMT));
            finalValues.JuneTotalCost = ComputeTotalCost(costSetting[5].ProductionCost, (value.JuneLimoWMT + value.JuneSaproWMT));
            finalValues.JulyTotalCost = ComputeTotalCost(costSetting[6].ProductionCost, (value.JulyLimoWMT + value.JulySaproWMT));
            finalValues.AugustTotalCost = ComputeTotalCost(costSetting[7].ProductionCost, (value.AugustLimoWMT + value.AugustSaproWMT));
            finalValues.SeptemberTotalCost = ComputeTotalCost(costSetting[8].ProductionCost, (value.SeptemberLimoWMT + value.SeptemberSaproWMT));
            finalValues.OctoberTotalCost = ComputeTotalCost(costSetting[9].ProductionCost, (value.OctoberLimoWMT + value.OctoberSaproWMT));
            finalValues.NovemberTotalCost = ComputeTotalCost(costSetting[10].ProductionCost, (value.NovemberLimoWMT + value.NovemberSaproWMT));
            finalValues.DecemberTotalCost = ComputeTotalCost(costSetting[11].ProductionCost, (value.DecemberLimoWMT + value.DecemberSaproWMT));

            return finalValues;
        }

        private decimal ComputeTotalCost(decimal prodCost, decimal limoSaproWMT)
        {
            decimal finalvalue = 0.0m;

            if (limoSaproWMT > 0)
                finalvalue = prodCost / limoSaproWMT;

            return finalvalue;
        }
    }
}