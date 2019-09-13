using System;
using System.Collections.Generic;
using System.Text;
using Landis.Core;
using Landis.SpatialModeling;
using Landis.Extension.Succession.BiomassPnET;

namespace Landis.Extension.Output.PnET
{
    class OutputEstablishmentTable
    {

        static List<string> FileContent = null;
        private static string FileName;

        public OutputEstablishmentTable(string Template)
        {
            FileName = Template;
            FileContent = new List<string>();
            string headerString = "Time" + "\t" + "Method";
            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                headerString = headerString + "\t"  + spc.Name;
            }

            FileContent.Add(headerString);
        }

        public static void WriteEstablishmentTable()
        {
            try
            {
                int siteCount = 0;
                Dictionary<ISpecies, int> cohortsByPlant = new Dictionary<ISpecies, int>();
                Dictionary<ISpecies, int> cohortsBySerotiny = new Dictionary<ISpecies, int>();
                Dictionary<ISpecies, int> cohortsByResprout = new Dictionary<ISpecies, int>();
                Dictionary<ISpecies, int> cohortsBySeed = new Dictionary<ISpecies, int>();

                foreach (ActiveSite site in PlugIn.ModelCore.Landscape)
                {
                    siteCount++;
                    foreach (ISpecies spc in PlugIn.ModelCore.Species)
                    {
                        if (PlugIn.cohorts[site].SpeciesByPlant.Contains(spc))
                        {
                            if (cohortsByPlant.ContainsKey(spc))
                            {
                                cohortsByPlant[spc] = cohortsByPlant[spc] + 1;
                            }
                            else
                            {
                                cohortsByPlant[spc] = 1;
                            }
                        }
                        if (PlugIn.cohorts[site].SpeciesBySerotiny.Contains(spc))
                        {
                            if (cohortsBySerotiny.ContainsKey(spc))
                            {
                                cohortsBySerotiny[spc] = cohortsBySerotiny[spc] + 1;
                            }
                            else
                            {
                                cohortsBySerotiny[spc] = 1;
                            }
                        }
                        if (PlugIn.cohorts[site].SpeciesByResprout.Contains(spc))
                        {
                            if (cohortsByResprout.ContainsKey(spc))
                            {
                                cohortsByResprout[spc] = cohortsByResprout[spc] + 1;
                            }
                            else
                            {
                                cohortsByResprout[spc] = 1;
                            }
                        }
                        if (PlugIn.cohorts[site].SpeciesBySeed.Contains(spc))
                        {
                            if (cohortsBySeed.ContainsKey(spc))
                            {
                                cohortsBySeed[spc] = cohortsBySeed[spc] + 1;
                            }
                            else
                            {
                                cohortsBySeed[spc] = 1;
                            }
                        }                  
                    }
                    PlugIn.cohorts[site].SpeciesByPlant = new List<ISpecies>();
                    PlugIn.cohorts[site].SpeciesBySerotiny = new List<ISpecies>();
                    PlugIn.cohorts[site].SpeciesByResprout = new List<ISpecies>();
                    PlugIn.cohorts[site].SpeciesBySeed = new List<ISpecies>();
                }

                // Report Planting
                string dataString_Plant = PlugIn.ModelCore.CurrentTime.ToString() + "\t" + "Plant";
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsByPlant.ContainsKey(spc))
                    {
                        dataString_Plant = dataString_Plant + "\t" + cohortsByPlant[spc];
                    }
                    else
                    {
                        dataString_Plant = dataString_Plant + "\t" + "0";
                    }
                }
                FileContent.Add(dataString_Plant);

                // Report Serotiny
                string dataString_Serotiny = PlugIn.ModelCore.CurrentTime.ToString() + "\t" + "Serotiny";
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsBySerotiny.ContainsKey(spc))
                    {
                        dataString_Serotiny = dataString_Serotiny + "\t" + cohortsBySerotiny[spc];
                    }
                    else
                    {
                        dataString_Serotiny = dataString_Serotiny + "\t" + "0";
                    }
                }
                FileContent.Add(dataString_Serotiny);

                // Report Resprout
                string dataString_Resprout = PlugIn.ModelCore.CurrentTime.ToString() + "\t" + "Resprout";
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsByResprout.ContainsKey(spc))
                    {
                        dataString_Resprout = dataString_Resprout + "\t" + cohortsByResprout[spc];
                    }
                    else
                    {
                        dataString_Resprout = dataString_Resprout + "\t" + "0";
                    }
                }
                FileContent.Add(dataString_Resprout);

                // Report Seed
                string dataString_Seed = PlugIn.ModelCore.CurrentTime.ToString() + "\t" + "Seed";
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    if (cohortsBySeed.ContainsKey(spc))
                    {
                        dataString_Seed = dataString_Seed + "\t" + cohortsBySeed[spc];
                    }
                    else
                    {
                        dataString_Seed = dataString_Seed + "\t" + "0";
                    }
                }

                FileContent.Add(dataString_Seed);
                System.IO.File.WriteAllLines(FileName, FileContent.ToArray());

            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Cannot write to " + FileName + " " + e.Message);
            }
        
        }
    }
}
