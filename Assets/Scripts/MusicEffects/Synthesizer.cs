using System.Collections.Generic;
using UnityEngine;

namespace WFS
{
    public class Synthesizer : MonoBehaviour
    {
        [SerializeField] private float sineWaveAmplitude = 0.5f;
        [SerializeField] private float attackTime = 0.1f;
        [SerializeField] private float attackSigmoidSlope = 10.0f;
        [SerializeField] private float releaseSigmoidSlope = 5.0f;
        [SerializeField] private int baseNote = 52;
        
        private float frequency = 500.0f;
        private int midiNoteNumber = 60;
        private float phase = 0.0f;
        private float secondsPerSample = 0.0f;
        private float sustainTimer = float.MaxValue;
        private float volume = 0.0f;
        private float releaseThreshold;

        private Dictionary<int, int> scaleDegreeToMidiNoteMapping = new Dictionary<int, int>()
        {
            [1] = 0,
            [2] = 2,
            [3] = 3,
            [4] = 5,
            [5] = 7,
            [6] = 8,
            [7] = 10,
            [8] = 12
        };
        
        public void PlayScaleDegree(int scaleDegree)
        {
            PlayMidiNote(scaleDegreeToMidiNoteMapping[scaleDegree]);
        }
        
        private void PlayMidiNote(int noteNumber)
        {
            midiNoteNumber = baseNote + noteNumber;
            frequency = 440 * Mathf.Pow(2, (midiNoteNumber - 69) / 12.0f);
            sustainTimer = 0;
            phase = 0;
            volume = 1;
            releaseThreshold = attackTime * (1 + 0.5f * attackSigmoidSlope / releaseSigmoidSlope);
            secondsPerSample = 1.0f / AudioSettings.outputSampleRate;
        }
        
        private float Sigmoid(float arg, float slope, float threshold)
        {
            return 1 / (1 + Mathf.Exp(-slope * (arg - threshold)));
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            int sampleCount = data.Length / channels;
            for (int sampleIndex = 0; sampleIndex < sampleCount; ++sampleIndex)
            {
                float valueInSample = sineWaveAmplitude * volume * Mathf.Sin(phase);
                for (int channelIndex = 0; channelIndex < channels;  ++channelIndex)
                {
                    data[sampleIndex * channels + channelIndex] = valueInSample;
                }
                
                phase += 2 * Mathf.PI * frequency * secondsPerSample;
                sustainTimer += secondsPerSample;
                if (sustainTimer < attackTime)
                {
                    volume = Sigmoid(sustainTimer, attackSigmoidSlope, 0.5f * attackTime);
                }
                else
                {
                    volume = Sigmoid(sustainTimer, -releaseSigmoidSlope, releaseThreshold);
                }
                
                if (phase >= 2 * Mathf.PI)
                {
                    phase -= 2 * Mathf.PI;
                }
            }
        }
    }
}