using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data;
using System.Text;
using Landis.Library.Metadata;
using Landis.Core;
using Edu.Wisc.Forest.Flel.Util;
using System.IO;
using Flel = Edu.Wisc.Forest.Flel;
using Landis.SpatialModeling;

namespace Landis.Extension.Output.PnET
{
    public static class MetadataHandler
    {

        public static ExtensionMetadata Extension { get; set; }

        public static void InitializeMetadata(int Timestep, OutputVariable LAI, OutputVariable Biomass, OutputVariable AbovegroundBiomass, OutputVariable EstablishmentProbability, ISiteVar<Landis.Library.Parameters.Species.AuxParm<bool>> SpeciesWasThere, OutputVariable AnnualPsn, OutputVariable BelowGround, OutputVariable CohortsPerSpc, OutputVariable Water, OutputVariable SubCanopyPAR, OutputVariable NonWoodyDebris, OutputVariable WoodyDebris, OutputVariable AgeDistribution, OutputVariable MonthlyFolResp, OutputVariable MonthlyGrossPsn, OutputVariable MonthlyNetPsn, OutputVariable MonthlyMaintResp, OutputVariable SpeciesEstablishment, ISiteVar<Library.Parameters.Species.AuxParm<int>> LastBiom, OutputAggregatedTable overalloutputs, string OutputTableMap)
        {

            ScenarioReplicationMetadata scenRep = new ScenarioReplicationMetadata()
            {
                RasterOutCellArea = PlugIn.ModelCore.CellArea,
                TimeMin = PlugIn.ModelCore.StartTime,
                TimeMax = PlugIn.ModelCore.EndTime,
            };

            Extension = new ExtensionMetadata(PlugIn.ModelCore)
            //Extension = new ExtensionMetadata()
            {
                Name = PlugIn.ExtensionName,
                TimeInterval = Timestep,
                ScenarioReplicationMetadata = scenRep
            };

            //---------------------------------------            
            //          map outputs:         
            //---------------------------------------

            //OutputMetadata mapOut_BiomassRemoved = new OutputMetadata()
            //{
            //    Type = OutputType.Map,
            //    Name = "biomass removed",
            //    FilePath = @HarvestMapName,
            //    Map_DataType = MapDataType.Continuous,
            //    Map_Unit = FieldUnits.Mg_ha,
            //    Visualize = true,
            //};
            //Extension.OutputMetadatas.Add(mapOut_BiomassRemoved);

            if(LAI != null)
            {
                OutputMetadata mapOut_LAI = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(LAI.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(LAI.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_LAI);
            }

            if(Biomass != null)
            {
                foreach(ISpecies spc in PlugIn.SelectedSpecies)
                {
                    OutputMetadata mapOut_Biomass = new OutputMetadata()
                    {
                        Type = OutputType.Map,
                        Name = FileNames.ReplaceTemplateVars(Biomass.MapNameTemplate, spc.Name, PlugIn.ModelCore.CurrentTime),
                        FilePath = FileNames.ReplaceTemplateVars(Biomass.MapNameTemplate, spc.Name, PlugIn.ModelCore.CurrentTime),
                        Map_DataType = MapDataType.Continuous,
                        Visualize = true,
                        //Map_Unit = "categorical",
                    };
                    Extension.OutputMetadatas.Add(mapOut_Biomass);
                }
            }
            if(AbovegroundBiomass != null)
            {
                foreach(ISpecies spc in PlugIn.SelectedSpecies)
                {
                    OutputMetadata mapOut_AbvBiomass = new OutputMetadata()
                    {
                        Type = OutputType.Map,
                        Name = FileNames.ReplaceTemplateVars(AbovegroundBiomass.MapNameTemplate, spc.Name, PlugIn.ModelCore.CurrentTime),
                        FilePath = FileNames.ReplaceTemplateVars(AbovegroundBiomass.MapNameTemplate, spc.Name, PlugIn.ModelCore.CurrentTime),
                        Map_DataType = MapDataType.Continuous,
                        Visualize = true,
                        //Map_Unit = "categorical",
                    };
                    Extension.OutputMetadatas.Add(mapOut_AbvBiomass);
                }
            }
            if(MonthlyFolResp != null)
            {
                OutputMetadata mapOut_Monthly = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(MonthlyFolResp.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(MonthlyFolResp.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_Monthly);
            }
            if(MonthlyGrossPsn != null)
            {
                OutputMetadata mapOut_Monthly = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(MonthlyGrossPsn.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(MonthlyGrossPsn.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_Monthly);
            }
            if(MonthlyNetPsn != null)
            {
                OutputMetadata mapOut_Monthly = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(MonthlyNetPsn.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(MonthlyNetPsn.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_Monthly);
            }
            if(MonthlyMaintResp != null)
            {
                OutputMetadata mapOut_Monthly = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(MonthlyMaintResp.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(MonthlyMaintResp.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_Monthly);
            }
            if(BelowGround != null)
            {
                OutputMetadata mapOut_BelowGround = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(BelowGround.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(BelowGround.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_BelowGround);
            }
            if(CohortsPerSpc != null)
            {
                foreach(ISpecies spc in PlugIn.ModelCore.Species)
                {
                    OutputMetadata mapOut_LAI = new OutputMetadata()
                    {
                        Type = OutputType.Map,
                        Name = FileNames.ReplaceTemplateVars(CohortsPerSpc.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                        FilePath = FileNames.ReplaceTemplateVars(CohortsPerSpc.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                        Map_DataType = MapDataType.Continuous,
                        Visualize = true,
                        //Map_Unit = "categorical",
                    };
                    Extension.OutputMetadatas.Add(mapOut_LAI);
                }
            }
            if (EstablishmentProbability != null)
            {
                foreach (ISpecies spc in PlugIn.SelectedSpecies)
                {
                    OutputMetadata mapOut_EProb = new OutputMetadata()
                    {
                        Type = OutputType.Map,
                        Name = FileNames.ReplaceTemplateVars(EstablishmentProbability.MapNameTemplate, spc.Name, PlugIn.ModelCore.CurrentTime),
                        FilePath = FileNames.ReplaceTemplateVars(EstablishmentProbability.MapNameTemplate, spc.Name, PlugIn.ModelCore.CurrentTime),
                        Map_DataType = MapDataType.Continuous,
                        Visualize = true,
                        //Map_Unit = "categorical",
                    };
                    Extension.OutputMetadatas.Add(mapOut_EProb);
                }
            }
            if(SpeciesEstablishment != null)
            {
                if (SpeciesWasThere != null)
                {
                        foreach(ISpecies spc in PlugIn.ModelCore.Species)
                        {
                            OutputMetadata mapOut_SpeciesE = new OutputMetadata()
                            {
                                Type = OutputType.Map,
                                Name = FileNames.ReplaceTemplateVars(SpeciesEstablishment.MapNameTemplate, spc.Name, PlugIn.ModelCore.CurrentTime),
                                FilePath = FileNames.ReplaceTemplateVars(SpeciesEstablishment.MapNameTemplate, spc.Name, PlugIn.ModelCore.CurrentTime),
                                Map_DataType = MapDataType.Continuous,
                                Visualize = true,
                                //Map_Unit = "categorical",
                            };
                            Extension.OutputMetadatas.Add(mapOut_SpeciesE);
                        }
                }

                OutputMetadata mapOut_SpeciesE2 = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(SpeciesEstablishment.MapNameTemplate).Replace(".img", ".txt"),
                    FilePath = FileNames.ReplaceTemplateVars(SpeciesEstablishment.MapNameTemplate).Replace(".img", ".txt"),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_SpeciesE2);
            }
            if(AnnualPsn != null)
            {
                if(LastBiom != null)
                {
                        foreach(ISpecies spc in PlugIn.ModelCore.Species)
                        {
                            OutputMetadata mapOut_AnnualPsn = new OutputMetadata()
                            {
                                Type = OutputType.Map,
                                Name = FileNames.ReplaceTemplateVars(AnnualPsn.MapNameTemplate, spc.Name, PlugIn.ModelCore.CurrentTime),
                                FilePath = FileNames.ReplaceTemplateVars(AnnualPsn.MapNameTemplate, spc.Name, PlugIn.ModelCore.CurrentTime),
                                Map_DataType = MapDataType.Continuous,
                                Visualize = true,
                                //Map_Unit = "categorical",
                            };
                            Extension.OutputMetadatas.Add(mapOut_AnnualPsn);
                        }
                }
            }
            if(Water != null)
            {
                    OutputMetadata mapOut_Water = new OutputMetadata()
                    {
                        Type = OutputType.Map,
                        Name = FileNames.ReplaceTemplateVars(Water.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                        FilePath = FileNames.ReplaceTemplateVars(Water.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                        Map_DataType = MapDataType.Continuous,
                        Visualize = true,
                        //Map_Unit = "categorical",
                    };
                    Extension.OutputMetadatas.Add(mapOut_Water);
            }
            if(SubCanopyPAR != null)
            {
                    OutputMetadata mapOut_SubCanopyPAR = new OutputMetadata()
                    {
                        Type = OutputType.Map,
                        Name = FileNames.ReplaceTemplateVars(SubCanopyPAR.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                        FilePath = FileNames.ReplaceTemplateVars(SubCanopyPAR.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                        Map_DataType = MapDataType.Continuous,
                        Visualize = true,
                        //Map_Unit = "categorical",
                    };
                    Extension.OutputMetadatas.Add(mapOut_SubCanopyPAR);
            }
            if(NonWoodyDebris != null)
            {
                OutputMetadata mapOut_NonWoodyDebris = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(NonWoodyDebris.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(NonWoodyDebris.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_NonWoodyDebris);
            }
            if(WoodyDebris != null)
            {
                OutputMetadata mapOut_WoodyDebris = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(WoodyDebris.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(WoodyDebris.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_WoodyDebris);
            }
            if(AgeDistribution != null)
            {
                OutputMetadata mapOut_AgeDistribution = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(AgeDistribution.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(AgeDistribution.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_AgeDistribution);
            }
            if(overalloutputs != null)
            {
                OutputMetadata tblOut_OverallOutputs = new OutputMetadata()
                {
                    Type = OutputType.Table,
                    Name = FileNames.ReplaceTemplateVars(OutputTableMap, "Overall", PlugIn.ModelCore.CurrentTime).Replace(".img", ".txt"),
                    FilePath = FileNames.ReplaceTemplateVars(OutputTableMap, "Overall", PlugIn.ModelCore.CurrentTime).Replace(".img", ".txt"),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(tblOut_OverallOutputs);
            }

            //---------------------------------------
            MetadataProvider mp = new MetadataProvider(Extension);
            mp.WriteMetadataToXMLFile("Metadata", Extension.Name, Extension.Name);
        }
        public static void CreateDirectory(string path)
        {
            //Require.ArgumentNotNull(path);
            path = path.Trim(null);
            if (path.Length == 0)
                throw new ArgumentException("path is empty or just whitespace");

            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir))
            {
                Flel.Util.Directory.EnsureExists(dir);
            }

            //return new StreamWriter(path);
            return;
        }
    }
}