//  Authors:  Austen Ruzicka

using Landis.SpatialModeling;

namespace Landis.Extension.Output.PnET
{
    public class FloatPixel : Pixel
    {
        public Band<float> MapCode = "The numeric code for each raster cell";

        public FloatPixel()
        {
            SetBands(MapCode);
        }
    }
}
