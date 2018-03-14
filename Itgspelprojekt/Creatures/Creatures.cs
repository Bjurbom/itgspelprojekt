﻿/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; // unnecessary

namespace Itgspelprojekt.Creatures
{
    class Creatures
    {
        string creaturesFile = string.Empty;
        List<Creature> creatures = new List<Creature>();
        public void ReadCreaturesTXT(string creaturesFile)
        {
            creaturesFile = "content/creatures.xnb";
            int counter = 0;
            string line;

            System.IO.StreamReader file =
                new System.IO.StreamReader(creaturesFile);
            while ((line = file.ReadLine()) != null)
            {
                this.creaturesFile += line + Environment.NewLine;
            }
            file.Close();

        }
        
        
        

        public void ParseCreaturesFile(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            string name;
            string position;
            string moveSpeed;
            string texture;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create("content/creatures.xnb", settings);

            reader.MoveToContent();
            // Parse the file and display each of the nodes.
            while (reader.Read())
            {
                while (reader.ReadToFollowing("Creature") != false)
                {
                    name = reader.GetAttribute("name");
                    position = reader.GetAttribute("startingPosition");
                    moveSpeed = reader.GetAttribute("moveSpeed");
                    texture = reader.GetAttribute("texture");

                    string[] pos = position.Split(',');
                    creatures.Add(new Creature(name, new Vector2(float.Parse(pos[0]), float.Parse(pos[1])), 
                                  float.Parse(moveSpeed), contentManager.Load<Texture2D>(texture)));


                }

            }
        }
    }
}
*/