using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reassessASE
{
    public class FileHandler
    {
        /// <summary>
        /// Reading the contents from the file
        /// </summary>
        /// <param name="filePath">file path string input</param>
        /// <returns></returns>
        /// <exception cref="GPLexception"></exception>
        public string ReadFromFile(string filePath)
        {
            try
            {
                // Ensure that the file exists before attempting to read
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"The file '{filePath}' was not found.");
                }

                // Use the using statement correctly for resource management
                using (var reader = new StreamReader(filePath))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new GPLexception("ERROR: Cannot read the file. " + ex.Message);
            }
        }

        /// <summary>
        /// Writing the contents to the file
        /// </summary>
        /// <param name="filePath">file path string input</param>
        /// <param name="content">contents to be written to the file</param>
        /// <exception cref="GPLexception"></exception>
        public void WriteToFile(string filePath, string content)
        {
            try
            {
                // FileMode.Create will create a new file, or overwrite if the file exists
                using (var stream = File.Open(filePath, FileMode.Create))
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(content); // Changed from WriteLine to Write for more flexibility
                }
            }
            catch (Exception ex)
            {
                throw new GPLexception("ERROR: Cannot save the file. " + ex.Message);
            }
        }
    }
}
