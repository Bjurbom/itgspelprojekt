using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input; // unnecessary
using Itgspelprojekt.Creatures;

namespace Itgspelprojekt.XML
{
    class XMLReader
    {
        string creaturesFile = string.Empty;
        public List<Creature> creatures = new List<Creature>();

        // Tommies mirakulösa XML-läsande kod

        public List<List<int>> ReadResolutions()
        {
            List<List<int>> resolutions = new List<List<int>>();

            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };
            XmlReader reader = XmlReader.Create("XML/Resolutions.xml", settings);

            try
            {
                reader.MoveToContent();
                // Parse the file and display each of the nodes.
                while (reader.Read())
                {
                    while (reader.ReadToFollowing("resolution") != false)
                    {
                        string s = reader.ReadElementContentAsString();

                        string[] resolution = s.Split(','); // Split position, e.g. "1337, 420" into "1337" and " 420", both of which can be correctly parsed as floats
                        resolutions.Add(new List<int>());
                        resolutions[resolutions.Count - 1].Add(int.Parse(resolution[0]));
                        resolutions[resolutions.Count - 1].Add(int.Parse(resolution[1]));
                    }
                }
                reader.Dispose();
                return resolutions;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return resolutions;
            }
        }

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
                XmlReader reader = XmlReader.Create("XML/creatures.xml", settings);

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
                reader.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString(); // Another exception can occur later if the creature was created, but with the wrong variables, or if one or more creatures weren't created.
            }
            return String.Empty;
        }
    }
}
