using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace VMUBinToH00
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] file = File.ReadAllBytes("CA2CHILD.VMS");
            List<string> strings = new List<string>();
            List<byte> bytes = new List<byte>();
            int startOffset = 0;

            for(int i =0; i < file.Length; i++)
            {
                if (bytes.Count < 0x10)
                {
                    if (bytes.Count == 0)
                        startOffset = i;
                    bytes.Add(file[i]);
                }
                else
                {
                    if (bytes.Count > 0)
                    {
                        string line = ":";
                        //string  size = (bytes.Count / 10).ToString();
                        //size += (bytes.Count % 10).ToString();
                        line += bytes.Count.ToString("X2");
                        line += startOffset.ToString("X4");
                        line += "00";
                        for (int j = 0; j < bytes.Count; j++)
                            line += bytes[j].ToString("X2");
                        line += "00";
                        strings.Add(line);
                        bytes.Clear();
                        i--;
                    }
                }
            }
            File.WriteAllLines("CA2CHILD.h00", strings.ToArray());
        }
    }
}
