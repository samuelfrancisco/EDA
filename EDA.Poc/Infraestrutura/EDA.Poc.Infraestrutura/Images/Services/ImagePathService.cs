using System;
using System.Configuration;
using EDA.Poc.Infraestrutura.Images.Enums;
using EDA.Poc.Infraestrutura.Images.Services.Interfaces;

namespace EDA.Poc.Infraestrutura.Images.Services
{
    public class ImagePathService : IImagePathService
    {
        public string GetImagePath(Guid id, ImageCollection imageCollection, ImageFormat imageFormat)
        {
            return string.Format("{0}{1}/{2}-{3}.png",
                ConfigurationManager.AppSettings["CaminhoDasImagens"],
                imageCollection.ToString(),
                id.ToString(),
                imageFormat.ToString());
        }
    }
}
