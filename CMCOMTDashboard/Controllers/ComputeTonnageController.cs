using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMCOMTDashboard.DAL;
using CMCOMTDashboard.Models;

namespace CMCOMTDashboard.Controllers
{
    public class ComputeTonnageController : Controller
    {
        CMCOMTDbContext _cmcOmtDb;
        public ComputeTonnageController()
        {
            _cmcOmtDb = new CMCOMTDbContext();
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

        public Tonnage GenerateRunOfMineReport(List<TonnageByActivity> DEURList, DateTime chosenDate, bool applyDate)
        {
            if (DEURList.Count == 0)
                return null;

            List<TonnageByActivity> tonnageList = new List<TonnageByActivity>();
            List<TonnageByActivity> tempTonnageList = new List<TonnageByActivity>();

            tonnageList = DEURList.Where(s=> s.TonnageAmount > 0).OrderByDescending(s => s.MaterialDateOfEntry).ToList();
            if (applyDate)
                tonnageList = DEURList.Where(s => s.MaterialDateOfEntry <= chosenDate).OrderByDescending(s => s.MaterialDateOfEntry).ToList();
            if (tonnageList.Count == 0)
                return null;

            DateTime? latestDate = tonnageList[0].MaterialDateOfEntry;
            if (applyDate)
                latestDate = chosenDate;

            #region variables initialization
            decimal yearToDateTonnageAmount = 0;
            decimal monthlyTonnageAmount = 0;
            decimal oneMonthBeforeTonnageAmount = 0;
            decimal twoMonthsBeforeTonnageAmount = 0;
            decimal dailyTonnageAmount = 0;
            decimal saproliteTonnage = 0;
            decimal limoniteTonnage = 0;
            decimal wasteTonnage = 0;
            decimal monthlySaproliteTonnage = 0;
            decimal monthlyLimoniteTonnage = 0;
            decimal monthlyWasteTonnage = 0;
            decimal yearlySaproliteTonnage = 0;
            decimal yearlyLimoniteTonnage = 0;
            decimal yearlyWasteTonnage = 0;
            string dailyDate = "";
            string monthlyDate = "";
            string yearlyDate = "";
            #endregion

            #region variable assignment
            //get year-to-date tonnage from January 1 - December 31 of the latest year available
            tempTonnageList = tonnageList.Where(s => s.MaterialDateOfEntry.Value.Year == latestDate.Value.Year).ToList();
            if (tempTonnageList.Count() > 0)
            {
                yearToDateTonnageAmount = tempTonnageList.Select(s => s.TonnageAmount).Sum();
                yearlyDate = tempTonnageList[0].MaterialDateOfEntry.Value.Year.ToString();
            }
            //get montly tonnage based on the latest month available
            tempTonnageList = tonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == latestDate.Value.Month).ToList();
            if (tempTonnageList.Count() > 0)
            {
                monthlyTonnageAmount = tempTonnageList.Select(s => s.TonnageAmount).Sum();
                monthlyDate = tempTonnageList[0].MaterialDateOfEntry.Value.Month.ToString();

                tempTonnageList = tonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == (latestDate.Value.Month - 1)).ToList();
                if (tempTonnageList.Count() > 0)
                    oneMonthBeforeTonnageAmount = tempTonnageList.Select(s => s.TonnageAmount).Sum();

                tempTonnageList = tonnageList.Where(s => s.MaterialDateOfEntry.Value.Month == (latestDate.Value.Month - 2)).ToList();
                if (tempTonnageList.Count() > 0)
                    twoMonthsBeforeTonnageAmount = tempTonnageList.Select(s => s.TonnageAmount).Sum();
            }
            //get daily tonnage based on the latest date available
            tempTonnageList = tonnageList.Where(s => s.MaterialDateOfEntry == latestDate).ToList();
            if (tempTonnageList.Count() > 0)
            {
                dailyTonnageAmount = tempTonnageList.Select(s => s.TonnageAmount).Sum();
                dailyDate = tempTonnageList[0].MaterialDateOfEntry.Value.ToShortDateString();
                System.Diagnostics.Debug.WriteLine("Latest Date: " + dailyDate + " Daily Tonnage Amount: " + dailyTonnageAmount);
            }

