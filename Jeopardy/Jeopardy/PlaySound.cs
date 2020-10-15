using System;

using System.Media;
using System.IO;

namespace Jeopardy
{
    class PlaySound
    {
        public static SoundPlayer spMain;

        static public void PlaySounds(Stream StreamFile)
        {
            Stream strmMain = StreamFile;
            using (spMain = new SoundPlayer(strmMain))
            {
                if (spMain != null) spMain.Stop();
                spMain.LoadAsync();

                spMain.Play();

                spMain.Dispose();
                strmMain.Dispose();
            }
        }


    }
}
