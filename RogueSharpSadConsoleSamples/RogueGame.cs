using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RogueSharp.Random;
using RogueSharpSadConsoleSamples.Core;
using RogueSharpSadConsoleSamples.Items;
using RogueSharpSadConsoleSamples.Systems;
using Console = SadConsole.Consoles.Console;

namespace RogueSharpSadConsoleSamples
{
   public class RogueGame : Game
   {
      private readonly GraphicsDeviceManager _graphics;

      private static readonly int _screenWidth = 100;
      private static readonly int _screenHeight = 70;
      private static readonly int _mapWidth = 80;
      private static readonly int _mapHeight = 48;
      private static readonly int _messageWidth = 80;
      private static readonly int _messageHeight = 11;
      private static readonly int _statWidth = 20;
      private static readonly int _statHeight = 70;
      private static readonly int _inventoryWidth = 80;
      private static readonly int _inventoryHeight = 11;

      private static Console _mapConsole;
      private static Console _messageConsole;
      private static Console _statConsole;
      private static Console _inventoryConsole;

      private static int _mapLevel = 1;
      private static bool _renderRequired = true;

      private static InputState _inputState;

      public static Player Player { get; set; }
      public static DungeonMap DungeonMap { get; private set; }
      public static MessageLog MessageLog { get; private set; }
      public static CommandSystem CommandSystem { get; private set; }
      public static SchedulingSystem SchedulingSystem { get; private set; }
      public static TargetingSystem TargetingSystem { get; private set; }
      public static IRandom Random { get; private set; }

      public RogueGame()
      {
         string consoleTitle = "RougeSharp SadConsole Example Game - Level 1";
         int seed = (int) DateTime.UtcNow.Ticks;
         Random = new DotNetRandom( seed );

         MessageLog = new MessageLog();
         MessageLog.Add( "The rogue arrives on level 1" );
         MessageLog.Add( $"Level created with seed '{seed}'" );

         Player = new Player();
         SchedulingSystem = new SchedulingSystem();

         MapGenerator mapGenerator = new MapGenerator( _mapWidth, _mapHeight, 20, 13, 7, _mapLevel );
         DungeonMap = mapGenerator.CreateMap();

         CommandSystem = new CommandSystem();
         TargetingSystem = new TargetingSystem();

         Player.Item1 = new RevealMapScroll();
         Player.Item2 = new RevealMapScroll();

         _inputState = new InputState();

         _graphics = new GraphicsDeviceManager( this );
         this.Window.Title = consoleTitle;

         Content.RootDirectory = "Content";
         var sadConsoleComponent = new SadConsole.EngineGameComponent( this, () => {
            using ( var stream = System.IO.File.OpenRead( "Fonts/Cheepicus12.font" ) )
               SadConsole.Engine.DefaultFont = SadConsole.Serializer.Deserialize<SadConsole.Font>( stream );

            SadConsole.Engine.DefaultFont.ResizeGraphicsDeviceManager( _graphics, _screenWidth, _screenHeight, 0, 0 );
            SadConsole.Engine.UseMouse = true;
            SadConsole.Engine.UseKeyboard = true;

            _mapConsole = new Console( _mapWidth, _mapHeight );
            _messageConsole = new Console( _messageWidth, _messageHeight );
            _statConsole = new Console( _statWidth, _statHeight );
            _inventoryConsole = new Console( _inventoryWidth, _inventoryHeight );

            _mapConsole.Position = new Point( 0, _inventoryHeight );
            _messageConsole.Position = new Point( 0, _screenHeight - _messageHeight );
            _statConsole.Position = new Point( _mapWidth, 0 );
            _inventoryConsole.Position = new Point( 0, 0 );

            SadConsole.Engine.ConsoleRenderStack.Add( _mapConsole );
            SadConsole.Engine.ConsoleRenderStack.Add( _messageConsole );
            SadConsole.Engine.ConsoleRenderStack.Add( _statConsole );
            SadConsole.Engine.ConsoleRenderStack.Add( _inventoryConsole );

            SadConsole.Engine.ActiveConsole = _mapConsole;
         } );

         Components.Add( sadConsoleComponent );
      }

      protected override void Initialize()
      {
         IsMouseVisible = true;

         base.Initialize();
      }

      protected override void Update( GameTime gameTime )
      {
         bool didPlayerAct = false;
         _inputState.Update( gameTime );

         if ( TargetingSystem.IsPlayerTargeting )
         {
            _renderRequired = true;
            TargetingSystem.HandleInput( _inputState );
         }
         else if ( CommandSystem.IsPlayerTurn )
         {
            if ( _inputState.IsKeyPressed( Keys.Up ) )
            {
               didPlayerAct = CommandSystem.MovePlayer( Direction.Up );
            }
            else if ( _inputState.IsKeyPressed( Keys.Down ) )
            {
               didPlayerAct = CommandSystem.MovePlayer( Direction.Down );
            }
            else if ( _inputState.IsKeyPressed( Keys.Left ) )
            {
               didPlayerAct = CommandSystem.MovePlayer( Direction.Left );
            }
            else if ( _inputState.IsKeyPressed( Keys.Right ) )
            {
               didPlayerAct = CommandSystem.MovePlayer( Direction.Right );
            }
            else if ( _inputState.IsKeyPressed( Keys.Escape ) )
            {
               this.Exit();
            }
            else if ( _inputState.IsKeyPressed( Keys.OemPeriod ) )
            {
               if ( DungeonMap.CanMoveDownToNextLevel() )
               {
                  MapGenerator mapGenerator = new MapGenerator( _mapWidth, _mapHeight, 20, 13, 7, ++_mapLevel );
                  DungeonMap = mapGenerator.CreateMap();
                  MessageLog = new MessageLog();
                  CommandSystem = new CommandSystem();
                  this.Window.Title = $"RougeSharp SadConsole Example Game - Level {_mapLevel}";
                  didPlayerAct = true;
               }
            }
            else
            {
               didPlayerAct = CommandSystem.HandleInput( _inputState );
            }

            if ( didPlayerAct )
            {
               _renderRequired = true;
               CommandSystem.EndPlayerTurn();
            }
         }
         else
         {
            CommandSystem.ActivateMonsters();
            _renderRequired = true;
         }
         base.Update( gameTime );
      }

      protected override void Draw( GameTime gameTime )
      {
         if ( _renderRequired )
         {
            GraphicsDevice.Clear( Color.Black );

            _mapConsole.CellData.Clear();
            _messageConsole.CellData.Clear();
            _statConsole.CellData.Clear();
            _inventoryConsole.CellData.Clear();

            MessageLog.Draw( _messageConsole );
            DungeonMap.Draw( _mapConsole, _statConsole, _inventoryConsole );
            MessageLog.Draw( _messageConsole );
            TargetingSystem.Draw( _mapConsole );

            base.Draw( gameTime );
         }
      }
   }
}
