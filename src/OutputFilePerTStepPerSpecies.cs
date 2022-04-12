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
                hdr += ", " + spc.Name + units;
            }
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

            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                line += ", " + Values[spc];
            }

            System.IO.StreamWriter sw = new System.IO.StreamWriter(FileName, true);
            sw.WriteLine(line);
            sw.Close();
        
        }

        // Average values across sites
        public static void Write<T>(string MapNameTemplate, string units, int TStep, ISiteVar<Landis.Library.Parameters.Species.AuxParm<T>> Values)
        {
            string FileName = FileNames.ReplaceTemplateVars(MapNameTemplate).Replace(".img", ".csv").Replace("{timestep}","AllYears");

            if (PlugIn.ModelCore.CurrentTime  == 0)
            {
                FileNames.MakeFolders(FileName);
                System.IO.File.WriteAllLines(FileName, new string[] { Header(units) });
            }

            AuxParm<ulong> Values_spc = new AuxParm<ulong>(PlugIn.ModelCore.Species);
            AuxParm<ulong> Values_cnt = new AuxParm<ulong>(PlugIn.ModelCore.Species);
            foreach (ActiveSite site in PlugIn.ModelCore.Landscape)
            {
                foreach (ISpecies spc in PlugIn.ModelCore.Species)
                {

                    if (typeof(T) == typeof(bool))
                    {
                        if (Values[site][spc].ToString() == bool.TrueString)
                        {
                            Values_spc[spc] ++;
                        }
                    }
                    else
                    {
                        ulong numeric = ulong.Parse(Values[site][spc].ToString());
                        if (!double.IsNaN((double)numeric))
                            Values_spc[spc] += numeric;
                    }

                    Values_cnt[spc]++;
                }
            }

            string line = TStep.ToString() ;

            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                line += ", " + (Values_spc[spc] / (float)Values_cnt[spc]);
            }

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

            AuxParm<ulong> Values_spc = new AuxParm<ulong>(PlugIn.ModelCore.Species);
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
                        ulong numeric = ulong.Parse(Values[site][spc].ToString());
                        if (!double.IsNaN((double)numeric))
                            Values_spc[spc] += numeric;
                    }
                }
            }

            string line = TStep.ToString() ;

            foreach (ISpecies spc in PlugIn.ModelCore.Species)
            {
                line += ", " + Values_spc[spc];
            }

            System.IO.StreamWriter sw = new System.IO.StreamWriter(FileName, true);
            sw.WriteLine(line);
            sw.Close();


        }
    }
}
