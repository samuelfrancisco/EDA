using System;
using EDA.Poc.Infraestrutura.Images.Enums;

namespace EDA.Poc.Infraestrutura.Images.Services.Interfaces
{
    public interface IImagePathService
    {
        string GetImagePath(Guid id, ImageCollection imageCollection, ImageFormat imageFormat);
    }
}
