// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Game.Rulesets.Replays;

namespace osu.Game.Rulesets.LearningVoltex.Replays
{
    public class LearningVoltexReplayFrame : ReplayFrame
    {
        public List<LearningVoltexAction> Actions = new List<LearningVoltexAction>();

        public LearningVoltexReplayFrame(LearningVoltexAction? button = null)
        {
            if (button.HasValue)
                Actions.Add(button.Value);
        }
    }
}
