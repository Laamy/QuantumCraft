using System;
using System.Collections.Generic;

namespace QuantumCraft
{
    /// <summary>
    /// DataService
    /// </summary>
    public class DataService
    {
        // stuff stored in the singlton
        private Dictionary<string, Dictionary<string, string>> configData;
        private string data;

        /// <summary>
        /// Initialize a new instance of (Singlton object) DataService
        /// </summary>
        public DataService(string data)
        {
            configData = new Dictionary<string, Dictionary<string, string>>();
            this.data = data;
            Read();
        }

        /// <summary>
        /// Get a value stored in key from datastore using a custom header
        /// </summary>
        /// <param name="key">Key to read from</param>
        /// <returns>Value read from the key in the custom header</returns>
        /// <exception cref="KeyNotFoundException">Failure to find header or key</exception>
        public string this[string headerName, string key]
        {
            get
            {
                if (configData.ContainsKey(headerName) && configData[headerName].ContainsKey(key))
                {
                    return configData[headerName][key];
                }
                else
                {
                    throw new KeyNotFoundException($"Header '{headerName}' or key '{key}' not found.");
                }
            }
        }

        /// <summary>
        /// Get a value stored in key from datastore using the "default" header
        /// </summary>
        /// <param name="key">Key to read from</param>
        /// <returns>Value read from the key in the "default" header</returns>
        /// <exception cref="KeyNotFoundException">Failure to find header or key</exception>
        public string this[string key]
        {
            get => this["header", key];
        }

        private void Read()
        {
            string currentHeader = null;

            foreach (string line in this.data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.Trim().StartsWith("[") && line.Trim().EndsWith("]"))
                {
                    currentHeader = line.Trim().Substring(1, line.Trim().Length - 2);
                    configData[currentHeader] = new Dictionary<string, string>();
                }
                else if (!string.IsNullOrWhiteSpace(line) && line.Contains("=") && currentHeader != null)
                {
                    int separatorIndex = line.IndexOf('=');
                    string key = line.Substring(0, separatorIndex).Trim();
                    string value = line.Substring(separatorIndex + 1).Trim();

                    configData[currentHeader][key] = value;
                }
            }
        }
    }
}
