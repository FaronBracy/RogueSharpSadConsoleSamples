#region File Description
//-----------------------------------------------------------------------------
// InputState.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RogueSharpSadConsoleSamples.Systems
{
   /// <summary>
   ///    Helper for reading input from keyboard, gamepad, and touch input. This class
   ///    tracks both the current and previous state of the input devices, and implements
   ///    query methods for high level input actions such as "move up through the menu"
   ///    or "pause the game".
   /// </summary>
   public class InputState
   {
      private TimeSpan lastUpdateTime = TimeSpan.MinValue;
      private TimeSpan currentTime;
      public const int MaxInputs = 4;

      public readonly GamePadState[] CurrentGamePadStates;
      public readonly KeyboardState[] CurrentKeyboardStates;
      public readonly bool[] GamePadWasConnected;

      public readonly GamePadState[] LastGamePadStates;
      public readonly KeyboardState[] LastKeyboardStates;

      public InputState()
      {
         CurrentKeyboardStates = new KeyboardState[MaxInputs];
         CurrentGamePadStates = new GamePadState[MaxInputs];

         LastKeyboardStates = new KeyboardState[MaxInputs];
         LastGamePadStates = new GamePadState[MaxInputs];

         CurrentMouseState = new MouseState();
         LastMouseState = new MouseState();

         GamePadWasConnected = new bool[MaxInputs];
      }

      public MouseState CurrentMouseState
      {
         get;
         private set;
      }

      public MouseState LastMouseState
      {
         get;
         private set;
      }

      /// <summary>
      ///    Reads the latest state of the keyboard and gamepad.
      /// </summary>
      public void Update( GameTime gameTime )
      {
         if ( lastUpdateTime == TimeSpan.MinValue )
         {
            lastUpdateTime = gameTime.TotalGameTime;
         }
         currentTime = gameTime.TotalGameTime;

         for ( int i = 0; i < MaxInputs; i++ )
         {
            LastKeyboardStates[i] = CurrentKeyboardStates[i];
            LastGamePadStates[i] = CurrentGamePadStates[i];

            CurrentKeyboardStates[i] = Keyboard.GetState( (PlayerIndex) i );
            CurrentGamePadStates[i] = GamePad.GetState( (PlayerIndex) i );

            // Keep track of whether a gamepad has ever been
            // connected, so we can detect if it is unplugged.
            if ( CurrentGamePadStates[i].IsConnected )
            {
               GamePadWasConnected[i] = true;
            }
         }

         LastMouseState = CurrentMouseState;
         CurrentMouseState = Mouse.GetState();
      }

      public bool IsNewLeftMouseClick( out MouseState mouseState )
      {
         mouseState = CurrentMouseState;
         return ( CurrentMouseState.LeftButton == ButtonState.Released && LastMouseState.LeftButton == ButtonState.Pressed );
      }

      public bool IsNewRightMouseClick( out MouseState mouseState )
      {
         mouseState = CurrentMouseState;
         return ( CurrentMouseState.RightButton == ButtonState.Released && LastMouseState.RightButton == ButtonState.Pressed );
      }

      public bool IsNewThirdMouseClick( out MouseState mouseState )
      {
         mouseState = CurrentMouseState;
         return ( CurrentMouseState.MiddleButton == ButtonState.Pressed && LastMouseState.MiddleButton == ButtonState.Released );
      }

      public bool IsNewMouseScrollUp( out MouseState mouseState )
      {
         mouseState = CurrentMouseState;
         return ( CurrentMouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue );
      }

      public bool IsNewMouseScrollDown( out MouseState mouseState )
      {
         mouseState = CurrentMouseState;
         return ( CurrentMouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue );
      }

      /// <summary>
      ///    Helper for checking if a key was newly pressed during this update. The
      ///    controllingPlayer parameter specifies which player to read input for.
      ///    If this is null, it will accept input from any player. When a keypress
      ///    is detected, the output playerIndex reports which player pressed it.
      /// </summary>
      public bool IsNewKeyPress( Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex )
      {
         if ( controllingPlayer.HasValue )
         {
            // Read input from the specified player.
            playerIndex = controllingPlayer.Value;

            var i = (int) playerIndex;

            return ( CurrentKeyboardStates[i].IsKeyDown( key ) && LastKeyboardStates[i].IsKeyUp( key ) );
         }
         else
         {
            // Accept input from any player.
            return ( IsNewKeyPress( key, PlayerIndex.One, out playerIndex ) || IsNewKeyPress( key, PlayerIndex.Two, out playerIndex )
                     || IsNewKeyPress( key, PlayerIndex.Three, out playerIndex ) || IsNewKeyPress( key, PlayerIndex.Four, out playerIndex ) );
         }
      }

      /// <summary>
      ///    Helper for checking if a button was newly pressed during this update.
      ///    The controllingPlayer parameter specifies which player to read input for.
      ///    If this is null, it will accept input from any player. When a button press
      ///    is detected, the output playerIndex reports which player pressed it.
      /// </summary>
      public bool IsNewButtonPress( Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex )
      {
         if ( controllingPlayer.HasValue )
         {
            // Read input from the specified player.
            playerIndex = controllingPlayer.Value;

            var i = (int) playerIndex;

            return ( CurrentGamePadStates[i].IsButtonDown( button ) && LastGamePadStates[i].IsButtonUp( button ) );
         }
         else
         {
            // Accept input from any player.
            return ( IsNewButtonPress( button, PlayerIndex.One, out playerIndex ) || IsNewButtonPress( button, PlayerIndex.Two, out playerIndex )
                     || IsNewButtonPress( button, PlayerIndex.Three, out playerIndex ) || IsNewButtonPress( button, PlayerIndex.Four, out playerIndex ) );
         }
      }

      public bool IsKeyPressed( Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex )
      {
         if ( controllingPlayer.HasValue )
         {
            // Read input from the specified player.
            playerIndex = controllingPlayer.Value;

            var i = (int) playerIndex;

            return ( CurrentKeyboardStates[i].IsKeyDown( key ) );
         }
         else
         {
            // Accept input from any player.
            return ( IsKeyPressed( key, PlayerIndex.One, out playerIndex ) || IsKeyPressed( key, PlayerIndex.Two, out playerIndex )
                     || IsKeyPressed( key, PlayerIndex.Three, out playerIndex ) || IsKeyPressed( key, PlayerIndex.Four, out playerIndex ) );
         }
      }

      public bool IsButtonPressed( Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex )
      {
         if ( controllingPlayer.HasValue )
         {
            // Read input from the specified player.
            playerIndex = controllingPlayer.Value;

            var i = (int) playerIndex;

            return ( CurrentGamePadStates[i].IsButtonDown( button ) );
         }
         else
         {
            // Accept input from any player.
            return ( IsButtonPressed( button, PlayerIndex.One, out playerIndex ) || IsButtonPressed( button, PlayerIndex.Two, out playerIndex )
                     || IsButtonPressed( button, PlayerIndex.Three, out playerIndex ) || IsButtonPressed( button, PlayerIndex.Four, out playerIndex ) );
         }
      }

      // My overload because normal IsKeyPressed is a pain to use
      public bool IsKeyPressed( Keys key )
      {
         PlayerIndex playerIndex;
         bool isPressed = ( IsKeyPressed( key, PlayerIndex.One, out playerIndex ) || IsKeyPressed( key, PlayerIndex.Two, out playerIndex )
            || IsKeyPressed( key, PlayerIndex.Three, out playerIndex ) || IsKeyPressed( key, PlayerIndex.Four, out playerIndex ) );

         if ( isPressed )
         {
            if ( ( currentTime - lastUpdateTime ).Milliseconds < 150 )
            {
               return false;
            }
            lastUpdateTime = currentTime;
         }

         return isPressed;
      }
   }
}
