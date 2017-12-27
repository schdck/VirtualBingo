using System;
using System.Speech.Synthesis;

namespace VirtualBingo.Helpers
{
    public static class SpeechHelper
    {
        private static SpeechSynthesizer _Synthesizer;
        private static Prompt _CurrentSpeak;

        static SpeechHelper()
        {
            _Synthesizer = new SpeechSynthesizer() { Rate = 1, Volume = 100 };

            AddSpeakCompletedHandler((s, e) =>
            {
                _CurrentSpeak = null;
            });
        }

        public static void PlaySpeech(string speech)
        {
            if (_CurrentSpeak != null)
            {
                return;
            }

            _CurrentSpeak = _Synthesizer.SpeakAsync(speech);
        }

        public static void StopSpeach()
        {
            if (_CurrentSpeak == null)
            {
                return;
            }

            _Synthesizer.SpeakAsyncCancel(_CurrentSpeak);

            _CurrentSpeak = null;
        }

        public static void AddSpeakCompletedHandler(EventHandler<SpeakCompletedEventArgs> eventHandler)
        {
            _Synthesizer.SpeakCompleted += eventHandler;
        }
    }
}