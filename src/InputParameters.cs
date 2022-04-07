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
         
        public string BelowgroundBiomass;
         
        public string SubCanopyPAR;
         
        public string EstablishmentProbability;

        public string SpeciesEst;

        public string MonthlyNetPsn;
        public string MonthlyFolResp;
        public string MonthlyGrossPsn;
        public string MonthlyMaintResp; 
        public string MonthlyAverageAlbedo;
        public string MonthlyActiveLayerDepth;
        public string MonthlyFrostDepth;

        public string LeafAreaIndex;

        public string Water;

        public string SpeciesBiom;
        public string SpeciesWoodBiom;
        public string SpeciesAbovegroundBiom;

        public string SpeciesWoodySenescence;
        public string SpeciesFoliageSenescence;
        public string AETAvg;   
    }
}
