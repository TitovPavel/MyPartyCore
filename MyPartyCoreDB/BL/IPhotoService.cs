using Microsoft.AspNetCore.Http;
using MyPartyCore.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPartyCore.DB.BL
{
    public interface IPhotoService
    {
        void UpdatePhoto(int fileID, IFormFile file);
        FileModel AddPhoto(IFormFile file);
        FileModel GetFileByID(int fileID);
    }
}
