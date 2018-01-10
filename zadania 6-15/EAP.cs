using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO2_Lab
{
    class EAP
    {
        delegate void EventHandler(EventArgs args);
        static event EventHandler foo;
        static void baseEvent(EventArgs args)
        {
            Console.WriteLine("Pozdrowienia z glownego ciala zdarzenia");
        }
        public EAP()
        {
            foo = new EventHandler(baseEvent);
            foo += myEvent;
            foo.Invoke(new EventArgs());
        }

        static void myEvent(EventArgs args)
        {
            Console.WriteLine("i z ciala zaimplementowanego w ramach klienta oprogramowania");
        }
    }
}
