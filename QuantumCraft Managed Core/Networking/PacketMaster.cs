using QuantumCraft.Packet;

namespace QuantumCraft.Networking
{
    public class PacketMaster
    {
        /// <summary>
        /// The current QuantumMaster used to make packet requests
        /// </summary>
        public static QuantumMaster QuantumMaster { get; set; } = new QuantumMaster();

        /// <summary>
        /// Get the latest VersionPacket from the master
        /// </summary>
        public static VersionPacket Version
        {
            get
            {
                // get the latest version data
                string iniData = QuantumMaster.GetDirectAsync("Version", "txt").Result;

                // start up a new DataService to parse the latest version data
                DataService data = new DataService(iniData);

                // create a new instance of VersionPacket
                VersionPacket packet = new VersionPacket();

                // fill in the packets details
                {
                    packet.Quantum = data["Latest", "Quantum"];

                    packet.Minecraft = data["Latest", "Minecraft"].Split(
                        new string[] { "," },
                        System.StringSplitOptions.RemoveEmptyEntries
                    );
                }

                // return packet
                return packet;
            }
        }
    }
}