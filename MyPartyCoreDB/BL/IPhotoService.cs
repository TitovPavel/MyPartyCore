using Microsoft.AspNetCore.Http;
using MyPartyCore.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPartyCore.DB.BL
{
    public interface IPhotoService
    {
        void UpdatePhoto(int fileID, IFormFile file);
        FileModel AddPhoto(IFormFile file);
        FileModel GetFileByID(int fileID);
        Task<string> GetPathAvatarByUserId(string userId);
        void DeletePhotoFromUser(User user);
    }
}
