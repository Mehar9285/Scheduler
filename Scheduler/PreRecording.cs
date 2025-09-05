using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class PreRecording : IContent
    {
        private readonly string _title;
        private readonly int _durationtominute;
        private bool _playing;




        public PreRecording(string title, int durationtominute, bool playing)
        {
            _title = title;
            _durationtominute = durationtominute;
            _playing = playing;
        }




        public int DurationInMinutes()
        {
            if (_playing)
                return _durationtominute;
            return 0;
        }

        public string Title()
        {
            if (_playing)
                return _title;
            return _playing ? _title : "No Pre Recordings Available";
        }



    }
}
