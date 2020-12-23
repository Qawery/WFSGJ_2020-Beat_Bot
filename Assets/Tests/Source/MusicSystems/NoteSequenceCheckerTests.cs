using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using WFS;
using Zenject;

namespace Tests
{
    class MockBeatProvider : IBeatProvider
    {
        public float Tempo { get; }
        public int SignatureUpper { get; }
        public int SignatureLower { get; }
        public float BeatDuration { get; }
        public event Action<int> OnBeat;

        public float GetBeatTimestamp(int beatsForward)
        {
            return Time.time + beatsForward;
        }

        public float GetRelativeTimestampOfNote(int subdivision, int start)
        {
            return (start - 1) / ((float) subdivision);
        }

        public void Beat()
        {
            OnBeat?.Invoke(0);
        }
    }

    class MockNoteSequence : INoteSequence
    {
        public int Subdivision => 4;

        public IReadOnlyList<Note> Notes => new List<Note>()
        {
            new Note(1, 1),
            new Note(2, 2),
            new Note(3, 3),
            new Note(4, 4),
        };

        public string Name { get; }
    }
    
    public class NoteSequenceCheckerTests
    {
        float TOLERANCE = 0.01f;
        private DiContainer container;
        private MockBeatProvider mockBeatProvider = new MockBeatProvider();
        
        [SetUp]
        public void SetUp()
        {
            container = new DiContainer();
            container.Bind<IBeatProvider>().FromInstance(mockBeatProvider);
        }

        //TODO: divide into separate test-cases
        [UnityTest]
        public IEnumerator BulkTests()
        {
            NoteSequenceChecker noteSequenceChecker = container.InstantiateComponent<WFS.NoteSequenceChecker>(new GameObject());
            bool isSequenceSet = false;
            noteSequenceChecker.OnNoteSequenceSet += noteTimestamps =>
            {
                isSequenceSet = true;
                Assert.IsTrue(noteTimestamps.Count == 4);
                for (var noteIndex = 0; noteIndex < noteTimestamps.Count; noteIndex++)
                {
                    var (timestamp, scaleDegree) = noteTimestamps[noteIndex];
                    Assert.IsTrue(Mathf.Abs(timestamp - 0.25f * noteIndex) < TOLERANCE);
                    Assert.IsTrue(scaleDegree == noteIndex + 1);
                }
            };

            bool noteHit = false;
            noteSequenceChecker.OnNoteHit += noteIndex => { noteHit = true; };

            NoteSequenceResult sequenceResult = NoteSequenceResult.Succeeded;
            noteSequenceChecker.OnNoteSequenceFinished += result =>
            {
                sequenceResult = result;
            };

            noteSequenceChecker.NoteSequence = new MockNoteSequence();
            mockBeatProvider.Beat();
            noteSequenceChecker.InitiateSequence(0.0f);
            Assert.IsTrue(isSequenceSet);
            
            yield return new WaitForSeconds(0.5f * noteSequenceChecker.NoteTolerance);
            noteSequenceChecker.RegisterNotePlayed(1);
            Assert.IsTrue(noteHit);
            noteSequenceChecker.RegisterNotePlayed(2);          
            Assert.IsTrue(sequenceResult == NoteSequenceResult.Failed);
        }
    }
}
