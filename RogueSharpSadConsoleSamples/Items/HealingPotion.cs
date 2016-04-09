using RogueSharpSadConsoleSamples.Abilities;
using RogueSharpSadConsoleSamples.Core;

namespace RogueSharpSadConsoleSamples.Items
{
   public class HealingPotion : Item
   {
      public HealingPotion()
      {
         Name = "Healing Potion";
         RemainingUses = 1;
      }

      protected override bool UseItem()
      {
         int healAmount = 15;
         RogueGame.MessageLog.Add( $"{RogueGame.Player.Name} consumes a {Name} and recovers {healAmount} health" );  

         Heal heal = new Heal( healAmount );

         RemainingUses--;

         return heal.Perform();
      }
   }
}
