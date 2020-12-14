using Landis.Core;
using System.Collections.Generic;
 
using Landis.SpatialModeling;
using System.Linq;
using Landis.Library.Parameters.Species; 

namespace Landis.Extension.Output.PnET
{
    public class OutputTableEcoregions
    {
        List<string> FileContent = new List<string>();
        string FileName;


        public OutputTableEcoregions(string MapNameTemplate)
        {
            FileName = FileNames.ReplaceTemplateVars(MapNameTemplate).Replace(".img", "-eco.csv").Replace(".gis", "-eco.csv").Replace("{timestep}","AllYears");
            FileNames.MakeFolders(FileName);

            string hdr = "Time";
            foreach ( IEcoregion e in PlugIn.ModelCore.Ecoregions) hdr += ", " + e.Name ;

            FileContent.Add(hdr);
        }
        public void WriteUpdate<T>(int year,  ISiteVar<T> values)
        {
            string line = year.ToString();

            Landis.Library.Parameters.Ecoregions.AuxParm<double> sum_values_per_ecoregion = new Library.Parameters.Ecoregions.AuxParm<double>(PlugIn.ModelCore.Ecoregions);
            Landis.Library.Parameters.Ecoregions.AuxParm<int> cnt_values_per_ecoregion = new Library.Parameters.Ecoregions.AuxParm<int>(PlugIn.ModelCore.Ecoregions);
            Landis.Library.Parameters.Ecoregions.AuxParm<double> avg_values_per_ecoregion = new Library.Parameters.Ecoregions.AuxParm<double>(PlugIn.ModelCore.Ecoregions);
            foreach (ActiveSite site in PlugIn.ModelCore.Landscape)
            {
                sum_values_per_ecoregion[PlugIn.ModelCore.Ecoregion[site]] += double.Parse(values[site].ToString());
                cnt_values_per_ecoregion[PlugIn.ModelCore.Ecoregion[site]]++;
                avg_values_per_ecoregion[PlugIn.ModelCore.Ecoregion[site]] = sum_values_per_ecoregion[PlugIn.ModelCore.Ecoregion[site]] / ((double)cnt_values_per_ecoregion[PlugIn.ModelCore.Ecoregion[site]]);
            }
            foreach (IEcoregion e in PlugIn.ModelCore.Ecoregions)
            {
                line += ", " + avg_values_per_ecoregion[e];
            }

            FileContent.Add(line);

            System.IO.File.WriteAllLines(FileName, FileContent.ToArray());

        }
        

         
    }
}
