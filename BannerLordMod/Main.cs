using System;
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
            rd.CollectResources();
            LoadFromXml(UIResourceManager.UIResourceDepot, "BetterTimeSpriteData.xml");
            var spriteData = UIResourceManager.SpriteData;

            var texture = new TaleWorlds.TwoDimension.Texture((TaleWorlds.TwoDimension.ITexture)
                new EngineTexture(
                    TaleWorlds.Engine.Texture.CreateTextureFromPath(
                        @"../../Modules/BetterTime/GUI/SpriteSheets/better_time_icons/", "better_time_icons_1.png")
                    )
                );
            var bettertimeicons = spriteData.SpriteCategories["better_time_icons"];
            bettertimeicons.SpriteSheets.Add(texture);
            bettertimeicons.Load((ITwoDimensionResourceContext)rc, rd);

            UIResourceManager.BrushFactory.Initialize();
        }

        private void LoadFromXml(ResourceDepot resourceDepot, string filename)
        {
            var sd = UIResourceManager.SpriteData;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(resourceDepot.GetFilePath(filename));
            XmlElement xmlElement = xmlDocument[nameof(SpriteData)];
            XmlNode xmlNode1 = (XmlNode)xmlElement["SpriteCategories"];
            XmlNode xmlNode2 = (XmlNode)xmlElement["SpriteParts"];
            XmlNode xmlNode3 = (XmlNode)xmlElement["Sprites"];
            foreach (XmlNode xmlNode4 in xmlNode1)
            {
                string innerText = xmlNode4["Name"].InnerText;
                int int32_1 = Convert.ToInt32(xmlNode4["SpriteSheetCount"].InnerText);
                Vec2i[] vec2iArray = new Vec2i[int32_1];
                foreach (XmlNode childNode in xmlNode4.ChildNodes)
                {
                    if (childNode.Name == "SpriteSheetSize")
                    {
                        int int32_2 = Convert.ToInt32(childNode.Attributes["ID"].InnerText);
                        int int32_3 = Convert.ToInt32(childNode.Attributes["Width"].InnerText);
                        int int32_4 = Convert.ToInt32(childNode.Attributes["Height"].InnerText);
                        vec2iArray[int32_2 - 1] = new Vec2i(int32_3, int32_4);
                    }
                }
                SpriteCategory spriteCategory = new SpriteCategory(innerText, sd, int32_1);
                spriteCategory.SheetSizes = vec2iArray;
                sd.SpriteCategories.Add(spriteCategory.Name, spriteCategory);
            }
            foreach (XmlNode xmlNode4 in xmlNode2)
            {
                string innerText = xmlNode4["Name"].InnerText;
                int int32_1 = Convert.ToInt32(xmlNode4["Width"].InnerText);
                int int32_2 = Convert.ToInt32(xmlNode4["Height"].InnerText);
                SpriteCategory spriteCategory = sd.SpriteCategories[xmlNode4["CategoryName"].InnerText];
                int width = int32_1;
                int height = int32_2;
                SpritePart spritePart = new SpritePart(innerText, spriteCategory, width, height);
                spritePart.SheetID = Convert.ToInt32(xmlNode4["SheetID"].InnerText);
                spritePart.SheetX = Convert.ToInt32(xmlNode4["SheetX"].InnerText);
                spritePart.SheetY = Convert.ToInt32(xmlNode4["SheetY"].InnerText);
                sd.SpritePartNames.Add(spritePart.Name, spritePart);
                spritePart.UpdateInitValues();
            }
            foreach (XmlNode xmlNode4 in xmlNode3)
            {
                Sprite sprite = (Sprite)null;
                if (xmlNode4.Name == "GenericSprite")
                    sprite = (Sprite)new SpriteGeneric(xmlNode4["Name"].InnerText, sd.SpritePartNames[xmlNode4["SpritePartName"].InnerText]);
                else if (xmlNode4.Name == "NineRegionSprite")
                {
                    string innerText1 = xmlNode4["Name"].InnerText;
                    string innerText2 = xmlNode4["SpritePartName"].InnerText;
                    int int32_1 = Convert.ToInt32(xmlNode4["LeftWidth"].InnerText);
                    int int32_2 = Convert.ToInt32(xmlNode4["RightWidth"].InnerText);
                    int int32_3 = Convert.ToInt32(xmlNode4["TopHeight"].InnerText);
                    int int32_4 = Convert.ToInt32(xmlNode4["BottomHeight"].InnerText);
                    SpritePart spritePartName = sd.SpritePartNames[innerText2];
                    int leftWidth = int32_1;
                    int rightWidth = int32_2;
                    int topHeight = int32_3;
                    int bottomHeight = int32_4;
                    sprite = (Sprite)new SpriteNineRegion(innerText1, spritePartName, leftWidth, rightWidth, topHeight, bottomHeight);
                }
                if (!sd.SpriteNames.ContainsKey(sprite.Name))
                    sd.SpriteNames.Add(sprite.Name, sprite);
            }
        }
    }
}
