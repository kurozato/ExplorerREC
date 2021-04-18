using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSugar.Repository
{
    internal class Encode
    {
        public const int ENCODE_SHIFT_JIS = 0;
    }

    public interface IFileReader
    {
        TResult Read<TResult>(string path);
    }

    public class FileReader : IFileReader
    {
        public TResult Read<TResult>(string path)
        {
            if (File.Exists(path) == false)
                return default(TResult);

            TResult result;
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {

                using (var reader = new StreamReader(stream, Encoding.GetEncoding(Encode.ENCODE_SHIFT_JIS)))
                {
                    result = JsonConvert.DeserializeObject<TResult>(reader.ReadToEnd());
                    reader.Close();
                }
                stream.Close();
            }
            return result;
        }
    }

    public interface IFileWriter
    {
        void Write(string path, object value);
    }

    public class FileWriter : IFileWriter
    {
        public void Write(string path, object value)
        {
            var json = JsonConvert.SerializeObject(value);
            using (var writer = new StreamWriter(path, false, Encoding.GetEncoding(Encode.ENCODE_SHIFT_JIS)))
            {
                writer.Write(json);
                writer.Close();
            }
        }
    }

}