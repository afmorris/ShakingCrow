// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bone.cs" company="Veritix">
//   Copyright (c) Veritix. All rights reserved.
// </copyright>
// <author>
//   Tony.Morris
// </author>
// <modified>
//   2015-05-24 4:29 PM
// </modified>
// <created>
//   2015-05-24 1:45 PM
// </created>
// <summary>
//   The timer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShakingCrow.Models
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    /// <summary>
    /// The timer.
    /// </summary>
    public class Bone
    {
        #region Static Fields

        private static readonly Lazy<Bone> Instance = new Lazy<Bone>(() => new Bone(GlobalHost.ConnectionManager.GetHubContext<TimerHub>().Clients));

        #endregion

        #region Fields

        private readonly IHubConnectionContext<dynamic> clients;
        private readonly Stopwatch stopwatch;
        private readonly Timer updateTimer;
        private readonly TimeSpan updateInterval = TimeSpan.FromMilliseconds(200);
        private readonly object updateTimerLock = new object();
        private double time;
        private volatile TimerState timerState;
        private volatile bool updatingTimer;

        #endregion

        #region Constructors and Destructors

        private Bone(IHubConnectionContext<dynamic> clients)
        {
            this.clients = clients;
            this.updateTimer = new Timer(this.UpdateTimer, null, this.updateInterval, this.updateInterval);
            this.stopwatch = new Stopwatch();
            this.TimerState = TimerState.Stopped;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static Bone Singleton
        {
            get
            {
                return Instance.Value;
            }
        }

        /// <summary>
        /// Gets the timer state.
        /// </summary>
        public TimerState TimerState
        {
            get
            {
                return this.timerState;
            }

            private set
            {
                this.timerState = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Gets the time
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        public double GetTime()
        {
            return this.time;
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void StartTimer()
        {
            lock (this.updateTimerLock)
            {
                if (this.TimerState != TimerState.Started)
                {
                    this.stopwatch.Start();
                    this.TimerState = TimerState.Started;
                    this.BroadcastTimerStateChange(TimerState.Started);
                }
            }
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void StopTimer()
        {
            lock (this.updateTimerLock)
            {
                if (this.TimerState != TimerState.Stopped)
                {
                    this.stopwatch.Stop();
                    this.TimerState = TimerState.Stopped;
                    this.BroadcastTimerStateChange(TimerState.Stopped);
                }
            }
        }

        #endregion

        #region Methods

        private void BroadcastTimerStateChange(TimerState state)
        {
            switch (state)
            {
                case TimerState.Started:
                    this.clients.All.timerStarted();
                    break;
                case TimerState.Stopped:
                    this.clients.All.timerStopped();
                    break;
            }
        }

        private void UpdateTimer(object state)
        {
            lock (this.updateTimerLock)
            {
                if (!this.updatingTimer)
                {
                    this.updatingTimer = true;

                    this.time = this.stopwatch.ElapsedMilliseconds;
                    this.clients.All.updateTimer(this.time);

                    this.updatingTimer = false;
                }
            }
        }

        #endregion
    }
}