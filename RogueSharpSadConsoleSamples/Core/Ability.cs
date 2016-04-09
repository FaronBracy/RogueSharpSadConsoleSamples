using Microsoft.Xna.Framework;
using RogueSharp;
using RogueSharpSadConsoleSamples.Interfaces;
using SadConsole.Consoles;

namespace RogueSharpSadConsoleSamples.Core
{
   public class Ability : IAbility, ITreasure, Interfaces.IDrawable
   {
      public Ability()
      {
         Symbol = '*';
         Color = Color.Yellow;
      }

      public string Name { get; protected set; }

      public int TurnsToRefresh { get; protected set; }

      public int TurnsUntilRefreshed { get; protected set; }

      public bool Perform()
      {
         if ( TurnsUntilRefreshed > 0 )
         {
            return false;
         }

         TurnsUntilRefreshed = TurnsToRefresh;

         return PerformAbility();
      }

      protected virtual bool PerformAbility()
      {
         return false;
      }


      public void Tick()
      {
         if ( TurnsUntilRefreshed > 0 )
         {
            TurnsUntilRefreshed--;
         }
      }

      public bool PickUp( IActor actor )
      {
         Player player = actor as Player;

         if ( player != null )
         {
            if ( player.AddAbility( this ) )
            {
               RogueGame.MessageLog.Add( $"{actor.Name} learned the {Name} ability" );
               return true;
            }
         }

         return false;
      }

      public Color Color { get; set; }
      public char Symbol { get; set; }
      public int X { get; set; }
      public int Y { get; set; }
      public void Draw( Console console, IMap map )
      {
         if ( !map.IsExplored( X, Y ) )
         {
            return;
         }

         if ( map.IsInFov( X, Y ) )
         {
            console.CellData.SetCharacter( X, Y, Symbol, Color, Colors.FloorBackgroundFov);
         }
         else
         {
            console.CellData.SetCharacter( X, Y, Symbol, Color.Multiply( Color.Gray, 0.5f ), Colors.FloorBackground );
         }
      }
   }
}
