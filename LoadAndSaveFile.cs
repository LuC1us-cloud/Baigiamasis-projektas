using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace movable_2dmap
{
    public static class LoadAndSaveFile
    {
        public static void SaveFile(string path, string name)
        {
            using (StreamWriter streamWriter = File.CreateText(path))
            foreach(var b in MapGenerator.map)
            {
                    streamWriter.WriteLine(b.ToString());
            }
        }
    }
}
