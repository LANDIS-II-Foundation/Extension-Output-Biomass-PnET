﻿using System.Collections.Generic;
using System;
using System.Linq;
using Landis.SpatialModeling;
using Landis.Library.Metadata;

namespace Landis.Extension.Output.PnET
{
   
    public class OutputAggregatedTable
    {
        static List<string> FileContent = null;
        private static string FileName;

        [DataFieldAttribute(Unit = FieldUnits.Year, Desc = "Simulation Year")]
        public int Time { set; get; }

        [DataFieldAttribute(Unit = FieldUnits.Count, Desc = "Number of Cohorts")]
        public int Cohorts { set; get; }

        [DataFieldAttribute(Unit = FieldUnits.Year, Desc = "Average Age", Format = "0.00")]
        public double AverageAge { set; get; }

        [DataFieldAttribute(Unit = FieldUnits.g_B_m2, Desc = "Average Wood Root Biomass", Format = "0.00")]
        public double AverageWoodRootBio { set; get; }

        [DataFieldAttribute(Unit = "m-2", Desc = "Average LAI", Format = "0.00")]
        public double AverageLAI { set; get; }

        [DataFieldAttribute(Unit = "mm_m-1", Desc = "Average Water", Format = "0.00")]
        public double AverageWater { set; get; }

        [DataFieldAttribute(Unit = "W_m-2_or_mmol_m-2)", Desc = "SubcanopyPAR", Format = "0.00")]
        public double SubcanopyPAR { set; get; }

        [DataFieldAttribute(Unit = "kgDW_m-2", Desc = "Litter", Format = "0.00")]
        public double Litter { set; get; }

        [DataFieldAttribute(Unit = "kgDW_m-2", Desc = "Woody Debris", Format = "0.00")]
        public double WoodyDebris { set; get; }

        [DataFieldAttribute(Unit = FieldUnits.g_B_m2, Desc = "Average Root Biomass", Format = "0.00")]
        public double AverageRoot { set; get; }

        [DataFieldAttribute(Unit = FieldUnits.g_B_m2, Desc = "Average Wood Biomass", Format = "0.00")]
        public double AverageWood { set; get; }

        [DataFieldAttribute(Unit = FieldUnits.g_B_m2, Desc = "Average Foliage", Format = "0.00")]
        public double AverageFoliage { set; get; }

        [DataFieldAttribute(Unit = FieldUnits.g_C_m2, Desc = "Average NSC", Format = "0.00")]
        public double AverageNSC { set; get; }

        [DataFieldAttribute(Unit = "mm-2", Desc = "Average AET", Format = "0.00")]
        public double AverageAET { set; get; }


