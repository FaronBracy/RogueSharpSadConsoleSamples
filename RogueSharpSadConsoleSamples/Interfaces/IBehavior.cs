using RogueSharpSadConsoleSamples.Core;
using RogueSharpSadConsoleSamples.Systems;

namespace RogueSharpSadConsoleSamples.Interfaces
{
   public interface IBehavior
   {
      bool Act( Monster monster, CommandSystem commandSystem );
   }
}
