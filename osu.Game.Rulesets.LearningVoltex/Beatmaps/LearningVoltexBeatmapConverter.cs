// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.LearningVoltex.Objects;
using osu.Game.Rulesets.LearningVoltex.UI;
using osuTK;

namespace osu.Game.Rulesets.LearningVoltex.Beatmaps
{
    public class LearningVoltexBeatmapConverter : BeatmapConverter<LearningVoltexHitObject>
    {
        private readonly float minPosition;
        private readonly float maxPosition;

        protected override IEnumerable<Type> ValidConversionTypes { get; } = new[] { typeof(IHasXPosition), typeof(IHasYPosition) };

        public LearningVoltexBeatmapConverter(IBeatmap beatmap)
            : base(beatmap)
        {
            minPosition = beatmap.HitObjects.Min(getUsablePosition);
            maxPosition = beatmap.HitObjects.Max(getUsablePosition);
        }

        protected override IEnumerable<LearningVoltexHitObject> ConvertHitObject(HitObject original, IBeatmap beatmap)
        {
            yield return new LearningVoltexHitObject
            {
                Samples = original.Samples,
                StartTime = original.StartTime,
                Lane = getLane(original)
            };
        }

        private int getLane(HitObject hitObject) => (int)MathHelper.Clamp(
            (getUsablePosition(hitObject) - minPosition) / (maxPosition - minPosition) * LearningVoltexPlayfield.LANE_COUNT, 0, LearningVoltexPlayfield.LANE_COUNT);

        private float getUsablePosition(HitObject h) => (h as IHasYPosition)?.Y ?? ((IHasXPosition)h).X;
    }
}
