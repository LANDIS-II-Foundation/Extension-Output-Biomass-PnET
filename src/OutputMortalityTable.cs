using System;
using System.Collections.Generic;
using System.Text;
using Landis.Core;
using Landis.SpatialModeling;
//using Landis.Extension.Succession.BiomassPnET;
using System.Linq;

namespace Landis.Extension.Output.PnET
{
    class OutputMortalityTable
    {

        static List<string> FileContent = null;
        private static string FileName;

        public OutputMortalityTable(string Template)
        {
            FileName = Template;
            FileContent = new List<string>();
            string headerString = "Time" +", " + "Cause";
            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                headerString = headerString + ", " + spc.Name;
            }
            FileContent.Add(headerString);
        }

        public static void WriteMortalityTable()
        {
            try
            {
                int siteCount = 0;
                Dictionary<ISpecies, int> cohortsBySuccession = new Dictionary<ISpecies, int>();
                Dictionary<ISpecies, int> cohortsByHarvest = new Dictionary<ISpecies, int>();
                Dictionary<ISpecies, int> cohortsByFire = new Dictionary<ISpecies, int>();
                Dictionary<ISpecies, int> cohortsByWind = new Dictionary<ISpecies, int>();
                Dictionary<ISpecies, int> cohortsByOther = new Dictionary<ISpecies, int>();

                foreach (ActiveSite site in PlugIn.ModelCore.Landscape)
                {
                    siteCount++;
                    foreach (ISpecies spc in PlugIn.ModelCore.Species)
                    {
                        if (cohortsBySuccession.ContainsKey(spc))
                        {
                            cohortsBySuccession[spc] = cohortsBySuccession[spc] + (PlugIn.cohorts[site].CohortsBySuccession[spc.Index]);
                        }
                        else
                        {
                            cohortsBySuccession[spc] = PlugIn.cohorts[site].CohortsBySuccession[spc.Index];
                        }


                        if (cohortsByHarvest.ContainsKey(spc))
                        {
                            cohortsByHarvest[spc] = cohortsByHarvest[spc] + (PlugIn.cohorts[site].CohortsByHarvest[spc.Index]);
                        }
                        else
                        {
                            cohortsByHarvest[spc] = PlugIn.cohorts[site].CohortsByHarvest[spc.Index];
                        }


                        if (cohortsByFire.ContainsKey(spc))
                        {
                            cohortsByFire[spc] = cohortsByFire[spc] + PlugIn.cohorts[site].CohortsByFire[spc.Index];
                        }
                        else
                        {
                            cohortsByFire[spc] = PlugIn.cohorts[site].CohortsByFire[spc.Index];
                        }

                        if (cohortsByWind.ContainsKey(spc))
                        {
                            cohortsByWind[spc] = cohortsByWind[spc] + PlugIn.cohorts[site].CohortsByWind[spc.Index];
                        }
                        else
                        {
                            cohortsByWind[spc] = PlugIn.cohorts[site].CohortsByWind[spc.Index];
                        }

                        if (cohortsByOther.ContainsKey(spc))
                        {
                            cohortsByOther[spc] = cohortsByOther[spc] + PlugIn.cohorts[site].CohortsByOther[spc.Index];
                        }
                        else
                        {
                            cohortsByOther[spc] = PlugIn.cohorts[site].CohortsByOther[spc.Index];
                        }

                    }
                    PlugIn.cohorts[site].CohortsBySuccession = new List<int>(new int[PlugIn.ModelCore.Species.Count()]);
                    PlugIn.cohorts[site].CohortsByHarvest = new List<int>(new int[PlugIn.ModelCore.Species.Count()]);
                    PlugIn.cohorts[site].CohortsByFire = new List<int>(new int[PlugIn.ModelCore.Species.Count()]);
                    PlugIn.cohorts[site].CohortsByWind = new List<int>(new int[PlugIn.ModelCore.Species.Count()]);
                    PlugIn.cohorts[site].CohortsByOther = new List<int>(new int[PlugIn.ModelCore.Species.Count()]);
                }

                // Report Succession
                string dataString_succession = PlugIn.ModelCore.CurrentTime.ToString() + ", " + "Succession";
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsBySuccession.ContainsKey(spc))
                    {
                        dataString_succession = dataString_succession + ", " + cohortsBySuccession[spc];
                    }
                    else
                    {
                        dataString_succession = dataString_succession + ", " + "0";
                    }
                }
                FileContent.Add(dataString_succession);

                // Report Harvest
                string dataString_harvest = PlugIn.ModelCore.CurrentTime.ToString() + ", " + "Harvest";
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsByHarvest.ContainsKey(spc))
                    {
                        dataString_harvest = dataString_harvest + ", " + cohortsByHarvest[spc];
                    }
                    else
                    {
                        dataString_harvest = dataString_harvest + ", " + "0";
                    }
                }
                FileContent.Add(dataString_harvest);

                // Report Fire
                string dataString_fire = PlugIn.ModelCore.CurrentTime.ToString() + ", " + "Fire";
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsByFire.ContainsKey(spc))
                    {
                        dataString_fire = dataString_fire + ", " + cohortsByFire[spc];
                    }
                    else
                    {
                        dataString_fire = dataString_fire + ", " + "0";
                    }
                }
                FileContent.Add(dataString_fire);

                // Report Wind
                string dataString_wind = PlugIn.ModelCore.CurrentTime.ToString() + ", " + "Wind";
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsByWind.ContainsKey(spc))
                    {
                        dataString_wind = dataString_wind + ", " + cohortsByWind[spc];
                    }
                    else
                    {
                        dataString_wind = dataString_wind + ", " + "0";
                    }
                }
                FileContent.Add(dataString_wind);

                // Report Other
                string dataString_Other = PlugIn.ModelCore.CurrentTime.ToString() + ", " + "Other";
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsByOther.ContainsKey(spc))
                    {
                        dataString_Other = dataString_Other + ", " + cohortsByOther[spc];
                    }
                    else
                    {
                        dataString_Other = dataString_Other + ", " + "0";
                    }
                }
                FileContent.Add(dataString_Other);

                System.IO.File.WriteAllLines(FileName, FileContent.ToArray());

            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Cannot write to " + FileName + " " + e.Message);
            }
        
        }
    }
}
