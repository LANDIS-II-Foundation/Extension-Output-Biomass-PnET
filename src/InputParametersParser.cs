//  Authors:  Arjan de Bruijn 

using Landis.Core;
using Landis.Utilities;
using System.Collections.Generic;
using Landis.Library.Parameters;

namespace Landis.Extension.Output.PnET
{
    /// <summary>
    /// A parser that reads the plug-in's parameters from text input.
    /// </summary>
    public class InputParametersParser
        : Landis.Utilities.TextParser<InputParameters>
    {

        //---------------------------------------------------------------------

        public InputParametersParser()
        {
        }

        //---------------------------------------------------------------------

        protected override InputParameters Parse()
        {
            InputVar<string> landisData = new InputVar<string>("LandisData");
            ReadVar(landisData);
            if (landisData.Value.Actual != PlugIn.ExtensionName)
                throw new InputValueException(landisData.Value.String, "The value is not \"{0}\"", PlugIn.ExtensionName);

            InputParameters parameters = new InputParameters();

            InputVar<int> timestep = new InputVar<int>("Timestep");
            ReadVar(timestep);
            parameters.Timestep = timestep.Value;

          

            //  Check for optional pair of parameters for species:
            //      Species
            //      MapNames
            InputVar<string> speciesName = new InputVar<string>("Species");
            InputVar<string> WoodRootBiomass = new InputVar<string>("Wood-RootBiomass");
            InputVar<string> WoodFoliageBiomass = new InputVar<string>("Wood-FoliageBiomass");
            InputVar<string> woodBiomass = new InputVar<string>("WoodBiomass");
            InputVar<string> woodySenescence = new InputVar<string>("WoodySenescence");
            InputVar<string> foliageSenescence = new InputVar<string>("FoliageSenescence");
            InputVar<string> aet = new InputVar<string>("AET");
            InputVar<string> aetAvg = new InputVar<string>("AETAvg");
            InputVar<string> LeafAreaIndex = new InputVar<string>("LeafAreaIndex");
            InputVar<string> Establishment = new InputVar<string>("Establishment");
            InputVar<string> EstablishmentProbability = new InputVar<string>("EstablishmentProbability");
            InputVar<string> MonthlyNetPsn = new InputVar<string>("MonthlyNetPsn");
            InputVar<string> MonthlyFolResp = new InputVar<string>("MonthlyFolResp");
            InputVar<string> MonthlyGrossPsn = new InputVar<string>("MonthlyGrossPsn");
            InputVar<string> MonthlyMaintResp = new InputVar<string>("MonthlyMaintResp");
            InputVar<string> Albedo = new InputVar<string>("Albedo");
            InputVar<string> MonthlyActiveLayerDepth = new InputVar<string>("MonthlyActiveLayerDepth");
            InputVar<string> MonthlyFrostDepth = new InputVar<string>("MonthlyFrostDepth");
            InputVar<string> MonthlyAvgSnowPack = new InputVar<string>("MonthlyAvgSnowPack");
            InputVar<string> MonthlyAvgWater = new InputVar<string>("MonthlyAvgWater");
            InputVar<string> MonthlyAvgLAI = new InputVar<string>("MonthlyAvgLAI");
            InputVar<string> MonthlyEvap = new InputVar<string>("MonthlyEvap");
            InputVar<string> MonthlyInterception = new InputVar<string>("MonthlyInterception");
            InputVar<string> MonthlyActualTrans = new InputVar<string>("MonthlyActualTrans");
            InputVar<string> MonthlyLeakage = new InputVar<string>("MonthlyLeakage");
            InputVar<string> MonthlyRunoff = new InputVar<string>("MonthlyRunoff");
            InputVar<string> MonthlyAET = new InputVar<string>("MonthlyAET");
            InputVar<string> MonthlyPotentialEvap = new InputVar<string>("MonthlyPotentialEvap");
            InputVar<string> MonthlyPotentialTrans = new InputVar<string>("MonthlyPotentialTrans");

            InputVar<string> Water = new InputVar<string>("Water");
            InputVar<string> SubCanopyPAR = new InputVar<string>("SubCanopyPAR");
            InputVar<string> RootBiomass = new InputVar<string>("RootBiomass");
            InputVar<string> FoliageBiomass = new InputVar<string>("FoliageBiomass");
            InputVar<string> CohortsPerSpecies = new InputVar<string>("CohortsPerSpecies");
            InputVar<string> AnnualPsn = new InputVar<string>("AnnualPsn");
            InputVar<string> WoodyDebris = new InputVar<string>("WoodyDebris");
            InputVar<string> Litter = new InputVar<string>("Litter");
            InputVar<string> AgeDistribution = new InputVar<string>("AgeDistribution");
            InputVar<string> CohortBalance = new InputVar<string>("CohortBalance");
            InputVar<string> EstablishmentTable = new InputVar<string>("EstablishmentTable");
            InputVar<string> MortalityTable = new InputVar<string>("MortalityTable");
            InputVar<string> NSC = new InputVar<string>("NSC");
            InputVar<string> PET = new InputVar<string>("PET");
            InputVar<string> SiteMossDepth = new InputVar<string>("SiteMossDepth");

            List<string> OutputList = new List<string>();
            OutputList.Add(WoodRootBiomass.Name);
            OutputList.Add(WoodFoliageBiomass.Name);
            OutputList.Add(woodBiomass.Name);
            OutputList.Add(woodySenescence.Name);
            OutputList.Add(foliageSenescence.Name);
            OutputList.Add(aet.Name);
            OutputList.Add(aetAvg.Name);
            OutputList.Add(LeafAreaIndex.Name);
            OutputList.Add(Establishment.Name);
            OutputList.Add(EstablishmentProbability.Name);
            OutputList.Add(MonthlyNetPsn.Name);
            OutputList.Add(MonthlyFolResp.Name);
            OutputList.Add(MonthlyGrossPsn.Name);
            OutputList.Add(MonthlyMaintResp.Name);
            OutputList.Add(Albedo.Name);
            OutputList.Add(MonthlyActiveLayerDepth.Name);
            OutputList.Add(MonthlyFrostDepth.Name);
            OutputList.Add(MonthlyAvgSnowPack.Name);
            OutputList.Add(MonthlyAvgWater.Name);
            OutputList.Add(MonthlyAvgLAI.Name);
            OutputList.Add(MonthlyEvap.Name);
            OutputList.Add(MonthlyInterception.Name);
            OutputList.Add(MonthlyActualTrans.Name);
            OutputList.Add(MonthlyLeakage.Name);
            OutputList.Add(MonthlyRunoff.Name);
            OutputList.Add(MonthlyAET.Name);
            OutputList.Add(MonthlyPotentialEvap.Name);
            OutputList.Add(MonthlyPotentialTrans.Name);

            OutputList.Add(Water.Name);
            OutputList.Add(SubCanopyPAR.Name);
            OutputList.Add(RootBiomass.Name);
            OutputList.Add(FoliageBiomass.Name);
            OutputList.Add(CohortsPerSpecies.Name);
            OutputList.Add(AnnualPsn.Name);
            OutputList.Add(WoodyDebris.Name);
            OutputList.Add(Litter.Name);
            OutputList.Add(AgeDistribution.Name);
            OutputList.Add(CohortBalance.Name);
            OutputList.Add(EstablishmentTable.Name);
            OutputList.Add(MortalityTable.Name);
            OutputList.Add(NSC.Name);
            OutputList.Add(PET.Name);
            OutputList.Add(SiteMossDepth.Name);

            int lineNumber = LineNumber;
            ReadVar(speciesName);
             
            if (System.String.Compare(speciesName.Value.Actual, "all", System.StringComparison.OrdinalIgnoreCase) == 0) 
            {
                parameters.SelectedSpecies = PlugIn.ModelCore.Species;
            }
            else if (System.String.Compare(speciesName.Value.Actual, "none", System.StringComparison.OrdinalIgnoreCase) == 0) 
            {
                parameters.SelectedSpecies = new List<ISpecies>();
            }
            else {
                
                ISpecies species = GetSpecies(speciesName.Value);
                List<ISpecies> selectedSpecies = new List<ISpecies>();
                selectedSpecies.Add(species);
                parameters.SelectedSpecies = selectedSpecies;

                Dictionary<string, int> lineNumbers = new Dictionary<string, int>();
                lineNumbers[species.Name] = lineNumber;

                while (!AtEndOfInput && !(OutputList.Contains(CurrentName)))
                {
                    StringReader currentLine = new StringReader(CurrentLine);

                    ReadValue(speciesName, currentLine);
                    species = GetSpecies(speciesName.Value);
                    if (lineNumbers.TryGetValue(species.Name, out lineNumber))
                        throw new InputValueException(speciesName.Value.String,
                                                      "The species {0} was previously used on line {1}",
                                                      speciesName.Value.String, lineNumber);
                    lineNumbers[species.Name] = LineNumber;

                    selectedSpecies.Add(species);
                    parameters.SelectedSpecies = selectedSpecies;
                    CheckNoDataAfter("the species name", currentLine);
                    GetNextLine();
                }
            }

            while (!AtEndOfInput)
            {

                if (ReadOptionalVar(WoodRootBiomass))
                {
                    parameters.SpeciesWoodRootBiom = WoodRootBiomass.Value;
                    continue;
                }
                if (ReadOptionalVar(WoodFoliageBiomass))
                {
                    parameters.SpeciesWoodFoliageBiom = WoodFoliageBiomass.Value;
                    continue;
                }
                if (ReadOptionalVar(woodBiomass))
                {
                    parameters.SpeciesWoodBiom = woodBiomass.Value;
                    continue;
                }
                if (ReadOptionalVar(woodySenescence))
                {
                    parameters.SpeciesWoodySenescence = woodySenescence.Value;
                    continue;
                }
                if (ReadOptionalVar(foliageSenescence))
                {
                    parameters.SpeciesFoliageSenescence = foliageSenescence.Value;
                    continue;
                }
                if (ReadOptionalVar(aet))
                {
                    parameters.AET = aet.Value;
                    continue;
                }
                if (ReadOptionalVar(aetAvg))
                {
                    parameters.AETAvg = aetAvg.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyNetPsn))
                {
                    parameters.MonthlyNetPsn = MonthlyNetPsn.Value;
                    continue;
                }
                
                if (ReadOptionalVar(MonthlyFolResp))
                {
                    parameters.MonthlyFolResp = MonthlyFolResp.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyGrossPsn))
                {
                    parameters.MonthlyGrossPsn = MonthlyGrossPsn.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyMaintResp))
                {
                    parameters.MonthlyMaintResp = MonthlyMaintResp.Value;
                    continue;
                }
                if (ReadOptionalVar(Albedo))
                {
                    parameters.Albedo = Albedo.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyActiveLayerDepth))
                {
                    parameters.MonthlyActiveLayerDepth = MonthlyActiveLayerDepth.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyFrostDepth))
                {
                    parameters.MonthlyFrostDepth = MonthlyFrostDepth.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyAvgSnowPack))
                {
                    parameters.MonthlyAvgSnowPack = MonthlyAvgSnowPack.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyAvgWater))
                {
                    parameters.MonthlyAvgWater = MonthlyAvgWater.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyAvgLAI))
                {
                    parameters.MonthlyAvgLAI = MonthlyAvgLAI.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyEvap))
                {
                    parameters.MonthlyEvap = MonthlyEvap.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyInterception))
                {
                    parameters.MonthlyInterception = MonthlyInterception.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyActualTrans))
                {
                    parameters.MonthlyActualTrans = MonthlyActualTrans.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyLeakage))
                {
                    parameters.MonthlyLeakage = MonthlyLeakage.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyRunoff))
                {
                    parameters.MonthlyRunoff = MonthlyRunoff.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyAET))
                {
                    parameters.MonthlyAET = MonthlyAET.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyPotentialEvap))
                {
                    parameters.MonthlyPotentialEvap = MonthlyPotentialEvap.Value;
                    continue;
                }
                if (ReadOptionalVar(MonthlyPotentialTrans))
                {
                    parameters.MonthlyPotentialTrans = MonthlyPotentialTrans.Value;
                    continue;
                }
                if (ReadOptionalVar(LeafAreaIndex))
                {
                    parameters.LeafAreaIndex = LeafAreaIndex.Value;
                    continue;
                }
                if (ReadOptionalVar(Establishment))
                {
                    parameters.SpeciesEst = Establishment.Value;
                    continue;
                }
                if (ReadOptionalVar(EstablishmentProbability))
                {
                    parameters.EstablishmentProbability = EstablishmentProbability.Value;
                    continue;
                }
                if (ReadOptionalVar(Water))
                {
                    parameters.Water = Water.Value;
                    continue;
                }
               
                if (ReadOptionalVar(SubCanopyPAR))
                {
                    parameters.SubCanopyPAR = SubCanopyPAR.Value;
                    continue;
                }
                if (ReadOptionalVar(AnnualPsn))
                {
                    parameters.AnnualPsn = AnnualPsn.Value;
                    continue;
                }
                if (ReadOptionalVar(CohortsPerSpecies))
                {
                    parameters.CohortsPerSpecies = CohortsPerSpecies.Value;
                    continue;
                }
                if (ReadOptionalVar(RootBiomass))
                {
                    parameters.RootBiom = RootBiomass.Value;
                    continue;
                }
                if (ReadOptionalVar(FoliageBiomass))
                {
                    parameters.FoliageBiom = FoliageBiomass.Value;
                    continue;
                }
                if (ReadOptionalVar(WoodyDebris))
                {
                    parameters.WoodyDebris  = WoodyDebris.Value;
                    continue;
                }
                if (ReadOptionalVar(Litter))
                {
                    parameters.Litter = Litter.Value;
                    continue;
                }
                if (ReadOptionalVar(AgeDistribution))
                {
                    parameters.AgeDistribution = AgeDistribution.Value;
                    continue;
                }
                if (ReadOptionalVar(CohortBalance))
                {
                    parameters.CohortBalance = CohortBalance.Value;
                    continue;
                }
                if (ReadOptionalVar(EstablishmentTable))
                {
                    parameters.EstablishmentTable = EstablishmentTable.Value;
                    continue;
                }
                if (ReadOptionalVar(MortalityTable))
                {
                    parameters.MortalityTable = MortalityTable.Value;
                    continue;
                }
                if (ReadOptionalVar(NSC))
                {
                    parameters.NSC = NSC.Value;
                    continue;
                }
                if (ReadOptionalVar(PET))
                {
                    parameters.PET = PET.Value;
                    continue;
                }

                if (ReadOptionalVar(SiteMossDepth))
                {
                    parameters.SiteMossDepth = SiteMossDepth.Value;
                    continue;
                }

                throw new System.Exception("Error in Output PnET cannot assign variable " + new StringReader(CurrentLine).ReadToEnd());

            }
            return parameters;  
        }

        //---------------------------------------------------------------------


        protected ISpecies GetSpecies(InputValue<string> name)
        {
            ISpecies species = PlugIn.ModelCore.Species[name.Actual];
            if (species == null)
                throw new InputValueException(name.String,
                                              "{0} is not a species name",
                                              name.String);
            return species;
        }
    }
}
