using Microsoft.Xna.Framework;

namespace RogueSharpSadConsoleSamples.Core
{
   public static class Colors
   {
      public static Color DoorBackground = Swatch.ComplimentDarkest;
      public static Color Door = Swatch.ComplimentLighter;
      public static Color DoorBackgroundFov = Swatch.ComplimentDarker;
      public static Color DoorFov = Swatch.ComplimentLightest;
      public static Color FloorBackground = Color.Black;
      public static Color Floor = Swatch.AlternateDarkest;
      public static Color FloorBackgroundFov = Swatch.DbDark;
      public static Color FloorFov = Swatch.Alternate;
      public static Color WallBackground = Swatch.SecondaryDarkest;
      public static Color Wall = Swatch.Secondary;
      public static Color WallBackgroundFov = Swatch.SecondaryDarker;
      public static Color WallFov = Swatch.SecondaryLighter;
      public static Color GoblinColor = Color.Green;
      public static Color KoboldColor = new Color( 255, 165, 0 );
      public static Color OozeColor = new Color( 102, 205, 170 );
      public static Color Player = Swatch.DbLight;
      public static Color InventoryHeading = Swatch.DbLight;
   }

   public static class Swatch
   {
      // http://paletton.com/#uid=73d0u0k5qgb2NnT41jT74c8bJ8X

      public static Color PrimaryLightest = new Color( 110, 121, 119 );
      public static Color PrimaryLighter = new Color( 88, 100, 98 );
      public static Color Primary = new Color( 68, 82, 79 );
      public static Color PrimaryDarker = new Color( 48, 61, 59 );
      public static Color PrimaryDarkest = new Color( 29, 45, 42 );

      public static Color SecondaryLightest = new Color( 116, 120, 126 );
      public static Color SecondaryLighter = new Color( 93, 97, 105 );
      public static Color Secondary = new Color( 72, 77, 85 );
      public static Color SecondaryDarker = new Color( 51, 56, 64 );
      public static Color SecondaryDarkest = new Color( 31, 38, 47 );

      public static Color AlternateLightest = new Color( 190, 184, 174 );
      public static Color AlternateLighter = new Color( 158, 151, 138 );
      public static Color Alternate = new Color( 129, 121, 107 );
      public static Color AlternateDarker = new Color( 97, 89, 75 );
      public static Color AlternateDarkest = new Color( 71, 62, 45 );

      public static Color ComplimentLightest = new Color( 190, 180, 174 );
      public static Color ComplimentLighter = new Color( 158, 147, 138 );
      public static Color Compliment = new Color( 129, 116, 107 );
      public static Color ComplimentDarker = new Color( 97, 84, 75 );
      public static Color ComplimentDarkest = new Color( 71, 56, 45 );

      // http://pixeljoint.com/forum/forum_posts.asp?TID=12795

      public static Color DbDark = new Color( 20, 12, 28 );
      public static Color DbOldBlood = new Color( 68, 36, 52 );
      public static Color DbDeepWater = new Color( 48, 52, 109 );
      public static Color DbOldStone = new Color( 78, 74, 78 );
      public static Color DbWood = new Color( 133, 76, 48 );
      public static Color DbVegetation = new Color( 52, 101, 36 );
      public static Color DbBlood = new Color( 208, 70, 72 );
      public static Color DbStone = new Color( 117, 113, 97 );
      public static Color DbWater = new Color( 89, 125, 206 );
      public static Color DbBrightWood = new Color( 210, 125, 44 );
      public static Color DbMetal = new Color( 133, 149, 161 );
      public static Color DbGrass = new Color( 109, 170, 44 );
      public static Color DbSkin = new Color( 210, 170, 153 );
      public static Color DbSky = new Color( 109, 194, 202 );
      public static Color DbSun = new Color( 218, 212, 94 );
      public static Color DbLight = new Color( 222, 238, 214 );
   }
}