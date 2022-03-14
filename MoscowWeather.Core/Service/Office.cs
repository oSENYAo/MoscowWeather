using System;
using System.IO;

namespace MoscowWeather.Core.Service
{
    public abstract class Office
    {
        public virtual void Remove(string filePath)
        {
            if (File.Exists(filePath))
                    File.Delete(filePath);
        }
    }
}
