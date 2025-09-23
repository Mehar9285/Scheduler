using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class LiveStudio : IContent
    {
        private readonly string _title;
        private readonly int _durationtinminute;
        private bool _playing;
        private const string Studio1 = "Studio1 ";
        private const string Studio2 = "Studio2 ";
        public string Host { get; set; }
        public List<string> Guests { get; set; } = new List<string>();




        public string GetStudio(int numberOfHosts, bool anyguest)
        {
            if (numberOfHosts == 1 && !anyguest)
                return Studio1;
            else return Studio2;


        }
        public LiveStudio(string title, int durationtinminute, bool playing = true )
        {
            _title = title;
            _durationtinminute = durationtinminute;
            _playing = playing;
        }
        public int DurationInMinutes()
        {
            if (_playing)
                return _durationtinminute;
            return 0;
        }

       public string Title()
        {
            if (_playing)
                return _title;
            return _playing ? _title : "No Live Session";
        }
        
    }
}
