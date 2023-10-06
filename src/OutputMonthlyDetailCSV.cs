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
            this.header = "Time, " + "January" + "_" + units + ", " + "February" + "_" + units + ", " + "March" + "_" + units + ", "
                + "April" + "_" + units + ", " + "May" + "_" + units + ", " + "June" + "_" + units + ", " + "July" + "_" + units + ", "
                + "August" + "_" + units + ", " + "September" + "_" + units + ", " + "October" + "_" + units + ", "
                + "November" + "_" + units + ", " + "December" + "_" + units;

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
            sw.WriteLine(TStep + ", " + monthlySums[0] + ", " + monthlySums[1] + ", " + monthlySums[2] + ", " + monthlySums[3] + ", "
                + monthlySums[4] + ", " + monthlySums[5] + ", " + monthlySums[6] + ", " + monthlySums[7] + ", " + monthlySums[8] + ", "
                + monthlySums[9] + ", " + monthlySums[10] + ", " + monthlySums[11]);
            sw.Close();
        }
    }
}