using RogueSharp;
using RogueSharpSadConsoleSamples.Core;
using RogueSharpSadConsoleSamples.Interfaces;
using RogueSharpSadConsoleSamples.Systems;

namespace RogueSharpSadConsoleSamples.Behaviors
{
   public class RunAway : IBehavior
   {
      public bool Act( Monster monster, CommandSystem commandSystem )
      {
         DungeonMap dungeonMap = RogueGame.DungeonMap;
         Player player = RogueGame.Player;

         // Set the cells the monster and player are on to walkable so the pathfinder doesn't bail early
         dungeonMap.SetIsWalkable( monster.X, monster.Y, true );
         dungeonMap.SetIsWalkable( player.X, player.Y, true );

         GoalMap goalMap = new GoalMap( dungeonMap );
         goalMap.AddGoal( player.X, player.Y, 0 );

         Path path = null;
         try
         {
            path = goalMap.FindPathAvoidingGoals( monster.X, monster.Y );
         }
         catch ( PathNotFoundException )
         {
            RogueGame.MessageLog.Add( $"{monster.Name} cowers in fear" );
         }


         // Reset the cell the monster and player are on  back to not walkable
         dungeonMap.SetIsWalkable( monster.X, monster.Y, false );
         dungeonMap.SetIsWalkable( player.X, player.Y, false );

         if ( path != null )
         {
            try
            {
               commandSystem.MoveMonster( monster, path.StepForward() );
            }
            catch ( NoMoreStepsException )
            {
               RogueGame.MessageLog.Add( $"{monster.Name} cowers in fear" );
            }
         }

         return true;
      }
   }
}
