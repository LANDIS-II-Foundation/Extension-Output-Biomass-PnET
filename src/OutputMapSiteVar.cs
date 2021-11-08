﻿using Landis.SpatialModeling;
using System;

namespace Landis.Extension.Output.PnET
{
    public class OutputMapSiteVar<T,M>
    {
         

        public OutputMapSiteVar(string FileName, ISiteVar<T> values, Func<T, M> func)
        {
            try
            {
                using (IOutputRaster<IntPixel> outputRaster = PlugIn.ModelCore.CreateRaster<IntPixel>(FileName, PlugIn.ModelCore.Landscape.Dimensions))
                {
                    foreach (Site site in PlugIn.ModelCore.Landscape.AllSites)
                    {
                        if (site.IsActive)
                        {
   
                            try
                            {
                                outputRaster.BufferPixel.MapCode.Value = (int)double.Parse(func(values[site]).ToString());// int.Parse(values[site].ToString());

                            }
                            catch (System.Exception e)
                            {
                                System.Console.WriteLine("Cannot write " + FileName + " for site " + site.Location.ToString() + " " + e.Message);
                            }
                        }
                        else outputRaster.BufferPixel.MapCode.Value = 0;

                        outputRaster.WriteBufferPixel();
                    }
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Cannot write " + FileName + " " + e.Message);

            }
        }

        public OutputMapSiteVar(string FileName, ISiteVar<float> values, bool convertToPercentage = false)
        {
            int multiplier = convertToPercentage ? 100 : 1;

            try
            {
                using (IOutputRaster<FloatPixel> outputRaster = PlugIn.ModelCore.CreateRaster<FloatPixel>(FileName, PlugIn.ModelCore.Landscape.Dimensions))
                {
                    foreach (Site site in PlugIn.ModelCore.Landscape.AllSites)
                    {
                        try
                        {
                            outputRaster.BufferPixel.MapCode.Value = values[site] * multiplier;// int.Parse(values[site].ToString());
                        }
                        catch (System.Exception e)
                        {
                            System.Console.WriteLine("Cannot write " + FileName + " for site " + site.Location.ToString() + " " + e.Message);
                        }

                        outputRaster.WriteBufferPixel();
                    }
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Cannot write " + FileName + " " + e.Message);

            }
        }
    }
}
