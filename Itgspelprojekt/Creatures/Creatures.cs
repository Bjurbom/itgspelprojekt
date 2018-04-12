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

        // Tommies mirakulösa XML-läsande kod
        

        public string ParseCreaturesFile(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            try
            {
                string name;
                string position;
                float moveSpeed;
                string texture;

                XmlReaderSettings settings = new XmlReaderSettings
                {
                    DtdProcessing = DtdProcessing.Parse
                };
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

                        string[] pos = position.Split(','); // Split position, e.g. "1337, 420" into "1337" and " 420", both of which can be correctly parsed as floats
                        creatures.Add(new Creature(name, new Vector2(float.Parse(pos[0]), float.Parse(pos[1])),
                                      moveSpeed, contentManager.Load<Texture2D>(texture)));
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString(); // Another exception can occur later if the creature was created, but with the wrong variables, or if one or more creatures weren't created.
            }
            return String.Empty;
        }
    }
}
