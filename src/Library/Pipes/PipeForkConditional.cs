using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompAndDel;
using TwitterUCU;
using CognitiveCoreUCU;
using CompAndDel.Filters;

namespace CompAndDel.Pipes
{
    public class PipeForkConditional : IPipe
    {
        protected IFilter filtro;
        protected IPipe nextPipe;
        
        /// <summary>
        /// La cañería recibe una imagen, le aplica un filtro y la envía a la siguiente cañería
        /// </summary>
        /// <param name="filtro">Filtro que se debe aplicar sobre la imagen</param>
        /// <param name="nextPipe">Siguiente cañería</param>
        public PipeForkConditional(IFilter filtro, IPipe nextPipe)
        {
            this.nextPipe = nextPipe;
            this.filtro = filtro;

        }
        /// <summary>
        /// Devuelve el proximo IPipe
        /// </summary>
        public IPipe Next
        {
            get { return this.nextPipe; }
        }
        /// <summary>
        /// Devuelve el IFilter que aplica este pipe
        /// </summary>
        public IFilter Filter
        {
            get { return this.filtro; }
        }
        /// <summary>
        /// Recibe una imagen, le aplica un filtro y la envía al siguiente Pipe
        /// </summary>
        /// <param name="picture">Imagen a la cual se debe aplicar el filtro</param>
        public IPicture Send(IPicture picture)
        {
            FilterNegative neg = new FilterNegative();
            PictureProvider provider = new PictureProvider();
            provider.SavePicture(picture, @"a.jpg");
            CognitiveFace cog = new CognitiveFace(true);
            cog.Recognize(@"a.jpg");
            if (cog.FaceFound)
            {
                picture = this.filtro.Filter(picture);
                provider.SavePicture(picture, @"Cara.jpg");
            }
            else
            {
                picture = neg.Filter(picture);
                provider.SavePicture(picture, @"No cara.jpg");
            }
            
            //var twitter = new TwitterImage();
            //Console.WriteLine(twitter.PublishToTwitter($"Beer {this.Filter.Filtro}", $@"beer {this.Filter.Filtro}.jpg"));
            
            return this.nextPipe.Send(picture);
        }
    }
}
