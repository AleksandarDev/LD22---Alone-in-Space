using System;

namespace Alone_in_Space {
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameAIS game = new GameAIS())
            {
                game.Run();
            }
        }
    }
#endif
}

