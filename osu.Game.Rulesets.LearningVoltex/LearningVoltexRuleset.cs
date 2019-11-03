// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Bindings;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Difficulty;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.LearningVoltex.Beatmaps;
using osu.Game.Rulesets.LearningVoltex.Mods;
using osu.Game.Rulesets.LearningVoltex.UI;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.LearningVoltex
{
    public class LearningVoltexRuleset : Ruleset
    {
        public LearningVoltexRuleset(RulesetInfo rulesetInfo = null)
            : base(rulesetInfo)
        {
        }

        public override string Description => "gather the osu!coins";

        public override DrawableRuleset CreateDrawableRulesetWith(IWorkingBeatmap beatmap, IReadOnlyList<Mod> mods) => new DrawableLearningVoltexRuleset(this, beatmap, mods);

        public override IBeatmapConverter CreateBeatmapConverter(IBeatmap beatmap) => new LearningVoltexBeatmapConverter(beatmap);

        public override DifficultyCalculator CreateDifficultyCalculator(WorkingBeatmap beatmap) => new LearningVoltexDifficultyCalculator(this, beatmap);

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.Automation:
                    return new[] { new LearningVoltexModAutoplay() };

                default:
                    return new Mod[] { null };
            }
        }

        public override string ShortName => "learningvoltex";

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => new[]
        {
            new KeyBinding(InputKey.W, LearningVoltexAction.MoveUp),
            new KeyBinding(InputKey.S, LearningVoltexAction.MoveDown),
        };

        public override Drawable CreateIcon() => new Sprite
        {
            Margin = new MarginPadding { Top = 3 },
            Texture = new TextureStore(new TextureLoaderStore(CreateResourceStore()), false).Get("Textures/coin"),
        };
    }
}
