using System;
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
        public List<Creature> creatures = new List<Creature>();
        

        public void ParseCreaturesFile(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            try
            {
                string name;
                string position;
                float moveSpeed;
                string texture;

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Parse;
                XmlReader reader = XmlReader.Create("creatures/creatures.xml", settings);

                reader.MoveToContent();
                // Parse the file and display each of the nodes.
                while (reader.Read())
                {
                    while (reader.ReadToFollowing("Creature") != false)
                    {
                        reader.ReadToFollowing("name");
                        name = reader.ReadElementContentAsString();
                        reader.ReadToFollowing("texture");
                        texture = reader.ReadElementContentAsString();
                        reader.ReadToFollowing("moveSpeed");
                        moveSpeed = reader.ReadElementContentAsFloat();
                        reader.ReadToFollowing("startingPosition");
                        position = reader.ReadElementContentAsString();

                        string[] pos = position.Split(',');
                        creatures.Add(new Creature(name, new Vector2(float.Parse(pos[0]), float.Parse(pos[1])),
                                      moveSpeed, contentManager.Load<Texture2D>(texture)));
                    }
                }
            }
            catch (Exception ex)
            {
                ex.GetType(); // Öppna en dialogue ruta där det står att det var en exception, fortsätt sen köra.
                
                //om det kommer en exception i creatures.Add() så kan det komma en till exception senare, om creature:n görs men har felaktiga variabler.
            }
        }
    }
}
