using Microsoft.Xna.Framework;
using RogueSharp;
using RogueSharpSadConsoleSamples.Interfaces;
using SadConsole.Consoles;

namespace RogueSharpSadConsoleSamples.Core
{
   public class Item : IItem, ITreasure, Interfaces.IDrawable
   {
      public Item()
      {
         Symbol = '!';
         Color = Color.Yellow;
      }

      public string Name { get; protected set; }
      public int RemainingUses { get; protected set; }

      public bool Use()
      {
         return UseItem();
      }

      protected virtual bool UseItem()
      {
         return false;
      }

      public bool PickUp( IActor actor )
      {
         Player player = actor as Player;

         if ( player != null )
         {
            if ( player.AddItem( this ) )
            {
               RogueGame.MessageLog.Add( $"{actor.Name} picked up {Name}" );
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
            console.CellData.SetCharacter( X, Y, Symbol, Color, Colors.FloorBackgroundFov );
         }
         else
         {
            console.CellData.SetCharacter( X, Y, Symbol, Color.Multiply( Color.Gray, 0.5f ), Colors.FloorBackground );
         }
      }
   }
}
