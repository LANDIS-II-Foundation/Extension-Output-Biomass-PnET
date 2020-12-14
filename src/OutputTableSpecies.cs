using System.Collections.Generic;
using Landis.Core;
using Landis.Library.Parameters.Species;

namespace Landis.Extension.Output.PnET
{

    public static class OutputTableSpecies
    {
        static string Header
        {
            get 
            { 
                string hdr = "Time";
                foreach (ISpecies species in PlugIn.ModelCore.Species) hdr += ", " + species.Name;
                return hdr;
            }
        }

        public static void WriteUpdate(string MapNameTemplate, int year, AuxParm<int> values)
        {
            string FileName  = FileNames.ReplaceTemplateVars(MapNameTemplate).Replace(".img", ".csv").Replace(".gis", ".csv");
            
            if (year == 0)
            {
                FileNames.MakeFolders(FileName);
                System.IO.File.WriteAllLines(FileName, new string[]{Header});
            }
             
            string line = year.ToString();
            foreach (ISpecies species in PlugIn.SelectedSpecies)
            {
                line += ", " + values[species] ;
            }
            System.IO.StreamWriter sw = new System.IO.StreamWriter(FileName,true);
            sw.WriteLine(line);
            sw.Close();
             

        }
        
    }
}
