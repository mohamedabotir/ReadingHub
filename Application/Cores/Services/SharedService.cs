using Microsoft.AspNetCore.Hosting;
using ReadingHub.Unit.Abstracts;

namespace ReadingHub.Cores.Services
{
    public class SharedService : ISharedService
    {
        private readonly IWebHostEnvironment Environment;
        public SharedService(IWebHostEnvironment env)
        {
            Environment = env;
        }
        public string GetAuthorPicture(string id)
        {
            var files = Directory.GetFiles(GetProfileContentsDirectory());

            var file = files.FirstOrDefault(x => x.Contains(id.ToString()));
            if (file != null)
                return id + "." + file.Split(".")[1];


            return null;
        }

        public string GetProfileContentsDirectory()
        {
            return Environment.ContentRootPath + "wwwroot/profile";
        }
    }
}
