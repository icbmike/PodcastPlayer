using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PodcastPlayer.Commands
{
    public static class FileExtensions
    {
        public static async Task CreateIfNotExists(string path, string withText = "")
        {
            if (!File.Exists(path))
            {
                using(var fs = File.Create(path)){
                    var bytes = Encoding.UTF8.GetBytes(withText);

                    await fs.WriteAsync(
                        bytes,
                        0,
                        bytes.Length
                    );
                }
            }
        }
    }
}
