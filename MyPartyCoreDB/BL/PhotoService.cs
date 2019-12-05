using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyPartyCore.DB.DAL;
using MyPartyCore.DB.Models;

namespace MyPartyCore.DB.BL
{
    public class PhotoService : IPhotoService
    {

        private readonly UserManager<User> _userManager;
        private readonly MyPartyContext _context;
        private readonly IHostingEnvironment _environment;

        public PhotoService(MyPartyContext context, IHostingEnvironment environment, UserManager<User> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }

        public FileModel AddPhoto(IFormFile file)
        {
            FileModel photo = new FileModel();

            string newFileName = string.Empty;
            string PathDB = string.Empty;

            if (file.Length > 0)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                string myUniqueFileName = Convert.ToString(Guid.NewGuid());

                string FileExtension = Path.GetExtension(fileName);

                newFileName = myUniqueFileName + FileExtension;

                fileName = Path.Combine(_environment.WebRootPath, "Files") + $@"\{newFileName}";
                PathDB = "Files/" + newFileName;

                using (FileStream fs = System.IO.File.Create(fileName))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }

            photo.Name = newFileName;
            photo.Path = PathDB;

            _context.Files.Add(photo);
            _context.SaveChanges();

            return photo;

        }

        public void DeletePhotoFromUser(User user)
        {
            FileModel fileModel = GetFileByID((int)user.AvatarId);

            if (fileModel != null)
            {
                string fileName = Path.Combine(_environment.WebRootPath, fileModel.Path);

                File.Delete(fileName);

                _context.Files.Remove(fileModel);
                _context.SaveChanges();

            }
        }

        public FileModel GetFileByID(int fileID)
        {
            return _context.Files.SingleOrDefault(x => x.Id == fileID);
        }

        public async Task<string> GetPathAvatarByUserId(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null && user.AvatarId != null)
            {
                FileModel file = GetFileByID((int)user.AvatarId);
                if (file != null)
                {
                    return file.Path;                
                }
            }
            return "Files/placeholder.jpg";
        }

        public void UpdatePhoto(int fileID, IFormFile file)
        {
            FileModel fileModel = GetFileByID(fileID);

            if(fileModel!=null)
            {
                string fileName = Path.Combine(_environment.WebRootPath, fileModel.Path);

                using (FileStream fs = System.IO.File.Open(fileName, FileMode.Create))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }

        }
    }
}
