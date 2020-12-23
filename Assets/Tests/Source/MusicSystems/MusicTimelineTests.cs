using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using WFS;
using Object = UnityEngine.Object;

namespace Tests
{
    public class MusicTimelineTests
    {
        [Test]
        public void StartStop()
        {
            var musicTimeline = new GameObject().AddComponent<MusicTimeline>();
            bool startEventCalled = false;
            bool beatCalled = false;
            bool stopEventCalled = false;
            musicTimeline.OnMusicStarted += () => startEventCalled = true;
            musicTimeline.OnMusicStopped += () => stopEventCalled = true;
            musicTimeline.OnBeat += beatNum => beatCalled = true;
            musicTimeline.StartMusic();
            musicTimeline.StopMusic();
            Object.Destroy(musicTimeline.gameObject);

            Assert.IsTrue(startEventCalled);
            Assert.IsTrue(stopEventCalled);
            Assert.IsTrue(beatCalled);
        }
        
        [UnityTest]
        public IEnumerator Beat()
        {
            var musicTimeline = new GameObject().AddComponent<MusicTimeline>();
            int beatsCalled = 0;
            musicTimeline.StartMusic();       
            Assert.IsTrue(musicTimeline.IsPlaying);
            musicTimeline.OnBeat += beatNum => beatsCalled++;
            yield return new WaitForSeconds(3.0f);
            Object.Destroy(musicTimeline.gameObject);
            Assert.IsTrue(beatsCalled > 0);
        }

        private const float TOLERANCE = 0.05f;
        [UnityTest]
        public IEnumerator NoteTimestamps()
        {
            var musicTimeline = new GameObject().AddComponent<MusicTimeline>();
            musicTimeline.Tempo = 60;
            musicTimeline.SignatureUpper = 4;
            musicTimeline.SignatureLower = 4;
            musicTimeline.StartMusic();

            float timeTillNote = musicTimeline.GetRelativeTimestampOfNote(8, 2);
            Assert.IsTrue(Mathf.Abs(timeTillNote - 0.5f) < TOLERANCE);
            
            timeTillNote = musicTimeline.GetRelativeTimestampOfNote( 8, 4);
            Assert.IsTrue(Mathf.Abs(timeTillNote - 1.5f) < TOLERANCE);

            Object.Destroy(musicTimeline.gameObject);
            yield return null;
        }
    }
}