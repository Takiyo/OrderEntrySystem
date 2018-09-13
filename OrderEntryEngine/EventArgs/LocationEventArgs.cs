using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    /// <summary>
    /// The class representing event arguments for the Location.
    /// </summary>
    public class LocationEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the LocationEventArgs class.
        /// </summary>
        /// <param name="location">Location to be created.</param>
        public LocationEventArgs(Location location)
        {
            this.Location = location;
        }

        /// <summary>
        /// A location property.
        /// </summary>
        public Location Location { get; private set; }
    }
}
