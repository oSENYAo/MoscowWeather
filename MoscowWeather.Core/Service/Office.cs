using System;

namespace MoscowWeather.Core.Service
{
    public abstract class Office
    {
        public virtual void Read(string filePath)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(string filePath)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
