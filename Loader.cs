using System;

namespace ExtremeTetris
{
    public static class Loader
    {
        [STAThread]
        static void Main()
        {   
            using (var program = new Program())
                program.Run();
        }
    }
}
