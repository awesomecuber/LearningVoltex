// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.LearningVoltex.Objects;
using osu.Game.Rulesets.LearningVoltex.Objects.Drawables;
using osu.Game.Rulesets.LearningVoltex.Replays;
using osu.Game.Rulesets.LearningVoltex.Scoring;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.UI.Scrolling;

namespace osu.Game.Rulesets.LearningVoltex.UI
{
    [Cached]
    public class DrawableLearningVoltexRuleset : DrawableScrollingRuleset<LearningVoltexHitObject>
    {
        public DrawableLearningVoltexRuleset(LearningVoltexRuleset ruleset, IWorkingBeatmap beatmap, IReadOnlyList<Mod> mods)
            : base(ruleset, beatmap, mods)
        {
            Direction.Value = ScrollingDirection.Left;
            TimeRange.Value = 6000;
        }

        public override ScoreProcessor CreateScoreProcessor() => new LearningVoltexScoreProcessor(this);

        protected override Playfield CreatePlayfield() => new LearningVoltexPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new LearningVoltexFramedReplayInputHandler(replay);

        public override DrawableHitObject<LearningVoltexHitObject> CreateDrawableRepresentation(LearningVoltexHitObject h) => new DrawableLearningVoltexHitObject(h);

        protected override PassThroughInputManager CreateInputManager() => new LearningVoltexInputManager(Ruleset?.RulesetInfo);
    }
}
