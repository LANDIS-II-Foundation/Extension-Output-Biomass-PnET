using Landis.Core;
using Landis.SpatialModeling;
using System.Collections.Generic;


namespace Landis.Extension.Output.PnET
{
    public class OutputCSV
    {
        private string NameTemplate;
        private string units;
        private string header;

        public OutputCSV(string NameTemplate,
                              string units)
        {
            this.NameTemplate = NameTemplate;
            this.units = units;

            if (!NameTemplate.Contains(".csv")) throw new System.Exception("NameTemplate " + NameTemplate + " does not have an extension '.csv'");
            if (NameTemplate.Length == 0) throw new System.Exception("Error initializing output CSV, no template name available");
        }

        public OutputCSV(string NameTemplate,
                              string units,
                              string variableName)
        {
            this.NameTemplate = NameTemplate;
            this.units = units;
            this.header = "Timestep\t" + variableName + " " + units;

            if (!NameTemplate.Contains(".csv")) throw new System.Exception("NameTemplate " + NameTemplate + " does not have an extension '.csv'");
            if (NameTemplate.Length == 0) throw new System.Exception("Error initializing output CSV, no template name available");

            FileNames.MakeFolders(NameTemplate);
            System.IO.File.WriteAllLines(NameTemplate, new string[] { header });
        }

        public void WriteAverageFromMonthly(int TStep, ISiteVar<float[]> values)
        {
            float sum = 0;

            // Get the sum from all the sites for every month this time step
            for (int mo = 0; mo < 12; mo++)
            {
                foreach (ActiveSite site in PlugIn.ModelCore.Landscape)
                {
                    sum += values[site][mo];
                }
            }

            float average = sum / PlugIn.ModelCore.Landscape.ActiveSiteCount;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(this.NameTemplate, true);
            sw.WriteLine(TStep + "\t" + average);
            sw.Close();
        }
    }
}