using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data;
using System.Text;
using Landis.Library.Metadata;
using Landis.Core;
using Landis.Utilities;
using System.IO;
using Landis.SpatialModeling;

namespace Landis.Extension.Output.PnET
{
    public static class MetadataHandler
    {

        public static ExtensionMetadata Extension { get; set; }

        public static void InitializeMetadata(int Timestep, OutputVariable LAI, OutputVariable WoodRootBiomass, OutputVariable WoodFoliageBiomass, OutputVariable WoodBiomass, OutputVariable EstablishmentProbability, ISiteVar<Landis.Library.Parameters.Species.AuxParm<bool>> SpeciesWasThere, OutputVariable AnnualPsn, OutputVariable RootBiomass, OutputVariable FoliageBiomass, OutputVariable CohortsPerSpc, OutputVariable Water, OutputVariable SubCanopyPAR, OutputVariable NonWoodyDebris, OutputVariable WoodyDebris, OutputVariable AgeDistribution, OutputVariable MonthlyFolResp, OutputVariable MonthlyGrossPsn, OutputVariable MonthlyNetPsn, OutputVariable MonthlyMaintResp, OutputVariable Albedo, OutputVariable MonthlyActiveLayerDepth, OutputVariable MonthlyFrostDepth, OutputVariable SpeciesEstablishment, OutputVariable NSC, OutputVariable PET, ISiteVar<Library.Parameters.Species.AuxParm<int>> LastBiom, OutputAggregatedTable overalloutputs, string OutputTableMap)
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
            //          table outputs:   
            //---------------------------------------
            if (overalloutputs != null)
            {
                PlugIn.ModelCore.UI.WriteLine("   Generating summary table...");
                CreateDirectory(OutputTableMap);

                OutputMetadata tblOut_OverallOutputs = new OutputMetadata()
                {
                    Type = OutputType.Table,
                    Name = Path.GetFileName(OutputTableMap),
                    FilePath = OutputTableMap,
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                };
                tblOut_OverallOutputs.RetriveFields(typeof(OutputAggregatedTable));
                Extension.OutputMetadatas.Add(tblOut_OverallOutputs);
            }


            //---------------------------------------            
            //          map outputs:         
            //---------------------------------------
            

            if(LAI != null)
            {
                foreach (ISpecies spc in PlugIn.SelectedSpecies)
                {
                    OutputMetadata mapOut_LAI = new OutputMetadata()
                    {
                        Type = OutputType.Map,
                        //Name = FileNames.ReplaceTemplateVars(LAI.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                        Name = FileNames.ReplaceTemplateVars(LAI.MapNameTemplate, spc.Name),
                        //FilePath = FileNames.ReplaceTemplateVars(LAI.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                        FilePath = FileNames.ReplaceTemplateVars(LAI.MapNameTemplate, spc.Name),
                        Map_DataType = MapDataType.Continuous,
                        Visualize = true,
                        //Map_Unit = "categorical",
                    };
                    Extension.OutputMetadatas.Add(mapOut_LAI);
                }
            }

