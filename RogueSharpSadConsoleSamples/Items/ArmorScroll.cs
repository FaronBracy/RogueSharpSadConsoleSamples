using RogueSharpSadConsoleSamples.Core;
using RogueSharpSadConsoleSamples.Equipment;

namespace RogueSharpSadConsoleSamples.Items
{
   public class ArmorScroll : Item
   {
      public ArmorScroll()
      {
         Name = "Armor Scroll";
         RemainingUses = 1;
      }

      protected override bool UseItem()
      {
         Player player = RogueGame.Player;

         if ( player.Body == BodyEquipment.None() )
         {
            RogueGame.MessageLog.Add( $"{player.Name} is not wearing any body armor to enhance" );
         }
         else if ( player.Defense >= 8 )
         {
            RogueGame.MessageLog.Add( $"{player.Name} cannot enhance their {player.Body.Name} any more" );
         }
         else
         {
            RogueGame.MessageLog.Add( $"{player.Name} uses a {Name} to enhance their {player.Body.Name}" );
            player.Body.Defense += 1;
            RemainingUses--;
         }

         return true;
      }
   }
}

