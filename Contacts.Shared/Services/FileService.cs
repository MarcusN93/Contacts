using Contacts.Shared.Interfaces;
using System.Diagnostics;

namespace Contacts.Shared.Services
{
    public class FileService : IFileService
    {

        /// <summary>
        /// Get content as string from a specified filepath
        /// </summary>
        /// <param name="filePath"> enter the filepath with extension (eg. c:\projects\myfile.json) </param>
        /// <returns> returns file content as string if file exists, else returns null </returns>
        public string GetContentFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
                
            }
            catch (Exception ex) { Debug.WriteLine("FileService - GetContentFromFile:: " + ex.Message); }
            return null!;
        }

        /// <summary>
        /// Save content to a specified filepath
        /// </summary>
        /// <param name="filePath"> enter the filepath with extension (eg. c:\projects\myfile.json) </param>
        /// <param name="content"> enter your content as a string </param>
        /// <returns> returns true if saved, else false if failed </returns>
        public bool SaveContentToFile(string filePath, string content)
        {
            try
            {
                using var sw = new StreamWriter(filePath);
                sw.WriteLine(content);
                return true;
            }
            catch (Exception ex) { Debug.WriteLine("FileService - SaveContentToFile:: " + ex.Message); }
            return false;
        }
    }
}
