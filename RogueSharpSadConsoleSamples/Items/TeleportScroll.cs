using RogueSharp;
using RogueSharpSadConsoleSamples.Core;

namespace RogueSharpSadConsoleSamples.Items
{
   public class TeleportScroll : Item
   {
      public TeleportScroll()
      {
         Name = "Teleport Scroll";
         RemainingUses = 1;
      }

      protected override bool UseItem()
      {
         DungeonMap map = RogueGame.DungeonMap;
         Player player = RogueGame.Player;

         RogueGame.MessageLog.Add( $"{player.Name} uses a {Name} and reappears in another place" );

         Point point = map.GetRandomLocation();
         
         map.SetActorPosition( player, point.X, point.Y );
         
         RemainingUses--;

         return true;
      }
   }
}

