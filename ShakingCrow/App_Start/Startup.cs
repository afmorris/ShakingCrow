// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Veritix">
//   Copyright (c) Veritix. All rights reserved.
// </copyright>
// <author>
//   Tony.Morris
// </author>
// <modified>
//   2015-05-24 4:31 PM
// </modified>
// <created>
//   2015-05-22 1:08 PM
// </created>
// <summary>
//   The startup.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Owin;

using ShakingCrow;

[assembly: OwinStartup(typeof(Startup))]

namespace ShakingCrow
{
    using Owin;

    /// <summary>
    /// The startup.
    /// </summary>
    public class Startup
    {
        #region Public Methods and Operators

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">
        /// The application.
        /// </param>
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }

        #endregion
    }
}