            //get DAILY, MONTHLY, YEARLY data based on material type: sapro, limo, waste
            tempTonnageList = tonnageList.Where(s => s.MaterialName.ToUpper() == "SAPROLITE" || s.MaterialName.ToUpper() == "SAPRO" || s.MaterialName.ToUpper() == "SHIPPABLE SAPRO").ToList();
            if (tempTonnageList.Count() > 0)
            {
                yearlySaproliteTonnage = tempTonnageList.Where(s => s.MaterialDateOfEntry.Value.Year.ToString() == yearlyDate).Select(s => s.TonnageAmount).Sum();
                monthlySaproliteTonnage = tempTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month.ToString() == monthlyDate && s.MaterialDateOfEntry.Value.Year.ToString() == yearlyDate).Select(s => s.TonnageAmount).Sum();
                saproliteTonnage = tempTonnageList.Where(s => s.MaterialDateOfEntry == latestDate).Select(s => s.TonnageAmount).Sum();
            }
            tempTonnageList = tonnageList.Where(s => s.MaterialName.ToUpper() == "LIMONITE" || s.MaterialName.ToUpper() == "LIMO" || s.MaterialName.ToUpper() == "SHIPPABLE LIMO").ToList();
            if (tempTonnageList.Count() > 0)
            {
                yearlyLimoniteTonnage = tempTonnageList.Where(s => s.MaterialDateOfEntry.Value.Year.ToString() == yearlyDate).Select(s => s.TonnageAmount).Sum();
                monthlyLimoniteTonnage = tempTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month.ToString() == monthlyDate && s.MaterialDateOfEntry.Value.Year.ToString() == yearlyDate).Select(s => s.TonnageAmount).Sum();
                limoniteTonnage = tempTonnageList.Where(s => s.MaterialDateOfEntry == latestDate).Select(s => s.TonnageAmount).Sum();
            }
            tempTonnageList = tonnageList.Where(s => s.MaterialName.ToUpper() == "BOULDERS" || s.MaterialName.ToUpper() == "CRUSHED BOULDERS" || s.MaterialName.ToUpper() == "LIMO WASTE" || s.MaterialName.Trim().ToUpper() == "SAPRO WASTE" || s.MaterialName.Trim().ToUpper() == "TOPSOIL").ToList();
            if (tempTonnageList.Count() > 0)
            {
                yearlyWasteTonnage = tempTonnageList.Where(s => s.MaterialDateOfEntry.Value.Year.ToString() == yearlyDate).Select(s => s.TonnageAmount).Sum();
                monthlyWasteTonnage = tempTonnageList.Where(s => s.MaterialDateOfEntry.Value.Month.ToString() == monthlyDate && s.MaterialDateOfEntry.Value.Year.ToString() == yearlyDate).Select(s => s.TonnageAmount).Sum();
                wasteTonnage = tempTonnageList.Where(s => s.MaterialDateOfEntry == latestDate).Select(s => s.TonnageAmount).Sum();
            }
            #endregion

            #region variable assignment to model
            Tonnage finalTonnage = new Tonnage();
            finalTonnage.TonnageDaily = dailyTonnageAmount;
            finalTonnage.TonnageYearly = yearToDateTonnageAmount;
            finalTonnage.TonnageMonthly = monthlyTonnageAmount;
            finalTonnage.TonnageOneMonthBefore = oneMonthBeforeTonnageAmount;
            finalTonnage.TonnageTwoMonthsBefore = twoMonthsBeforeTonnageAmount;

            finalTonnage.DailyTonnageSaprolite = saproliteTonnage;
            finalTonnage.DailyTonnageLimonite = limoniteTonnage;
            finalTonnage.DailyTonnageWaste = wasteTonnage;
            finalTonnage.DateDaily = dailyDate;

            finalTonnage.MonthlyTonnageSaprolite = monthlySaproliteTonnage;
            finalTonnage.MonthlyTonnageLimonite = monthlyLimoniteTonnage;
            finalTonnage.MonthlyTonnageWaste = monthlyWasteTonnage;
            finalTonnage.DateMonthly = monthlyDate;
            //finalTonnage.DateOneMonthBefore = (Convert.ToInt32(monthlyDate) - 1).ToString();
            //finalTonnage.DateTwoMonthsBefore = (Convert.ToInt32(monthlyDate) - 2).ToString();

            finalTonnage.YearlyTonnageSaprolite = yearlySaproliteTonnage;
            finalTonnage.YearlyTonnageLimonite = yearlyLimoniteTonnage;
            finalTonnage.YearlyTonnageWaste = yearlyWasteTonnage;
            finalTonnage.DateYearly = yearlyDate;
            #endregion

            return finalTonnage;
        }
        
        public TonnageByMajorActivity ComputeTonnageByActivity(string activityType, List<TonnageByActivity> activityList)
        {
            TonnageByMajorActivity finalValues = new TonnageByMajorActivity();
            List<TonnageByActivity> tempActivityList = new List<TonnageByActivity>();

            tempActivityList = activityList.Where(s => s.ActivityType.Trim().ToUpper() == activityType).ToList();
            if (tempActivityList.Count() <= 0)
                return null;

            finalValues.BargeShipLoading = ComputeTonnageByType("ACTIVITY", "BARGELOADING/SHIPLOADING", tempActivityList);
            finalValues.Crushing = ComputeTonnageByType("ACTIVITY", "CRUSHING", tempActivityList);
            finalValues.DirectMining = ComputeTonnageByType("ACTIVITY", "DIRECT MINING", tempActivityList);
            finalValues.EnviMajor = ComputeTonnageByType("ACTIVITY", "ENVI MAJOR", tempActivityList);
            finalValues.EPEP = ComputeTonnageByType("ACTIVITY", "EPEP", tempActivityList);
            finalValues.MiningAreaMngmtMntnce = ComputeTonnageByType("ACTIVITY", "MINING AREA MANAGEMENT AND MAINTENANCE", tempActivityList);
            finalValues.PitPrepMiscellaneousAct = ComputeTonnageByType("ACTIVITY", "PIT PREP AND MISCELLANEOUS ACTIVITIES", tempActivityList);
            finalValues.Rehandling = ComputeTonnageByType("ACTIVITY", "REHANDLING", tempActivityList);

            return finalValues;
        }

        public TonnageByEquipment ComputeTonnageByEquipment(List<TonnageByActivity> activityList)
        {
            TonnageByEquipment finalValues = new TonnageByEquipment();

            finalValues.DumpTruckData = ComputeTonnageByType("EQUIPMENT", "1", activityList);
            finalValues.ExcavatorData = ComputeTonnageByType("EQUIPMENT", "2", activityList);
            finalValues.AuxiliaryData = ComputeTonnageByType("EQUIPMENT", "3", activityList);
            finalValues.StationaryData = ComputeTonnageByType("EQUIPMENT", "4", activityList);
            finalValues.EnviData = ComputeTonnageByType("EQUIPMENT", "5", activityList);
            finalValues.SVSTData = ComputeTonnageByType("EQUIPMENT", "6", activityList);
            finalValues.NonEquipmentData = ComputeTonnageByType("EQUIPMENT", "7", activityList);
            finalValues.RainfallData = ComputeTonnageByType("EQUIPMENT", "8", activityList);
            
            return finalValues;
        }

        public MajorActivityDetails ComputeTonnageByType(string sortBy, string sortKey, List<TonnageByActivity> activityList)
        {
            MajorActivityDetails finalValues = new MajorActivityDetails();
            List<TonnageByActivity> tempActivityList = new List<TonnageByActivity>();
            List<TonnageByActivity> finalActivityList = new List<TonnageByActivity>();

            if (sortBy.ToUpper() == "ACTIVITY")
                tempActivityList = activityList.Where(s => s.GeneralActivity.Trim().ToUpper() == sortKey).ToList();
            else if (sortBy.ToUpper() == "EQUIPMENT")
                tempActivityList = activityList.Where(s => s.EquipmentID == Convert.ToInt32(sortKey)).ToList();

            finalValues.TotalWMT = 0;
            finalValues.TotalHours = 0;
            finalValues.TotalWMTPerHour = 0;
            finalValues.SaproWMT = 0;
            finalValues.SaproHours = 0;
            finalValues.LimoWMT = 0;
            finalValues.LimoHours = 0;
            finalValues.WasteWMT = 0;
            finalValues.WasteHours = 0;

            if (tempActivityList.Count <= 0)
                return finalValues;

            finalValues.TotalWMT = tempActivityList.Select(s => s.TonnageAmount).Sum();
            finalValues.TotalHours = Convert.ToDecimal(tempActivityList.Select(s => s.TotalMinutes).Sum() / 60);
            finalValues.TotalWMTPerHour = finalValues.TotalWMT / finalValues.TotalHours;

            finalActivityList = tempActivityList.Where(s => s.MaterialName.Trim().ToUpper() == "SAPROLITE" || s.MaterialName.Trim().ToUpper() == "SAPRO" || s.MaterialName.Trim().ToUpper() == "SHIPPABLE SAPRO").ToList();
            if (finalActivityList.Count > 0)
            {
                finalValues.SaproWMT = finalActivityList.Select(s => s.TonnageAmount).Sum();
                finalValues.SaproHours = Convert.ToDecimal(finalActivityList.Select(s => s.TotalMinutes).Sum() / 60);
            }

            finalActivityList = tempActivityList.Where(s => s.MaterialName.Trim().ToUpper() == "LIMONITE" || s.MaterialName.Trim().ToUpper() == "LIMO" || s.MaterialName.Trim().ToUpper() == "SHIPPABLE LIMO").ToList();
            if (finalActivityList.Count > 0)
            {
                finalValues.LimoWMT = finalActivityList.Select(s => s.TonnageAmount).Sum();
                finalValues.LimoHours = Convert.ToDecimal(finalActivityList.Select(s => s.TotalMinutes).Sum() / 60);
            }

            finalActivityList = tempActivityList.Where(s => s.MaterialName.Trim().ToUpper() == "BOULDERS" || s.MaterialName.Trim().ToUpper() == "CRUSHED BOULDERS" || s.MaterialName.Trim().ToUpper() == "LIMO WASTE" || s.MaterialName.Trim().ToUpper() == "SAPRO WASTE" || s.MaterialName.Trim().ToUpper() == "TOPSOIL").ToList();
            if (finalActivityList.Count > 0)
            {
                finalValues.WasteWMT = finalActivityList.Select(s => s.TonnageAmount).Sum();
                finalValues.WasteHours = Convert.ToDecimal(finalActivityList.Select(s => s.TotalMinutes).Sum() / 60);
            }

            return finalValues;
        }
    }
}