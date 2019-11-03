// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Replays;
using osu.Game.Rulesets.LearningVoltex.Objects;
using osu.Game.Rulesets.LearningVoltex.UI;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.LearningVoltex.Replays
{
    public class LearningVoltexAutoGenerator : AutoGenerator
    {
        protected Replay Replay;
        protected List<ReplayFrame> Frames => Replay.Frames;

        public new Beatmap<LearningVoltexHitObject> Beatmap => (Beatmap<LearningVoltexHitObject>)base.Beatmap;

        public LearningVoltexAutoGenerator(IBeatmap beatmap)
            : base(beatmap)
        {
            Replay = new Replay();
        }

        public override Replay Generate()
        {
            int currentLane = 0;

            Frames.Add(new LearningVoltexReplayFrame());

            foreach (LearningVoltexHitObject hitObject in Beatmap.HitObjects)
            {
                if (currentLane == hitObject.Lane)
                    continue;

                int totalTravel = Math.Abs(hitObject.Lane - currentLane);
                var direction = hitObject.Lane > currentLane ? LearningVoltexAction.MoveDown : LearningVoltexAction.MoveUp;

                double time = hitObject.StartTime - 5;

                if (totalTravel == LearningVoltexPlayfield.LANE_COUNT)
                    addFrame(time, direction == LearningVoltexAction.MoveDown ? LearningVoltexAction.MoveUp : LearningVoltexAction.MoveDown);
                else
                {
                    time -= totalTravel * KEY_UP_DELAY;

                    for (int i = 0; i < totalTravel; i++)
                    {
                        addFrame(time, direction);
                        time += KEY_UP_DELAY;
                    }
                }

                currentLane = hitObject.Lane;
            }

            return Replay;
        }

        private void addFrame(double time, LearningVoltexAction direction)
        {
            Frames.Add(new LearningVoltexReplayFrame(direction) { Time = time });
            Frames.Add(new LearningVoltexReplayFrame { Time = time + KEY_UP_DELAY }); //Release the keys as well
        }
    }
}
