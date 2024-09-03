using Google;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

public class ChannelData
{
    public string Title { get; set; }
    public string CustomUrl { get; set; }
    public string Id { get; set; }
    public long VideoCount { get; set; }
    public string PublishedRow { get; set; }
    public bool Success { get; set; }
}
public class VideoData
{
    public string Title { get; set; }
    public string Id { get; set; }
    public string PublishedRow { get; set; }
    public long Duration { get; set; }
    public long ViewCount { get; set; }
    public string ChannelId { get; set; }
    public bool Success { get; set; }
}

public class YouTubeApi
{
    private static readonly string defaultAPIKey = "AIzaSyAz9SEeEoIz_ygz0Pyf-c8OilStZCrVe34";
    private static List<string> APIKeys = new List<string> { defaultAPIKey };
    private static int numAPIKey = 0;

    public static void APIKeyAdd(string apiKey)
    {
        if (!APIKeys.Contains(apiKey))
            APIKeys.Insert(0, apiKey);
    }
    public static void APIKeysClear()
    {
        APIKeys.Clear();
        APIKeys.Add(defaultAPIKey);
        numAPIKey = 0;
}

    public static string DurationToString(int duration)
    {
        string durationStr = "";
        int hours = duration / 3600;
        duration -= hours * 3600;
        int minutes = duration / 60;
        duration -= minutes * 60;
        int seconds = duration;

        if (hours != 0)
        {
            durationStr += $"{hours}:";
            if (minutes < 10)
                durationStr += "0";
        }
        durationStr += $"{minutes}:";
        if (seconds < 10)
            durationStr += "0";
        durationStr += $"{seconds}";

        return durationStr;
    }


    public static int ParseDuration(string durationStr)
    {
        if (durationStr is null)
            return 0;
        int hours = 0;
        int minutes = 0;
        int seconds = 0;

        string numberStr = "";
        foreach (char symbol in durationStr)
        {
            if (symbol == 'T')
            {
                numberStr = "";
            }
            else if (symbol == 'H')
            {
                hours = int.Parse(numberStr);
                numberStr = "";
            }
            else if (symbol == 'M')
            {
                minutes = int.Parse(numberStr);
                numberStr = "";
            }
            else if (symbol == 'S')
            {
                seconds = int.Parse(numberStr);
                numberStr = "";
            }
            else
            {
                numberStr += symbol;
            }
        }

        return hours * 3600 + minutes * 60 + seconds;
    }

    public static string ExtractVideoId(string text)
    {
        if (Regex.IsMatch(text, @"^[a-zA-Z0-9-_]{11}$"))
        {
            return text;
        }

        Match match = Regex.Match(text, @"^([a-zA-Z0-9-_]{11})[^a-zA-Z0-9-_]");
        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        match = Regex.Match(text, @"[^a-zA-Z0-9-_]([a-zA-Z0-9-_]{11})$");
        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        match = Regex.Match(text, @"[^a-zA-Z0-9-_]([a-zA-Z0-9-_]{11})[^a-zA-Z0-9-_]");
        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        return null;
    }

