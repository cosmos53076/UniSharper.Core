// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEngine;

namespace UniSharper.Audio
{
    /// <summary>
    /// Interface for playing audio source.
    /// </summary>
    public interface IAudioPlayer
    {
        #region Properties

        /// <summary>
        /// Get the component of <see cref="UnityEngine.AudioSource"/>.
        /// </summary>
        /// <value>The component of <see cref="UnityEngine.AudioSource"/>. </value>
        AudioSource AudioSource { get; }

        /// <summary>
        /// Un- / Mutes the <see cref="UnityEngine.AudioSource"/>.
        /// </summary>
        /// <value>The indicator that the <see cref="UnityEngine.AudioSource"/> is muting. </value>
        bool Mute { get; set; }

        /// <summary>
        /// Is the audio clip looping.
        /// </summary>
        /// <value>The indicator that the <see cref="UnityEngine.AudioSource"/> replays after it finishes or not. </value>
        bool IsLoop { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Pauses playing.
        /// </summary>
        void Pause();

        /// <summary>
        /// Plays the audio.
        /// </summary>
        /// <param name="muted">Whether mutes the audio. </param>
        /// <param name="isLoop">Whether replays after the audio finishes. </param>
        void Play(bool muted = false, bool isLoop = false);

        /// <summary>
        /// Resumes playing.
        /// </summary>
        void Resume();

        /// <summary>
        /// Stops playing audio.
        /// </summary>
        void Stop();

        #endregion Methods
    }
}