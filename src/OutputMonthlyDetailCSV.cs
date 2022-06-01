using Landis.Core;
using Landis.SpatialModeling;
using System.Collections.Generic;


namespace Landis.Extension.Output.PnET
{
    public class OutputMonthlyDetailCSV
    {
        private string NameTemplate;
        private string units;
        private string header;

        public OutputMonthlyDetailCSV(string NameTemplate,
                              string units)
        {
            this.NameTemplate = NameTemplate;
            this.units = units;
            this.header = "Timestep,\t" + "January" + "_(" + units + "),\t" + "February" + "_(" + units + "),\t" + "March" + "_(" + units + "),\t"
                + "April" + "_(" + units + "),\t" + "May" + "_(" + units + "),\t" + "June" + "_(" + units + "),\t" + "July" + "_(" + units + "),\t"
                + "August" + "_(" + units + "),\t" + "September" + "_(" + units + "),\t" + "October" + "_(" + units + "),\t"
                + "November" + "_(" + units + "),\t" + "December" + "_(" + units + ")";

            if (!NameTemplate.Contains(".csv")) throw new System.Exception("NameTemplate " + NameTemplate + " does not have an extension '.csv'");
            if (NameTemplate.Length == 0) throw new System.Exception("Error initializing output CSV, no template name available");

            FileNames.MakeFolders(NameTemplate);
            System.IO.File.WriteAllLines(NameTemplate, new string[] { header });
        }

        public void WriteMonthlyDetail(int TStep, ISiteVar<float[]> values, int multiplier = 1)
        {
            float[] monthlySums = new float[12];

            // Get the sum from all the sites for every month this time step
            for (int mo = 0; mo < 12; mo++)
            {
                foreach (ActiveSite site in PlugIn.ModelCore.Landscape)
                {
                    monthlySums[mo] += values[site][mo];
                }

                // Average the sums and apply multiplier
                monthlySums[mo] = (monthlySums[mo] / PlugIn.ModelCore.Landscape.ActiveSiteCount) * multiplier;
            }
            System.IO.StreamWriter sw = new System.IO.StreamWriter(this.NameTemplate, true);
            sw.WriteLine(TStep + ",\t" + monthlySums[0] + ",\t" + monthlySums[1] + ",\t" + monthlySums[2] + ",\t" + monthlySums[3] + ",\t"
                + monthlySums[4] + ",\t" + monthlySums[5] + ",\t" + monthlySums[6] + ",\t" + monthlySums[7] + ",\t" + monthlySums[8] + ",\t"
                + monthlySums[9] + ",\t" + monthlySums[10] + ",\t" + monthlySums[11]);
            sw.Close();
        }
    }
}