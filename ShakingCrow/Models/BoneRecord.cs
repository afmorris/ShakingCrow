// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BoneRecord.cs" company="Veritix">
//   Copyright (c) Veritix. All rights reserved.
// </copyright>
// <author>
//   Tony.Morris
// </author>
// <modified>
//   2015-05-24 4:36 PM
// </modified>
// <created>
//   2015-05-24 4:36 PM
// </created>
// <summary>
//   The bone record.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShakingCrow.Models
{
    using System;

    /// <summary>
    /// The bone record.
    /// </summary>
    public class BoneRecord
    {
        #region Public Properties

        /// <summary>
        /// Gets the duration.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                return this.EndDate - this.StartDate;
            }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        #endregion
    }
}