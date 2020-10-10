using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace BetterTime
{
    public class BetterTimeHotkeyCategory : GameKeyContext
    {
        public BetterTimeHotkeyCategory() : base(nameof(BetterTimeHotkeyCategory), 1)
        {
            this.RegisterGameKeys();
        }

        private void RegisterGameKeys()
        {
            this.RegisterGameKey(new GameKey(0, "MapTimeFastForward", nameof(BetterTimeHotkeyCategory), InputKey.D3, GameKeyMainCategories.CampaignMapCategory));
        }
    }
}
