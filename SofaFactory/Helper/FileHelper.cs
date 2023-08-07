using System;
namespace SofaFactory.Helper
{
	public static class FileHelper
	{
        //public static async Task<string> SaveFileAsync(IFormFile file, string path)
        //{
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }
        //    var fileName = string.Concat(Path.GetFileNameWithoutExtension(file.FileName),
        //                                  "_",
        //                                  DateTime.Now.ToString("yyyyMMddTHHmmss"),
        //                                  Path.GetExtension(file.FileName));
        //    var pathWithName = Path.Combine(path, fileName);
        //    using (FileStream stream = new FileStream(pathWithName, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }
        //    var relativePath = Path.GetRelativePath("wwwroot", pathWithName);
        //    return relativePath;
        //}

        public static async Task<List<string>> SaveFilesAsync(List<IFormFile> files, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileUrls = new List<string>();

            foreach (var file in files)
            {
                var fileName = string.Concat(Path.GetFileNameWithoutExtension(file.FileName),
                                             "_",
                                             DateTime.Now.ToString("yyyyMMddTHHmmss"),
                                             Path.GetExtension(file.FileName));

                var pathWithName = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(pathWithName, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var relativePath = Path.GetRelativePath("wwwroot", pathWithName);
                fileUrls.Add(relativePath);
            }

            return fileUrls;
        }

        public static bool DeleteFile(string path)
        {

            var file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                return true;
            }
            return false;
        }
    }
}

