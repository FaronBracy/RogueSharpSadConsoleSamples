using RogueSharp;
using RogueSharpSadConsoleSamples.Core;
using RogueSharpSadConsoleSamples.Interfaces;

namespace RogueSharpSadConsoleSamples.Abilities
{
   public class MagicMissile : Ability, ITargetable
   {
      private readonly int _attack;
      private readonly int _attackChance;

      public MagicMissile( int attack, int attackChance)
      {
         Name = "Magic Missile";
         TurnsToRefresh = 10;
         TurnsUntilRefreshed = 0;
         _attack = attack;
         _attackChance = attackChance;
      }

      protected override bool PerformAbility()
      {
         return RogueGame.TargetingSystem.SelectMonster( this );
      }

      public void SelectTarget( Point target )
      {
         DungeonMap map = RogueGame.DungeonMap;
         Player player = RogueGame.Player;
         Monster monster = map.GetMonsterAt( target.X, target.Y );
         if ( monster != null )
         {
            RogueGame.MessageLog.Add( $"{player.Name} casts a {Name} at {monster.Name}" );
            Actor magicMissleActor = new Actor
            {
               Attack = _attack, AttackChance = _attackChance, Name = Name
            };
            RogueGame.CommandSystem.Attack( magicMissleActor, monster );
         }
      }
   }
}