        public OutputAggregatedTable(string Template)
        {
            
            FileName = FileNames.ReplaceTemplateVars(Template, "Overall", PlugIn.ModelCore.CurrentTime).Replace(".img", ".csv").Replace(".txt", ".csv");
            FileContent = new List<string>();
            FileContent.Add("Time" + "," + "#Cohorts" + "," + "AverageAge" + "," + "AverageWoodRoot(g/m2)" + "," + "AverageLAI(m2)" + "," + "AverageWater(mm/m)" + "," + "SubCanopyPAR(W/m2_or_mmol/m2/s)" + "," + "Litter(kgDW/m2)" + "," + "WoodyDebris(kgDW/m2)" + "," + "AverageWoodFoliage(g/m2)"+ "," + "AverageRoot(g/m2)" + "," + "AverageWood(g/m2)" + "," + "AverageFoliage(g/m2)" + "," + "AverageNSC(gC/m2)" + "," + "AverageAET(mm)");
        }
        public static void WriteNrOfCohortsBalance()
        {
            try
            {

                ISiteVar<int> CohortsPerSite = PlugIn.cohorts.GetIsiteVar(x => x.CohortCount);
                ISiteVar<float> WoodRootBiomass = PlugIn.cohorts.GetIsiteVar(x => x.BiomassSum);
                ISiteVar<float> CohortWoodySenescence = PlugIn.cohorts.GetIsiteVar(x => x.WoodySenescenceSum);
                ISiteVar<float> FoliageWoodySenescence = PlugIn.cohorts.GetIsiteVar(x => x.FoliageSenescenceSum);
                ISiteVar<int> CohortAge = PlugIn.cohorts.GetIsiteVar(x => (x.CohortCount >0) ? x.AverageAge : -1);
                ISiteVar<float> CohortLAI = PlugIn.cohorts.GetIsiteVar(x => x.CanopyLAImax);
                ISiteVar<float> WaterPerSite = PlugIn.cohorts.GetIsiteVar(x => x.WaterAvg);
                ISiteVar<float> SubCanopyRAD = PlugIn.cohorts.GetIsiteVar(x => x.JulySubCanopyPar);
                ISiteVar<double> Litter = PlugIn.cohorts.GetIsiteVar(x => x.Litter);
                ISiteVar<double> WoodyDebris = PlugIn.cohorts.GetIsiteVar(x => x.WoodyDebris);
                ISiteVar<float> WoodFoliageBiom = PlugIn.cohorts.GetIsiteVar(x => x.AbovegroundBiomassSum);
                ISiteVar<float> RootBiom = PlugIn.cohorts.GetIsiteVar(x => x.BelowGroundBiomassSum);
                ISiteVar<float> WoodBiom = PlugIn.cohorts.GetIsiteVar(x => x.WoodBiomassSum);
                ISiteVar<float> FoliageBiom = PlugIn.cohorts.GetIsiteVar(x => x.FoliageSum);
                ISiteVar<float> NSCBiom = PlugIn.cohorts.GetIsiteVar(x => x.NSCSum);
                ISiteVar<float> AET = PlugIn.cohorts.GetIsiteVar(x => x.AETSum);

                double Water_SUM = 0;
                double WoodRootBiom_SUM = 0;
                double WoodFoliageBiom_SUM = 0;
                double RootBiom_SUM = 0;
                double WoodBiom_SUM = 0;
                double FoliageBiom_SUM = 0;
                double NSCBiom_SUM = 0;
                double CohortAge_SUM = 0;
                double CohortLAI_SUM = 0;
                int CohortCount = 0;
                int siteCount = 0;
                double SubCanopyRad_SUM = 0;
                double Litter_SUM = 0;
                double Woody_debris_SUM = 0;
                double AET_SUM = 0;

                foreach (ActiveSite site in PlugIn.ModelCore.Landscape)
                {
                    siteCount++;
                    CohortCount += CohortsPerSite[site];
                    WoodRootBiom_SUM += WoodRootBiomass[site];
                    WoodFoliageBiom_SUM += WoodFoliageBiom[site];
                    RootBiom_SUM += RootBiom[site];
                    WoodBiom_SUM += WoodBiom[site];
                    FoliageBiom_SUM += FoliageBiom[site];
                    NSCBiom_SUM += NSCBiom[site];
                    Water_SUM += WaterPerSite[site];
                    SubCanopyRad_SUM += SubCanopyRAD[site];
                    Litter_SUM += Litter[site];
                    Woody_debris_SUM += WoodyDebris[site];
                    AET_SUM += AET[site];

                    if (CohortsPerSite[site] > 0)
                    {
                        CohortAge_SUM += CohortAge[site];
                        CohortLAI_SUM += CohortLAI[site];
                       
                    }
                }

                string c = CohortCount.ToString();
                string CohortAge_av = (CohortAge_SUM / (float)siteCount).ToString();
                string WoodRootBiom_av = (WoodRootBiom_SUM / (float)siteCount).ToString();
                string WoodFoliageBiom_av = (WoodFoliageBiom_SUM / (float)siteCount).ToString();
                string RootBiom_av = (RootBiom_SUM / (float)siteCount).ToString();
                string WoodBiom_av = (WoodBiom_SUM / (float)siteCount).ToString();
                string FoliageBiom_av = (FoliageBiom_SUM / (float)siteCount).ToString();
                string NSCBiom_av = (NSCBiom_SUM / (float)siteCount).ToString();
                string LAI_av = (CohortLAI_SUM / (float)siteCount).ToString();
                string Water_av = (Water_SUM / (float)siteCount).ToString();
                string SubCanopyRad_av = (SubCanopyRad_SUM / (float)siteCount).ToString();
                string Litter_av = (Litter_SUM / (float)siteCount).ToString();
                string Woody_debris_ave = (Woody_debris_SUM / (float)siteCount).ToString();
                string AET_ave = (AET_SUM / (float)siteCount).ToString();

                FileContent.Add(PlugIn.ModelCore.CurrentTime.ToString() + "," + c + "," + CohortAge_av + "," + WoodRootBiom_av + "," + LAI_av + "," + Water_av + "," + SubCanopyRad_av + "," + Litter_av + "," + Woody_debris_ave + "," + WoodFoliageBiom_av + "," + RootBiom_av + "," + WoodBiom_av + "," + FoliageBiom_av + "," + NSCBiom_av + "," + AET_ave);

                System.IO.File.WriteAllLines(FileName, FileContent.ToArray());
                 
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Cannot write to " +FileName +" "+ e.Message);
            }
            

        }
    }
    
}
