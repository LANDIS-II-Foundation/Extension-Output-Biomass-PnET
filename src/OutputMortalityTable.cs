using System;
using System.Collections.Generic;
using System.Text;
using Landis.Core;
using Landis.SpatialModeling;
using Landis.Extension.Succession.BiomassPnET;

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
            string headerString = "Time";
            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                headerString = headerString + "\t" + "Succession_" + spc.Name;
            }
            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                headerString = headerString + "\t" + "Harvest_" + spc.Name;
            }
            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                headerString = headerString + "\t" + "Fire_" + spc.Name;
            }
            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                headerString = headerString + "\t" + "Wind_" + spc.Name;
            }
            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                headerString = headerString + "\t" + "Other_" + spc.Name;
            }
            FileContent.Add(headerString);
        }

        /*public static void WriteMortalityTable()
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
                        if (PlugIn.cohorts[site].CohortsBySuccession.Contains(spc))
                        {
                            if (cohortsBySuccession.ContainsKey(spc))
                            {
                                cohortsBySuccession[spc] = cohortsBySuccession[spc] + 1;
                            }
                            else
                            {
                                cohortsBySuccession[spc] = 1;
                            }
                        }
                        if (PlugIn.cohorts[site].CohortsByHarvet.Contains(spc))
                        {
                            if (cohortsByHarvest.ContainsKey(spc))
                            {
                                cohortsByHarvest[spc] = cohortsByHarvest[spc] + 1;
                            }
                            else
                            {
                                cohortsByHarvest[spc] = 1;
                            }
                        }
                        if (PlugIn.cohorts[site].CohortsByFire.Contains(spc))
                        {
                            if (cohortsByFire.ContainsKey(spc))
                            {
                                cohortsByFire[spc] = cohortsByFire[spc] + 1;
                            }
                            else
                            {
                                cohortsByFire[spc] = 1;
                            }
                        }
                        if (PlugIn.cohorts[site].CohortsByWind.Contains(spc))
                        {
                            if (cohortsByWind.ContainsKey(spc))
                            {
                                cohortsByWind[spc] = cohortsByWind[spc] + 1;
                            }
                            else
                            {
                                cohortsByWind[spc] = 1;
                            }
                        }
                        if (PlugIn.cohorts[site].CohortsByOther.Contains(spc))
                        {
                            if (cohortsByOther.ContainsKey(spc))
                            {
                                cohortsByOther[spc] = cohortsByOther[spc] + 1;
                            }
                            else
                            {
                                cohortsByOther[spc] = 1;
                            }
                        }
                    }
                    PlugIn.cohorts[site].CohortsBySuccession = new List<ISpecies>();
                    PlugIn.cohorts[site].CohortsByHarvet = new List<ISpecies>();
                    PlugIn.cohorts[site].CohortsByFire = new List<ISpecies>();
                    PlugIn.cohorts[site].CohortsByWind = new List<ISpecies>();
                    PlugIn.cohorts[site].CohortsByOther = new List<ISpecies>();
                }

                string dataString = PlugIn.ModelCore.CurrentTime.ToString();

                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsBySuccession.ContainsKey(spc))
                    {
                        dataString = dataString + "\t" + cohortsBySuccession[spc];
                    }
                    else
                    {
                        dataString = dataString + "\t" + "0";
                    }
                }
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsByHarvest.ContainsKey(spc))
                    {
                        dataString = dataString + "\t" + cohortsByHarvest[spc];
                    }
                    else
                    {
                        dataString = dataString + "\t" + "0";
                    }
                }
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsByFire.ContainsKey(spc))
                    {
                        dataString = dataString + "\t" + cohortsByFire[spc];
                    }
                    else
                    {
                        dataString = dataString + "\t" + "0";
                    }
                }
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsByWind.ContainsKey(spc))
                    {
                        dataString = dataString + "\t" + cohortsByWind[spc];
                    }
                    else
                    {
                        dataString = dataString + "\t" + "0";
                    }
                }
                foreach(ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsByOther.ContainsKey(spc))
                    {
                        dataString = dataString + "\t" + cohortsByOther[spc];
                    }
                    else
                    {
                        dataString = dataString + "\t" + "0";
                    }
                }

                FileContent.Add(dataString);
                System.IO.File.WriteAllLines(FileName, FileContent.ToArray());

            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Cannot write to " + FileName + " " + e.Message);
            }
        
        }*/
    }
}
