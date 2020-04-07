﻿using System;
using System.Xml;
using TaleWorlds.Engine.GauntletUI;
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
        }
    }
}
