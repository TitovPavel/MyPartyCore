using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.DB.BL;
using MyPartyCore.DB.Models;

namespace MyPartyCore.Controllers
{
    public class FileController : Controller
    {

        private readonly IPhotoService _photoService;
        private readonly IHostingEnvironment _environment;

        public FileController(IPhotoService photoService,
            IHostingEnvironment environment)
        {
            _photoService = photoService;
            _environment = environment;
        }

        public ActionResult GetImage(int fileID)
        {
            FileModel file = _photoService.GetFileByID(fileID);

            if (file != null)
            {
                string path = Path.Combine(_environment.WebRootPath, file.Path);
                if (System.IO.File.Exists(path))
                {
                    FileStream fs = new FileStream(path, FileMode.Open);
                    string file_type = "image/jpg";
                    return File(fs, file_type, file.Name);
                }
            }

            return null;
        }
    }
}