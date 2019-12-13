using System.IO;

namespace movable_2dmap
{
    public static class SaveFile
    {
        public static void SaveFileToTxt(string path, string name)
        {
            using (StreamWriter streamWriter = File.CreateText(path))
            foreach(var b in MapGenerator.map)
            {
                streamWriter.WriteLine(b.ToString());
            }
        }
    }
}
