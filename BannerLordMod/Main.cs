using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.TwoDimension;

namespace BetterTime
{
    public partial class Main : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            var rd = UIResourceManager.UIResourceDepot;
            var rc = UIResourceManager.ResourceContext;
            var sd = UIResourceManager.SpriteData;

            var spriteData = new SpriteData("BetterTimeSpriteData");
            spriteData.Load(rd);

            var texture = new TaleWorlds.TwoDimension.Texture((TaleWorlds.TwoDimension.ITexture)
                new EngineTexture(
                    TaleWorlds.Engine.Texture.CreateTextureFromPath(
                        @"../../Modules/BetterTime/GUI/SpriteSheets/better_time_icons/", "better_time_icons_1.png")
                    )
                );

            sd.SpriteCategories.Add("better_time_icons", spriteData.SpriteCategories["better_time_icons"]);
            sd.SpritePartNames.Add("FastForward@4x", spriteData.SpritePartNames["FastForward@4x"]);
            sd.SpriteNames.Add("FastForward@4x", new SpriteGeneric("FastForward@4x", spriteData.SpritePartNames["FastForward@4x"]));
            sd.SpritePartNames.Add("FastForward@4x_selected", spriteData.SpritePartNames["FastForward@4x_selected"]);
            sd.SpriteNames.Add("FastForward@4x_selected", new SpriteGeneric("FastForward@4x", spriteData.SpritePartNames["FastForward@4x_selected"]));

            var bettertimeicons = sd.SpriteCategories["better_time_icons"];
            bettertimeicons.SpriteSheets.Add(texture);
            bettertimeicons.Load((ITwoDimensionResourceContext)rc, rd);

            UIResourceManager.BrushFactory.Initialize();
            InitializeHotKeyManager();
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            try
            {
                using (Stream stream = (Stream)new FileStream(System.IO.Path.Combine(BasePath.Name, "Modules", "BetterTime", "Settings.xml"), FileMode.Open))
                    Support.settings = (Settings)new XmlSerializer(typeof(Settings)).Deserialize(stream);
            }
            catch (Exception ex)
            {
                Support.LogMessage("Better Time: Could not read settings file, using default values!");
                Support.settings = new Settings();
            }
        }

        private float currentSpeed;
        private bool timeSpedUp;
        private CampaignTimeControlMode currentTimeMode;

        protected override void OnApplicationTick(float dt)
        {
            if (Campaign.Current != null)
            {
                var test = HotKeyManager.GetAllCategories().First().RegisteredGameKeys;
                if (Campaign.Current.CurrentMenuContext != null && (!Campaign.Current.CurrentMenuContext.GameMenu.IsWaitActive || Campaign.Current.TimeControlModeLock))
                    return;

                if (Input.IsKeyReleased(InputKey.D4))
                {
                    Campaign.Current.SpeedUpMultiplier = Support.settings.extra_fast_forward_speed;
                    Campaign.Current.SetTimeSpeed(2);
                }
                if (Input.IsKeyReleased(InputKey.D3))
                {
                    Campaign.Current.SpeedUpMultiplier = Support.settings.fast_forward_speed;
                }

                if (Input.IsKeyDown(InputKey.LeftControl) && Input.IsKeyDown(InputKey.Space))
                {
                    if (Campaign.Current.SpeedUpMultiplier != Support.settings.ctrl_space_speed)
                    {
                        currentSpeed = Campaign.Current.SpeedUpMultiplier;
                        currentTimeMode = Campaign.Current.TimeControlMode;
                        timeSpedUp = true;
                    }

                    Campaign.Current.SpeedUpMultiplier = Support.settings.ctrl_space_speed;
                    Campaign.Current.SetTimeSpeed(2);
                }
                else if (timeSpedUp)
                {
                    timeSpedUp = false;
                    Campaign.Current.SpeedUpMultiplier = currentSpeed;
                    Campaign.Current.TimeControlMode = currentTimeMode;
                }
            }
        }

        private void InitializeHotKeyManager() => HotKeyManager.Initialize("Bannerlord", Utilities.GetConfigsPath() + "BannerlordGameKeys.xml", (IEnumerable<GameKeyContext>)new List<GameKeyContext>()
        {
            (GameKeyContext) new BetterTimeHotkeyCategory()
        }, !MBDebug.TestModeEnabled);
    }
}
