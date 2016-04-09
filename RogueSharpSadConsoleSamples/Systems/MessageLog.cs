using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using SadConsole.Consoles;

namespace RogueSharpSadConsoleSamples.Systems
{
   public class MessageLog
   {
      private readonly Queue<string> _lines;

      public MessageLog()
      {
         _lines = new Queue<string>();
      }

      public void Add( string message )
      {
         _lines.Enqueue( message );
         if ( _lines.Count > 9 )
         {
            _lines.Dequeue();
         }
      }

      public void Draw( Console console )
      {
         //console.Clear();
         string[] lines = _lines.ToArray();
         for ( int i = 0; i < lines.Count(); i++ )
         {
            console.CellData.Print( 1, i + 1, lines[i], Color.White );
         }
      }
   }
}