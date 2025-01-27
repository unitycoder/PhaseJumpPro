﻿using UnityEngine;
using System.Collections;

/*
 * RATING: 5 stars
 * Has unit tests
 * CODE REVIEW: 4/16/22
 */
namespace PJ
{
    /// <summary>
    /// Flashes on and off N times
    /// Used for game objects that flash (usually after they receive damage)
    /// 
    /// SEE ALSO: PulseTimer (more generalized)
    /// </summary>
    public class FlashTimer : SomeTimed
    {
        public int flashCount = 3;

        protected bool isFlashOn;
        protected int flashCountState;
        protected float flashDuration;
        protected float timer;

        public bool IsFlashOn => isFlashOn;
        public override float Progress => 0.0f;

        public float Duration
        {
            get => flashCount * flashDuration * 2.0f;
        }

        public FlashTimer(int flashCount, float flashDuration) : base(0, SomeTimed.RunType.RunOnce)
        {
            this.flashCount = flashCount;
            this.flashDuration = flashDuration;
        }

        public override void Reset()
        {
            base.Reset();

            flashCountState = 0;
            isFlashOn = false;
            timer = 0;
        }

        public override void OnUpdate(TimeSlice time)
        {
            base.OnUpdate(time);

            if (!IsRunning || IsFinished)
            {
                return;
            }

            timer += time.delta;
            while (timer >= flashDuration)
            {
                timer -= flashDuration;
                OnTimerFinish();
            }
        }

        protected virtual void OnFlashChange() {}

        protected void OnTimerFinish()
        {
            isFlashOn = !isFlashOn;
            OnFlashChange();

            // Increment on flash off so we don't get stuck with it on
            if (!isFlashOn)
            {
                flashCountState += 1;

                // Stop flashing when we've reached the limit
                if (flashCount > 0 && flashCountState >= flashCount)
                {
                    IsFinished = true;
                }
            }
        }
    }
}