            if(WoodRootBiomass != null)
            {
                foreach(ISpecies spc in PlugIn.SelectedSpecies)
                {
                    OutputMetadata mapOut_WoodRootBiomass = new OutputMetadata()
                    {
                        Type = OutputType.Map,
                        Name = FileNames.ReplaceTemplateVars(WoodRootBiomass.MapNameTemplate, spc.Name),
                        FilePath = FileNames.ReplaceTemplateVars(WoodRootBiomass.MapNameTemplate, spc.Name),
                        Map_DataType = MapDataType.Continuous,
                        Visualize = true,
                        //Map_Unit = "categorical",
                    };
                    Extension.OutputMetadatas.Add(mapOut_WoodRootBiomass);
                }
            }
            if(WoodFoliageBiomass != null)
            {
                foreach(ISpecies spc in PlugIn.SelectedSpecies)
                {
                    OutputMetadata mapOut_WoodFoliageBiomass = new OutputMetadata()
                    {
                        Type = OutputType.Map,
                        //Name = FileNames.ReplaceTemplateVars(AbovegroundBiomass.MapNameTemplate, spc.Name, PlugIn.ModelCore.CurrentTime),
                        Name = FileNames.ReplaceTemplateVars(WoodFoliageBiomass.MapNameTemplate, spc.Name),
                        //FilePath = FileNames.ReplaceTemplateVars(AbovegroundBiomass.MapNameTemplate, spc.Name, PlugIn.ModelCore.CurrentTime),
                        FilePath = FileNames.ReplaceTemplateVars(WoodFoliageBiomass.MapNameTemplate, spc.Name),
                        Map_DataType = MapDataType.Continuous,
                        Visualize = true,
                        //Map_Unit = "categorical",
                    };
                    Extension.OutputMetadatas.Add(mapOut_WoodFoliageBiomass);
                }
            }
            if (WoodBiomass != null)
            {
                foreach (ISpecies spc in PlugIn.SelectedSpecies)
                {
                    OutputMetadata mapOut_WoodBiomass = new OutputMetadata()
                    {
                        Type = OutputType.Map,
                        Name = FileNames.ReplaceTemplateVars(WoodBiomass.MapNameTemplate, spc.Name),
                        FilePath = FileNames.ReplaceTemplateVars(WoodBiomass.MapNameTemplate, spc.Name),
                        Map_DataType = MapDataType.Continuous,
                        Visualize = true,
                        //Map_Unit = "categorical",
                    };
                    Extension.OutputMetadatas.Add(mapOut_WoodBiomass);
                }
            }
            if (MonthlyFolResp != null)
            {
                OutputMetadata mapOut_Monthly = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    //Name = FileNames.ReplaceTemplateVars(MonthlyFolResp.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Name = FileNames.ReplaceTemplateVars(MonthlyFolResp.MapNameTemplate),
                    //FilePath = FileNames.ReplaceTemplateVars(MonthlyFolResp.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(MonthlyFolResp.MapNameTemplate),
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
            if (Albedo != null)
            {
                OutputMetadata mapOut_Monthly = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(Albedo.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(Albedo.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_Monthly);
            }
            if (MonthlyActiveLayerDepth != null)
            {
                OutputMetadata mapOut_Monthly = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(MonthlyActiveLayerDepth.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(MonthlyActiveLayerDepth.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_Monthly);
            }
            if (MonthlyFrostDepth != null)
            {
                OutputMetadata mapOut_Monthly = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(MonthlyFrostDepth.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(MonthlyFrostDepth.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_Monthly);
            }
            if (RootBiomass != null)
            {
                OutputMetadata mapOut_RootBiomass = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(RootBiomass.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(RootBiomass.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_RootBiomass);
            }
            if (FoliageBiomass != null)
            {
                OutputMetadata mapOut_FoliageBiomass = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(FoliageBiomass.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(FoliageBiomass.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_FoliageBiomass);
            }
            if (CohortsPerSpc != null)
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
                    Name = FileNames.ReplaceTemplateVars(SpeciesEstablishment.MapNameTemplate).Replace(".img", ".csv").Replace("{timestep}","AllYears"),
                    FilePath = FileNames.ReplaceTemplateVars(SpeciesEstablishment.MapNameTemplate).Replace(".img", ".csv").Replace("{timestep}", "AllYears"),
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
            if (NSC != null)
            {
                OutputMetadata mapOut_NSC = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(NSC.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(NSC.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_NSC);
            }
            if (PET != null)
            {
                OutputMetadata mapOut_PET = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = FileNames.ReplaceTemplateVars(PET.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    FilePath = FileNames.ReplaceTemplateVars(PET.MapNameTemplate, "", PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_PET);
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
                Landis.Utilities.Directory.EnsureExists(dir);
            }

            //return new StreamWriter(path);
            return;
        }
    }
}