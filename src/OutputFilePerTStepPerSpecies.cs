using Landis.Core;
using Landis.Library.Parameters.Species;
using System.Collections.Generic;
using Landis.SpatialModeling;

namespace Landis.Extension.Output.PnET
{
    public static class OutputFilePerTStepPerSpecies
    {
        static string Header(string units)
        {

            string hdr = "Time";
            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                hdr += ", " + spc.Name + "_" + units;
            }
            hdr += ", " + "AllSpp_" + units;
            return hdr;
            
        }
        public static void Write<T>(string MapNameTemplate, string units, int TStep, Landis.Library.Parameters.Species.AuxParm<T> Values)
        {
            string FileName = FileNames.ReplaceTemplateVars(MapNameTemplate).Replace(".img", ".csv");

            if (PlugIn.ModelCore.CurrentTime == 0)
            {
                FileNames.MakeFolders(FileName);
                System.IO.File.WriteAllLines(FileName, new string[] { Header(units) });
            }

            string line = TStep.ToString();
            float valueSum = 0;
            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                line += ", " + Values[spc];
                valueSum += float.Parse(Values[spc].ToString());
            }
            line += ", " + valueSum;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(FileName, true);
            sw.WriteLine(line);
            sw.Close();
        
        }

        // Average values across sites
        public static void Write<T>(string MapNameTemplate, string units, int TStep, ISiteVar<Landis.Library.Parameters.Species.AuxParm<T>> Values, double multiplier = 1, bool includeZeroValueSitesInAverage = true)
        {
            string FileName = FileNames.ReplaceTemplateVars(MapNameTemplate).Replace(".img", ".csv").Replace("{timestep}","AllYears");

            if (PlugIn.ModelCore.CurrentTime  == 0)
            {
                FileNames.MakeFolders(FileName);
                System.IO.File.WriteAllLines(FileName, new string[] { Header(units) });
            }

            AuxParm<float> Values_spc = new AuxParm<float>(PlugIn.ModelCore.Species);
            AuxParm<uint> Values_cnt = new AuxParm<uint>(PlugIn.ModelCore.Species);
          
            foreach (ActiveSite site in PlugIn.ModelCore.Landscape)
            {
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {
                    bool nonZeroSite = false;

                    if (typeof(T) == typeof(bool))
                    {
                        if (Values[site][spc].ToString() == bool.TrueString)
                        {
                            Values_spc[spc] ++;
                            nonZeroSite = true;
                        }
                    }
                    else
                    {
                        float numeric = float.Parse(Values[site][spc].ToString());
                        if (!double.IsNaN((double)numeric))
                        {
                            Values_spc[spc] += (ulong)(numeric * multiplier);

                            if ((numeric * multiplier) != 0)
                            {
                                nonZeroSite = true;
                            }
                        }
                    }

                    if (nonZeroSite || includeZeroValueSitesInAverage)
                    {
                        Values_cnt[spc]++;
                    }
                }
            }

            string line = TStep.ToString();
            float valueSum = 0;
            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                // Avoid divide by zero if all values are zero
                if ((float)Values_cnt[spc] == 0)
                {
                    Values_cnt[spc] = 1;
                }

                line += ", " + (Values_spc[spc] / (float)Values_cnt[spc]);
                valueSum += (Values_spc[spc] / (float)Values_cnt[spc]);
            }
            line += ", " + valueSum;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(FileName, true);
            sw.WriteLine(line);
            sw.Close();
        }

        // Sum values across sites
        public static void WriteSum<T>(string MapNameTemplate, string units, int TStep, ISiteVar<Landis.Library.Parameters.Species.AuxParm<T>> Values)
        {
            string FileName = FileNames.ReplaceTemplateVars(MapNameTemplate).Replace(".img", ".csv").Replace("{timestep}", "AllYears");

            if (PlugIn.ModelCore.CurrentTime == 0)
            {
                FileNames.MakeFolders(FileName);
                System.IO.File.WriteAllLines(FileName, new string[] { Header(units) });
            }

            AuxParm<float> Values_spc = new AuxParm<float>(PlugIn.ModelCore.Species);
            foreach (ActiveSite site in PlugIn.ModelCore.Landscape)
            {
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {

                    if (typeof(T) == typeof(bool))
                    {
                        if (Values[site][spc].ToString() == bool.TrueString)
                        {
                            Values_spc[spc]++;
                        }
                    }
                    else
                    {
                        float numeric = float.Parse(Values[site][spc].ToString());
                        if (!double.IsNaN((double)numeric))
                            Values_spc[spc] += numeric;
                    }
                }
            }

            string line = TStep.ToString() ;
            float valueSum = 0;
            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                line += ", " + Values_spc[spc];
                valueSum += float.Parse(Values_spc[spc].ToString());
            }
            line += ", " + valueSum;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(FileName, true);
            sw.WriteLine(line);
            sw.Close();


        }
    }
}
