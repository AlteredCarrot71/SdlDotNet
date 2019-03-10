#region LICENSE
/*
 * Copyright (C) 2005 Rob Loach (http://www.robloach.net)
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */
#endregion LICENSE

using System.Collections.ObjectModel;

using SdlDotNet.Graphics;

namespace SdlDotNet.Particles
{
    /// <summary>
    /// A collection of independant particle systems.
    /// </summary>
    public class ParticleSystemCollection : Collection<ParticleSystem>
    {
        /// <summary>
        /// Creates an empty collection of particle systems.
        /// </summary>
        public ParticleSystemCollection()
        {
        }

        ///// <summary>
        ///// Creates a collection of particle systems.
        ///// </summary>
        ///// <param name="system">The system to start off the collection.</param>
        //public ParticleSystemCollection(ParticleSystem system)
        //{
        //    Add(system);
        //}

        /// <summary>
        /// Updates all particle systems within the collection.
        /// </summary>
        public void Update()
        {
            foreach (ParticleSystem system in this)
            {
                system.Update();
            }
        }
        /// <summary>
        /// Renders all particle systems within the collection on the destination surface.
        /// </summary>
        /// <param name="destination">The surface to render the particle systems on.</param>
        public void Render(Surface destination)
        {
            foreach (ParticleSystem system in this)
            {
                system.Render(destination);
            }
        }

        //		/// <summary>
        //		/// Provide the explicit interface member for ICollection.
        //		/// </summary>
        //		/// <param name="array">Array to copy collection to</param>
        //		/// <param name="index">Index at which to insert the collection items</param>
        //		void ICollection.CopyTo(Array array, int index)
        //		{
        //			this.List.CopyTo(array, index);
        //		}
    }
}
