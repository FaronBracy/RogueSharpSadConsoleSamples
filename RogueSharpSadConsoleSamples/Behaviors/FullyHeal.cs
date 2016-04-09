using RogueSharpSadConsoleSamples.Core;
using RogueSharpSadConsoleSamples.Interfaces;
using RogueSharpSadConsoleSamples.Systems;

namespace RogueSharpSadConsoleSamples.Behaviors
{
   public class FullyHeal : IBehavior
   {
      public bool Act( Monster monster, CommandSystem commandSystem )
      {
         if ( monster.Health < monster.MaxHealth )
         {
            int healthToRecover = monster.MaxHealth - monster.Health;
            monster.Health = monster.MaxHealth;
            RogueGame.MessageLog.Add( $"{monster.Name} catches his breath and recovers {healthToRecover} health" );    
            return true;
         }
         return false;
      }
   }
}
