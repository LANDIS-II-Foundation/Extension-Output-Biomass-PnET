//  Authors:  Brian Miranda, USFS

using Landis.Core;
using Landis.Utilities;
using System.Collections.Generic;

namespace Landis.Extension.Output.PnET
{
    /// <summary>
    /// The input parameters for the plug-in.
    /// </summary>
    public class InputParameters
      
    {
        public int Timestep;

        public string CohortsPerSpecies;

        public string AnnualPsn;

        public string CohortBalance;

        public string EstablishmentTable;

        public string MortalityTable;

        public IEnumerable<ISpecies> SelectedSpecies;

        public string WoodyDebris;

        public string AgeDistribution;
 
        public string Litter;
         
        public string SubCanopyPAR;
         
        public string EstablishmentProbability;

        public string SpeciesEst;

        public string MonthlyNetPsn;
        public string MonthlyFolResp;
        public string MonthlyGrossPsn;
        public string MonthlyMaintResp; 
        public string Albedo;
        public string MonthlyActiveLayerDepth;
        public string MonthlyFrostDepth;
        public string MonthlyAvgSnowPack;
        public string MonthlyAvgWater;
        public string MonthlyAvgLAI;

        public string LeafAreaIndex;

        public string Water;

        public string SpeciesWoodRootBiom;
        public string SpeciesWoodBiom;
        public string SpeciesWoodFoliageBiom;
        public string FoliageBiom;
        public string RootBiom;

        public string SpeciesWoodySenescence;
        public string SpeciesFoliageSenescence;
        public string AET;
        public string AETAvg;

        public string NSC;
        public string PET;
    }
}
