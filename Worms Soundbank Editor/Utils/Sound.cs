using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worms_Soundbank_Editor.Utils
{
    public class Sound
    {
        public string FileName { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public bool Used { get; private set; }
        public string SoundPath { get; set; }
        public double SoundDuration { get; set; }

        public Sound(string FileName, string DisplayName, string Description, bool Used)
        {
            this.FileName = FileName;
            this.DisplayName = DisplayName;
            this.Description = Description;
            this.Used = Used;
        }
    }
}
