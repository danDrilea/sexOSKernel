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
                        break;
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
                        break;
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
                        break;
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
                        break;
                    }
                    break;
                case "writestring"://file writestring 0:\Myfile.txt "abcacaca"
                    try
                    {
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(args[1]).GetFileStream();
                        if(fs.CanWrite)//can write to the file
                        {
                            int ctr = 0;
                            StringBuilder sb = new StringBuilder();
                            foreach(String s in args)
                            {
                                if (ctr > 1)
                                    sb.Append(s + ' ');
                                ++ctr;
                            }
                            //need to convert the string to byte sequences
                            //byte array
                            String txt = sb.ToString();
                            Byte[] data = Encoding.ASCII.GetBytes(txt.Substring(0, txt.Length - 1));
                            fs.Write(data, 0, data.Length);
                            fs.Close();
                            response = "Sucessfully wrote to the file!";

                        }
                        else
                        {
                            response = "Unable to write to file, it is not open for writing.";
                            break;
                        }
                    }
                    catch(Exception ex)
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
                default:
                    response = "What fucking argument did you give(" + args[1] + ")?";
                    break;
            }
            return response;
        }

    }
}
