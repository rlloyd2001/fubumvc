using System;
using System.IO;
using FubuCore.Descriptions;
using FubuMVC.Core.Http;

namespace FubuMVC.Core.Caching
{
    public class WriteStream : IRecordedHttpOutput, DescribesItself
    {
        private readonly MemoryStream _stream;
        private readonly object _locker = new object();

        public WriteStream(MemoryStream stream)
        {
            _stream = stream;
        }

        public void Replay(IHttpResponse response)
        {
            response.Write(stream =>
            {
                lock (_locker)
                {
                    _stream.Position = 0;
                    _stream.CopyTo(stream);
                }
            });
        }

        public string ReadAll()
        {
            lock (_locker)
            {
                _stream.Position = 0;
                var reader = new StreamReader(_stream);
                return reader.ReadToEnd();
            }
        }

        public void Describe(Description description)
        {
            description.Title = "Write to stream";
            description.LongDescription = ReadAll();
        }
    }
}