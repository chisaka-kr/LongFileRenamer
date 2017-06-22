using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongFileRenamer {
    class Program {
        static void Main(string[] args) {
            if (args.Length > 0) {
                RecursiveGetDirectory(args[0]);
            }
            else {
                Console.WriteLine("No input");
            }
            System.Console.Read();
        }
        static void RecursiveGetDirectory(string directoryName) {
            var list = Directory.GetDirectories(directoryName);
            if (list.Length > 0) {
                //폴더가 있음

                foreach(string inDirectoryName in list) {
                    var dirName = Path.GetDirectoryName(inDirectoryName);
                    if (System.Text.ASCIIEncoding.Unicode.GetByteCount(dirName) > 156) {
                        System.Console.WriteLine("★ Long Directory Name:" + dirName);
                    }
                    //System.Console.WriteLine("[D]:"+inDirectoryName);
                    RecursiveGetDirectory(inDirectoryName);
                }
            }
            else {
                //파일만 있음
                var file_list = Directory.GetFiles(directoryName);
                int count = 1;
                    foreach(string filePath in file_list) {
                    //System.Console.WriteLine("[F]:" + fileName);
                    //파일 이름의 길이 체크
                    var fileName = Path.GetFileName(filePath);
                    System.Console.WriteLine(fileName + " LENGTH:" + System.Text.ASCIIEncoding.Unicode.GetByteCount(fileName));
                    //if (System.Text.ASCIIEncoding.Unicode.GetByteCount(fileName) > 156) {
                        System.Console.WriteLine("Long File Name:" + fileName + " LENGTH:" + System.Text.ASCIIEncoding.Unicode.GetByteCount(fileName));
                        //var newFileName = fileName.Substring(0, 70) + " ~"+count+Path.GetExtension(filePath);
                        var newFileName = count + Path.GetExtension(filePath);
                        System.Console.WriteLine("New File Name is: " + newFileName);
                        System.IO.File.AppendAllText(directoryName+"\\renamed file list.txt",fileName+"||"+newFileName+"\r\n");
                        File.Move(filePath, directoryName+"\\"+newFileName);
                       count++;
                    //}
                }

            }
        }
    }
}
