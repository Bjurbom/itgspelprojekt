using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Itgspelprojekt.Creatures;

namespace Itgspelprojekt.XML
{
    class XMLReader
    {
        string creaturesFile = string.Empty;
        public List<Creature> creatures = new List<Creature>();

        // Tommies mirakulösa XML-läsande kod

        /// <summary>
        /// Read the resolutions from Resolutions.XML and output them as a list of (lists of 2 integers each)
        /// </summary>
        /// <returns></returns>
        public List<List<int>> ReadResolutions() // Code herein should be pretty self-explanatory
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

                while (reader.Read())
                {
                    while (reader.ReadToFollowing("resolution") != false)
                    {
                        string s = reader.ReadElementContentAsString();

                        string[] resolution = s.Split(','); // Split resolution into two seperate integers that can be individually parsed.
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

        /// <summary>
        /// Read the creatures from Creatures.XML and add them to the creatures list which Game1 takes them from.
        /// Returns an error message, if there was one. Else returns string.empty
        /// </summary>
        /// <param name="contentManager"></param>
        /// <returns></returns>
        public string ParseCreaturesFile(Microsoft.Xna.Framework.Content.ContentManager contentManager) // this code should also be fairly self-explanatory
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

                        string[] pos = position.Split(','); // Split position into two seperate floats that can be individually parsed
                        creatures.Add(new Creature(name, new Vector2(float.Parse(pos[0]), float.Parse(pos[1])), // name, position
                                      moveSpeed, contentManager.Load<Texture2D>(texture))); // moveSpeed, texture
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