    public async static Task<ChannelData> GetChannelInfoById(string channelId)
    {
        while (true)
        {
            try
            {
                YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = APIKeys[numAPIKey],
                });

                var channelsListRequest = youtubeService.Channels.List("snippet,statistics");
                channelsListRequest.Id = channelId;
                var channelsListResponse = await channelsListRequest.ExecuteAsync();

                if (channelsListResponse != null && channelsListResponse.Items != null && channelsListResponse.Items.Count > 0)
                {
                    var channelData = channelsListResponse.Items[0];

                    return new ChannelData
                    {
                        Title = channelData.Snippet.Title.Replace("'", "''"),
                        CustomUrl = channelData.Snippet.CustomUrl,
                        Id = channelData.Id,
                        VideoCount = (long)channelData.Statistics.VideoCount.Value,
                        PublishedRow = channelData.Snippet.PublishedAtRaw,
                        Success = true
                    };
                }
                return new ChannelData
                {
                    Success = false
                };
            }
            catch (GoogleApiException ex)
            {
                if (ex.Error.Code == 403 && ex.Error.Message.Contains("quota"))
                {
                    numAPIKey++;
                    if (numAPIKey >= APIKeys.Count)
                    {
                        numAPIKey = APIKeys.Count-1;
                        MessageBox.Show("Бесплатная квота на сегодня закончилась!", "Ошибка GoogleApi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show(ex.Error.Message, "Ошибка GoogleApi.GetChannel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка GoogleApi.GetChannel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }

    public async static Task<ChannelData> GetChannelIdByName(string channelName)
    {
        while (true)
        {
            try
            {
                var youtubeService = new YouTubeService(new BaseClientService.Initializer(){
                    ApiKey = APIKeys[numAPIKey]
                });

                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = channelName;
                searchListRequest.Type = "channel";
                searchListRequest.MaxResults = 1;

                var searchListResponse = await searchListRequest.ExecuteAsync();

                if (searchListResponse.Items.Count > 0)
                {
                    return new ChannelData
                    {
                        Id = searchListResponse.Items[0].Id.ChannelId,
                        Success = true
                    };
                }
                return new ChannelData
                {
                    Success = false
                };
            }
            catch (GoogleApiException ex)
            {
                if (ex.Error.Code == 403 && ex.Error.Message.Contains("quota"))
                {
                    numAPIKey++;
                    if (numAPIKey >= APIKeys.Count)
                    {
                        numAPIKey = APIKeys.Count-1;
                        MessageBox.Show("Бесплатная квота на сегодня закончилась!", "Ошибка GoogleApi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show(ex.Error.Message, "Ошибка GoogleApi.GetChannel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка GoogleApi.GetChannel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }

    public async static Task<ChannelData> GetChannelIdByVideoId(string videoUrl)
    {
        while (true)
        {
            try
            {
                YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = APIKeys[numAPIKey],
                });

                string videoId = ExtractVideoId(videoUrl);
                if (string.IsNullOrEmpty(videoId))
                {
                    return null;
                }

                var videosListRequest = youtubeService.Videos.List("snippet");
                videosListRequest.Id = videoId;
                var videosListResponse = await videosListRequest.ExecuteAsync();

                if (videosListResponse.Items.Count > 0)
                {
                    return new ChannelData
                    {
                        Id = videosListResponse.Items[0].Snippet.ChannelId,
                        Success = true
                    };
                }
                return new ChannelData
                {
                    Success = false
                };
            }
            catch (GoogleApiException ex)
            {
                if (ex.Error.Code == 403 && ex.Error.Message.Contains("quota"))
                {
                    numAPIKey++;
                    if (numAPIKey >= APIKeys.Count)
                    {
                        numAPIKey = APIKeys.Count - 1;
                        MessageBox.Show("Бесплатная квота на сегодня закончилась!", "Ошибка GoogleApi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show(ex.Error.Message, "Ошибка GoogleApi.GetChannel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка GoogleApi.GetChannel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }

    public async static Task<VideoData> GetVideoInfoByIdAsync(string videoUrl)
    {
        while (true)
        {
            try
            {
                YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = APIKeys[numAPIKey],
                });

                string videoId = ExtractVideoId(videoUrl);
                if (string.IsNullOrEmpty(videoId))
                {
                    return new VideoData
                    {
                        Success = false
                    };
                }

                var videosListRequest = youtubeService.Videos.List("snippet,contentDetails,statistics,topicDetails");
                videosListRequest.Id = videoId;
                var videosListResponse = await videosListRequest.ExecuteAsync();

                if (videosListResponse != null && videosListResponse.Items != null && videosListResponse.Items.Count > 0)
                {
                    var videoData = videosListResponse.Items[0];

                    if(videoData.Statistics.ViewCount is null || videoData.ContentDetails.Duration is null || videoData.Snippet.PublishedAtRaw is null)
                    {
                        return new VideoData
                        {
                            Success = false
                        };
                    }
                    return new VideoData
                    {
                        Title = videoData.Snippet.Title?.Replace("'", "''") ?? "",
                        Id = videoData.Id,
                        PublishedRow = videoData.Snippet.PublishedAtRaw,
                        Duration = ParseDuration(videoData.ContentDetails.Duration),
                        ViewCount = (long)videoData.Statistics.ViewCount.Value,
                        ChannelId = videoData.Snippet.ChannelId,
                        Success = true
                    };
                }
                return new VideoData
                {
                    Success = false
                };
            }
            catch (GoogleApiException ex)
            {
                if (ex.Error.Code == 403 && ex.Error.Message.Contains("quota"))
                {
                    numAPIKey++;
                    if (numAPIKey >= APIKeys.Count)
                    {
                        numAPIKey = APIKeys.Count - 1;
                        MessageBox.Show("Бесплатная квота на сегодня закончилась!", "Ошибка GoogleApi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show(ex.Error.Message, "Ошибка GoogleApi.GetVideo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка GoogleApi.GetVideo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }

    public async static Task<List<VideoData>> LoadingVideosFromChannel(string channelId, DateTimeOffset? publishedAfter, DateTimeOffset? publishedBefore)
    {
        string nextPageToken = null;
        List<VideoData> videos = new List<VideoData>();
        while (true)
        {
            try
            {
                YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = APIKeys[numAPIKey],
                });

                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.ChannelId = channelId;
                searchListRequest.Type = "video";
                searchListRequest.MaxResults = 50;
                searchListRequest.PageToken = nextPageToken;
                searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
                searchListRequest.PublishedAfterDateTimeOffset = publishedAfter;
                searchListRequest.PublishedBeforeDateTimeOffset = publishedBefore;

                var searchListResponse = await searchListRequest.ExecuteAsync();

                if (searchListResponse != null && searchListResponse.Items != null && searchListResponse.Items.Count > 0)
                {
                    foreach (var searchResult in searchListResponse.Items)
                    {
                        if (searchResult.Id.Kind == "youtube#video")
                        {
                            string videoId = searchResult.Id.VideoId;
                            VideoData video = await GetVideoInfoByIdAsync(videoId);
                            if (video is null)
                                return videos;
                            if (video.Success)
                                videos.Add(video);
                        }
                    }
                    if (searchListResponse.NextPageToken != null)
                    {
                        nextPageToken = searchListResponse.NextPageToken;
                        continue;
                    }
                }
                return videos;
            }
            catch (GoogleApiException ex)
            {
                if (ex.Error.Code == 403 && ex.Error.Message.Contains("quota"))
                {
                    numAPIKey++;
                    if (numAPIKey >= APIKeys.Count)
                    {
                        numAPIKey = APIKeys.Count - 1;
                        MessageBox.Show("Бесплатная квота на сегодня закончилась!", "Ошибка GoogleApi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show(ex.Error.Message, "Ошибка GoogleApi.GetVideos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка GoogleApi.GetVideos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
    public async static Task<bool> CheckYouTubeApiKey(string apiKey)
    {
        using (var httpClient = new HttpClient())
        {
            try
            {
                var requestUrl = $"https://www.googleapis.com/youtube/v3/search?part=snippet&q=YouTube+Data+API&type=video&key={apiKey}";
                var response = await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                    return true;
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden && response.Content.ReadAsStringAsync().Result.Contains("quotaExceeded"))
                    return true;

                return false;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка при проверке API-ключа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
