using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Sys = Cosmos.System;
namespace sexOSKernel.Commands
{
    public class File : Command
    {
        public File(String name, string description) : base(name, description) { }  
        public override string Execute(string[] args)
        {
            //file create Myfile.txt
            string response = "";
            switch(args[0])//create
            {
                case "create":
                    ///vfs - file system 
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.CreateFile(args[1]);
                        response = "Your file: " + args[1] + " was sucessfully created!";
                        //0: echivalent cu C:
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }

                    break;
                case "delete":
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.DeleteFile(args[1]);
                        response = "Your file: " + args[1] + " was sucessfully removed!";
                        //0: echivalent cu C:
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;
                case "createdir":
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.CreateDirectory(args[1]);
                        response = "Sucesfully created the directory " + args[1];
                    }
                    catch(Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;
                case "removedir":
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.DeleteDirectory(args[1], true);
                        response = "Sucesfully deleted the directory " + args[1];
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;
                case "listdir"://Aici trebuie sa listam directoarele
                    try
                    {
                        var directories = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(args[1]);
                        foreach (var dir in directories)
                        {
                            Console.WriteLine(dir.mName);
                        }
                    }
                    catch(Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }
                    break;
                case "writestring": //file writestring 0:\Myfile.txt "abcacaca"
                    try
                    {
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(args[1]).GetFileStream();
                        if (fs.CanWrite) //can write to the file
                        {
                            // Clear the content of the file before writing new content
                            fs.SetLength(0); // This line flushes the existing content by setting the file length to 0

                            int ctr = 0;
                            StringBuilder sb = new StringBuilder();
                            foreach (String s in args)
                            {
                                if (ctr > 1)
                                    sb.Append(s + ' ');
                                ++ctr;
                            }
                            //need to convert the string to byte sequences
                            //byte array
                            String txt = sb.ToString();
                            Byte[] data;
                            data = Encoding.ASCII.GetBytes(txt.Substring(0, txt.Length - 1));
                            fs.Write(data, 0, data.Length);
                            fs.Close();
                            response = "Successfully wrote to the file!";
                        }
                        else
                        {
                            response = "Unable to write to file, it is not open for writing.";
                        }
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }
                    break;

                case "readstring":
                    try
                    {
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(args[1]).GetFileStream();
                        if (fs.CanRead)
                        {
                            Byte[] data = new Byte[256];
                            fs.Read(data, 0, data.Length);//function copies what's in the filestream to data(kinda like a void)
                            response = Encoding.ASCII.GetString(data);
                        }
                        else
                        {
                            response = "Unable to read from file, not open for reading.";
                            break;

                        }
                    }
                    catch(Exception ex)
                    {
                        response = ex.ToString();
                        break;
                    }
                    break;
                case "":
                    response = "No arguments given.";
                    break;
                default:
                    try
                    {
                        response = "Invalid input.";
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                        break;

                    }
                    break;
            }
            return response;
        }

    }
}
