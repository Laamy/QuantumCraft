using System.Xml;

namespace QuantumCraft
{
    public class GameManifest
    {
        public static string ManifestPath = "";

        public static string GameVersion
        {
            get
            {
                string result = null;

                using (XmlReader reader = XmlReader.Create(ManifestPath))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && reader.Name == "Identity")
                        {
                            string gameVersion = reader["Version"];
                            result = gameVersion;
                            reader.Dispose();
                            break;
                        }
                    }
                }

                return result;
            }
        }

        public static string GameArchitecture
        {
            get
            {
                string result = null;

                using (XmlReader reader = XmlReader.Create(ManifestPath))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && reader.Name == "Identity")
                        {
                            string gameVersion = reader["ProcessorArchitecture"];
                            result = gameVersion;
                            reader.Dispose();
                            break;
                        }
                    }
                }

                return result;
            }
        }
    }
}