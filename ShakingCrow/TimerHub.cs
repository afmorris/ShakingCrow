// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimerHub.cs" company="Veritix">
//   Copyright (c) Veritix. All rights reserved.
// </copyright>
// <author>
//   Tony.Morris
// </author>
// <modified>
//   2015-05-24 2:33 PM
// </modified>
// <created>
//   2015-05-24 1:44 PM
// </created>
// <summary>
//   The timer hub.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShakingCrow
{
    using Microsoft.AspNet.SignalR;

    using ShakingCrow.Models;

    /// <summary>
    /// The timer hub.
    /// </summary>
    public class TimerHub : Hub
    {
        #region Fields

        private readonly Bone bone;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerHub"/> class.
        /// </summary>
        public TimerHub()
            : this(Bone.Singleton)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerHub"/> class.
        /// </summary>
        /// <param name="bone">
        /// The timer.
        /// </param>
        public TimerHub(Bone bone)
        {
            this.bone = bone;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the time.
        /// </summary>
        /// <returns>
        /// The time.
        /// </returns>
        public double GetTime()
        {
            return this.bone.GetTime();
        }

        /// <summary>
        /// Gets the timer state.
        /// </summary>
        /// <returns>
        /// The string representation of the timer state.
        /// </returns>
        public string GetTimerState()
        {
            return this.bone.TimerState.ToString();
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void StartTimer()
        {
            this.bone.StartTimer();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void StopTimer()
        {
            this.bone.StopTimer();
        }

        #endregion
    }
}