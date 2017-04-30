﻿using System;
using System.IO;

namespace Andlogger
{
    public class FileLog : IStrategy
    {
        private string path;
        private Level level;
        private string fileName = "logger.txt";

        public FileLog(Level level, string path)
        {
            this.level = level;
            this.path = path;
        }

        public void Save(Level level, string log)
        {
            if (level <= this.level)
            {
                try
                {
                    if (File.Exists(fileName))
                    {
                        FileInfo fileInfo = new FileInfo(fileName);
                        if (fileInfo.Length > 2048)
                        {
                            File.Move(fileName, fileName + Guid.NewGuid().ToString());
                            using (StreamWriter streamWriter = File.CreateText(fileName))
                            {
                                streamWriter.WriteLine(log);
                            }
                        }
                        else
                        {
                            using (StreamWriter streamWriter = File.AppendText(fileName))
                            {
                                streamWriter.WriteLine(log);
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter streamWriter = File.CreateText(fileName))
                        {
                            streamWriter.WriteLine(log);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}