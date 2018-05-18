using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Itgspelprojekt.Menus
{
    class Settings // this class is used to pass the output of Settings.Update() to the caller
    {
        public bool debugActive;
        public int[] resolution;
        public bool goBack;
    }
}
