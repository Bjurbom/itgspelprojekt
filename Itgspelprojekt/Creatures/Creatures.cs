using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itgspelprojekt.Creatures
{
    class Creatures
    {

        public void ReadCreaturesTXT(string creaturesFile)
        {
            creaturesFile = "content/creatures.xnb";
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(creaturesFile);
            while ((line = file.ReadLine()) != null)
            {



            }

            file.Close();
        }
    }
}
