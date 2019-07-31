using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace Youtube.NET
{
    public class Uploads
    {
        public Uploads(YouTubeService service)
        {
            youtubeService = service;
        }

        private YouTubeService youtubeService { get; }

        public async Task UpdateDescription(Video video, string newDescription)
        {
            await Log.LogMessage($"Updating video {video.Snippet.Title} ({video.Id}) Description from:\n---------\n{video.Snippet.Description}\n----------\nto:\n----------\n{newDescription}");
            video.Snippet.Description = newDescription;
            var request = youtubeService.Videos.Update(video, "snippet");
            await request.ExecuteAsync();
            await Log.LogMessage("\n----------\nFinished Updating Video.\n----------\n");
        }

        public async Task<List<Video>> GetMyVideos()
        {
            var userVideos = await Run();
            var videoIds = userVideos.Select(x => x.Snippet.ResourceId.VideoId).ToArray();

            var resList = new List<Video>();

            var nextPageToken = "";
            var videoRequest = youtubeService.Videos.List("snippet");
            int takenAmount = 0;
            
            Console.WriteLine($"Batching requests for the following videos: {videoRequest.Id}");
            while (takenAmount < videoIds.Length)
            {
                var batchedVideos = videoIds.Skip(takenAmount).Take(50).ToArray();
                videoRequest.Id = string.Join(",", batchedVideos);
                takenAmount += batchedVideos.Length;

                videoRequest.PageToken = nextPageToken;
                videoRequest.MaxResults = 50;
                var videos = await videoRequest.ExecuteAsync();

                foreach (var video in videos.Items)
                {
                    resList.Add(video);
                }
                nextPageToken = videos.NextPageToken;
                
                Console.WriteLine($"Added {videos.Items.Count} videos to myvideo response.");
            }

            
            Console.WriteLine($"{resList.Count} User Uploads found.");
            return resList;
        }

        public async Task<List<PlaylistItem>> Run()
        {
            var channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.Mine = true;

            // Retrieve the contentDetails part of the channel resource for the authenticated user's channel.
            var channelsListResponse = await channelsListRequest.ExecuteAsync();

            var videos = new List<PlaylistItem>();
            foreach (var channel in channelsListResponse.Items)
            {
                // From the API response, extract the playlist ID that identifies the list
                // of videos uploaded to the authenticated user's channel.
                var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;

                Console.WriteLine("Videos in list {0}", uploadsListId);

                var nextPageToken = "";
                while (nextPageToken != null)
                {
                    var playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet");
                    playlistItemsListRequest.PlaylistId = uploadsListId;
                    playlistItemsListRequest.MaxResults = 50;
                    playlistItemsListRequest.PageToken = nextPageToken;

                    // Retrieve the list of videos uploaded to the authenticated user's channel.
                    var playlistItemsListResponse = await playlistItemsListRequest.ExecuteAsync();

                    foreach (var playlistItem in playlistItemsListResponse.Items)
                    {
                        // Print information about each video.
                        videos.Add(playlistItem);
                    }

                    nextPageToken = playlistItemsListResponse.NextPageToken;
                }
            }
            
            Console.WriteLine($"{videos.Count} Videos found.");
            return videos;
        }
    }
}