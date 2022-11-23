using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;

using TwitterUCU;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider();
            IPicture picture = provider.GetPicture(@"beer.jpg");
            
            FilterNegative filterNegative = new FilterNegative();
            FilterGreyscale filterGreyscale = new FilterGreyscale();
            PipeNull pipeNull = new PipeNull();
            /*    distinta secuencia
            PipeSerial pipeSerial2 = new PipeSerial(filterNegative, pipeNull);
            PipeSerial pipeSerial1 = new PipeSerial(filterGreyscale, pipeSerial2);
            IPicture image = pipeSerial1.Send(picture);
            */
            PipeForkConditional fork = new PipeForkConditional(filterGreyscale, pipeNull);  
            IPicture test = fork.Send(picture);       
            
        }
    }
}
