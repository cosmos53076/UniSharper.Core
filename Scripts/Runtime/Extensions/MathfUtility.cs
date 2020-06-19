﻿// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace UnityEngine
{
    /// <summary>
    /// This class provides some useful <see cref="UnityEngine.Mathf"/> utilities.
    /// </summary>
    public static class MathfUtility
    {
        #region Methods

        /// <summary>
        /// Clamps the angle between -180 and 180.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <param name="min">The minimum angle.</param>
        /// <param name="max">The maximum angle.</param>
        /// <returns>The angle between -180 and 180.</returns>
        public static float ClampAngle(float angle, float min, float max)
        {
            angle = NormalizeAngle(angle);

            if (angle > 180)
            {
                angle -= 360;
            }
            else if (angle < -180)
            {
                angle += 360;
            }

            min = NormalizeAngle(min);

            if (min > 180)
            {
                min -= 360;
            }
            else if (min < -180)
            {
                min += 360;
            }

            max = NormalizeAngle(max);

            if (max > 180)
            {
                max -= 360;
            }
            else if (max < -180)
            {
                max += 360;
            }

            return Mathf.Clamp(angle, min, max);
        }

        /// <summary>
        /// Gets the angle between <see cref="Vector3"/> a and b.
        /// </summary>
        /// <param name="a">The <see cref="Vector3"/> a.</param>
        /// <param name="b">The <see cref="Vector3"/> b.</param>
        /// <returns>The angle between <see cref="Vector3"/> a and b.</returns>
        public static float GetAngle(Vector3 a, Vector3 b)
        {
            return Mathf.Acos(Vector3.Dot(a.normalized, b.normalized)) * Mathf.Rad2Deg;
        }
        
        /// <summary>
        /// Gets the angle between <see cref="Vector3"/> a and b when the z value of a and b are zero.
        /// </summary>
        /// <param name="a">The <see cref="Vector3"/> a.</param>
        /// <param name="b">The <see cref="Vector3"/> b.</param>
        /// <returns>The angle between <see cref="Vector3"/> a and b.</returns>
        public static float Get2DAngle(Vector3 a, Vector3 b)
        {
            var isRight = Vector3.Cross(a, b).z < 0;
            var angle = Vector3.Angle(a, b);

            if (isRight)
                angle = -angle;

            return angle;
        }

        /// <summary>
        /// Normalizes the angle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        /// <returns>The normalized angle value.</returns>
        public static float NormalizeAngle(float angle)
        {
            while (angle > 360)
            {
                angle -= 360;
            }

            while (angle < 0)
            {
                angle += 360;
            }

            return angle;
        }

        #endregion Methods
    }
}