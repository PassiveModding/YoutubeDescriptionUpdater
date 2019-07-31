using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;

namespace Youtube.NET
{
    public class Servicer
    {
        public YouTubeService YTService { get; set; }

        public async Task<YouTubeService> CreateService(string filePath)
        {
            UserCredential credential;
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows for read-only access to the authenticated 
                    // user's account, but not other types of account access.
                    new[] { YouTubeService.Scope.Youtube, YouTubeService.Scope.YoutubeForceSsl, YouTubeService.Scope.YoutubeReadonly },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(GetType().ToString())
                );
            }

            var service = new YouTubeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = GetType().ToString()
            });
            YTService = service;
            return service;
        }
    }
}
