using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

public class HistoryDataSqlite
{
    public long Id { get; set; }
    public string Date { get; set; }
    public long MinDuration { get; set; }
    public long MaxDuration { get; set; }
}
public class ChannelDataSQLite
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Nickname { get; set; }
    public string IdChannel { get; set; }
    public string CreateDate { get; set; }
    public string DateLimit { get; set; }
    public long NumVideos { get; set; }
    public string LoadNewVideos { get; set; }
}
public class VideoDataSQLite
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string IdVideo { get; set; }
    public string PublishedAt { get; set; }
    public long Duration { get; set; }
    public long NumViews { get; set; }
    public long ChannelId { get; set; }
}
public class VideoSortDataSQLite
{
    public string Title { get; set; } = "";
    public string Id { get; set; } = "";
    public List<int> ThemesId { get; set; } = null;
    public List<int> StoriesId { get; set; } = null;
    public string StartDate { get; set; } = "";
    public string EndDate { get; set; } = "";
    public int MinDuration { get; set; } = -1;
    public int MaxDuration { get; set; } = -1;
    public List<int> ChannelsId { get; set; } = null;
    public int Method { get; set; } = -1;
}
public class ThemeDataSQLite
{
    public long Id { get; set; }
    public string Title { get; set; }
}
public class ContentBlockDataSQLite
{
    public long Id { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string IdContent { get; set; }
}
public class APIKeyDataSQLite
{
    public long Id { get; set; }
    public string Value { get; set; }
}



public class SQLiteManager
{
    private static string _connectionString;

    public static async void Initialize(string databasePath)
    {
        _connectionString = $"Data Source={databasePath}";
        try
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS channels (
                            id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            title TEXT, 
                            nickname TEXT, 
                            id_channel TEXT, 
                            create_date TEXT,
                            date_limit TEXT, 
                            num_videos INTEGER,
                            load_new_videos TEXT
                        );
                
                        CREATE TABLE IF NOT EXISTS videos (
                            id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            title TEXT, 
                            id_video TEXT, 
                            published_at TEXT, 
                            duration INTEGER, 
                            num_views INTEGER, 
                            channel_id INTEGER, 
                            FOREIGN KEY (channel_id) REFERENCES channels(id)
                        );

                        CREATE TABLE IF NOT EXISTS history (
                            id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            date TEXT,
                            min_duration INTEGER,
                            max_duration INTEGER
                        );

                        CREATE TABLE IF NOT EXISTS history_videos (
                            history_id INTEGER, video_id INTEGER,
                            FOREIGN KEY (history_id) REFERENCES history(id),
                            FOREIGN KEY (video_id) REFERENCES videos(id),
                            PRIMARY KEY (history_id, video_id)
                        );
                
                        CREATE TABLE IF NOT EXISTS history_themes (
                            history_id INTEGER, theme_id INTEGER,
                            FOREIGN KEY (history_id) REFERENCES history(id),
                            FOREIGN KEY (theme_id) REFERENCES themes(id),
                            PRIMARY KEY (history_id, theme_id)
                        );

                        CREATE TABLE IF NOT EXISTS themes (
                            id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            title TEXT
                        );

                        CREATE TABLE IF NOT EXISTS themes_videos (
                            theme_id INTEGER, video_id INTEGER,
                            FOREIGN KEY (theme_id) REFERENCES themes(id),
                            FOREIGN KEY (video_id) REFERENCES videos(id),
                            PRIMARY KEY (theme_id, video_id)
                        );
            
                        CREATE TABLE IF NOT EXISTS key_words (
                            id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            word TEXT, 
                            theme_id INTEGER,  
                            FOREIGN KEY (theme_id) REFERENCES themes(id)
                        );

                        CREATE TABLE IF NOT EXISTS black_list (
                            id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            type TEXT,
                            title TEXT, 
                            id_content TEXT
                        );

                        CREATE TABLE IF NOT EXISTS parameters (
                            id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            name TEXT,
                            value TEXT
                        );

                        CREATE TABLE IF NOT EXISTS api_keys (
                            id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            value TEXT
                        );

                        INSERT INTO parameters (name, value)
                        SELECT 'LAST_RELOAD_DATE', '2021-08-01T20:19:56Z'
                        WHERE NOT EXISTS (
	                        SELECT 1
	                        FROM parameters
	                        WHERE name = 'LAST_RELOAD_DATE'
                        );

                        INSERT INTO parameters (name, value)
                        SELECT 'FREQUENCY_RELOADED_HOURS', '24'
                        WHERE NOT EXISTS (
	                        SELECT 1
	                        FROM parameters
	                        WHERE name = 'FREQUENCY_RELOADED_HOURS'
                        );
                    ";
                    await command.ExecuteNonQueryAsync();
                }
                connection.Close();
            }
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при инициализации базы данных: {databasePath}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при инициализации базы данных: {databasePath}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        
    }


    //Лучше вынести все в отдельные Repository

    // PARAMETERS
    public static async Task<int?> UpdateValueParameter(string parameterStr, string value)
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"
                        UPDATE parameters SET value = '{value}' WHERE name = '{parameterStr}'
                    ";

                    object res = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(res);

                    if (result == 0)
                        MessageBox.Show($"Ошибка при изменении параметра: {parameterStr}\nПараметр {parameterStr} не найден!", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при изменении параметра: {parameterStr}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при изменении параметра: {parameterStr}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    private static async Task<object> GetValueParameter(string parameterStr)
    {
        try
        {
            object result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"
                        SELECT value FROM parameters WHERE name = '{parameterStr}'
                    ";

                    result = await command.ExecuteScalarAsync();
                    if (result is null)
                    {
                        MessageBox.Show($"Ошибка при получении параметра: {parameterStr}\nПараметр {parameterStr} не найден!", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении параметра: {parameterStr}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении параметра: {parameterStr}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<string> GetValueStringParameters(string parameterStr)
    {
        object parameter = await GetValueParameter(parameterStr);

        if (parameter != null)
            return parameter.ToString();
        return null;
    }
    public static async Task<int?> GetValueIntParameters(string parameterStr)
    {
        object parameter = await GetValueParameter(parameterStr);

        if (parameter != null)
            return Convert.ToInt32(parameter);
        return null;
    }
    
    // CHANNELS
    public static async Task<int?> GetCountChannels(string idChannel = null)
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT COUNT(*) FROM channels
                    ";
                    if (idChannel != null)
                        command.CommandText += $" WHERE id_channel = '{idChannel}'";

                    object res = await command.ExecuteScalarAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении количества каналов\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении количества каналов\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<List<ChannelDataSQLite>> GetChannels(int? id = null, int? limit = null, int? offset = null)
    {
        try
        {
            List<ChannelDataSQLite> result = new List<ChannelDataSQLite>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT * FROM channels
                    ";
                    if (id != null)
                        command.CommandText += $" WHERE id = '{id}'";

                    if (limit != null)
                        command.CommandText += $" LIMIT {limit}";

                    if (offset != null)
                        command.CommandText += $" OFFSET {offset}";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new ChannelDataSQLite
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Title = reader.GetString(reader.GetOrdinal("title")),
                                Nickname = reader.GetString(reader.GetOrdinal("nickname")),
                                IdChannel = reader.GetString(reader.GetOrdinal("id_channel")),
                                CreateDate = reader.GetString(reader.GetOrdinal("create_date")),
                                DateLimit = reader.GetString(reader.GetOrdinal("date_limit")),
                                NumVideos = reader.GetInt32(reader.GetOrdinal("num_videos")),
                                LoadNewVideos = reader.GetString(reader.GetOrdinal("load_new_videos")),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении каналов\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении каналов\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<int?> UpdateChannel(ChannelDataSQLite channel)
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"
                        UPDATE channels 
                        SET title = '{channel.Title.Replace("'", "")}', 
                            nickname = '{channel.Nickname.Replace("'", "")}', 
                            create_date = '{channel.CreateDate}', 
                            date_limit = '{channel.DateLimit}', 
                            num_videos = {channel.NumVideos}
                        WHERE id = {channel.Id}
                    ";

                    object res = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при измении канала: {channel.Title}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при измении канала: {channel.Title}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<int?> DeleteChannel(ChannelDataSQLite channel)
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"
                        DELETE FROM history_videos WHERE video_id = (SELECT id FROM videos WHERE channel_id = '{channel.Id}');

                        DELETE FROM themes_videos WHERE video_id = (SELECT id FROM videos WHERE channel_id = '{channel.Id}');
                        
                        DELETE FROM videos WHERE channel_id = '{channel.Id}';

                        DELETE FROM channels WHERE id = '{channel.Id}';
                    ";

                    object res = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при удалении канала: {channel.Title}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при удалении канала: {channel.Title}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }



    // VIDEOS
    public static async Task<int?> GetCountVideos(VideoSortDataSQLite videoSortParams = null, HistoryDataSqlite history = null)
    {
        try
        {
            int result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "SELECT COUNT(*) FROM videos";

                    if (videoSortParams != null)
                    {
                        List<string> commandTextWhere = new List<string>();

                        if (videoSortParams.ThemesId != null && videoSortParams.ThemesId.Count != 0)
                        {
                            string list_themes_id = "(" + string.Join(", ", videoSortParams.ThemesId) + ")";
                            command.CommandText += $" JOIN themes_videos ON themes_videos.video_id = videos.id AND themes_videos.theme_id IN {list_themes_id}";
                        }
                        if (videoSortParams.StoriesId != null && videoSortParams.StoriesId.Count != 0)
                        {
                            string list_stories_id = "(" + string.Join(", ", videoSortParams.StoriesId) + ")";
                            command.CommandText += $" JOIN history_videos ON history_videos.video_id = videos.id AND history_videos.history_id IN {list_stories_id}";
                        }

                        if (history != null)
                            commandTextWhere.Add($"(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id AND history_videos.history_id = {history.Id}) == 0");

                        if (videoSortParams.Title != "")
                            commandTextWhere.Add($"lower(title) LIKE '%{videoSortParams.Title.Replace("'", "")}%'");
                        if (videoSortParams.Id != "")
                            commandTextWhere.Add($"lower(id_video) LIKE '%{videoSortParams.Id}%'");
                        if (videoSortParams.StartDate != "" && videoSortParams.EndDate != "")
                            commandTextWhere.Add($"published_at BETWEEN '{videoSortParams.StartDate}' AND '{videoSortParams.EndDate}'");
                        if (videoSortParams.MinDuration != -1)
                            commandTextWhere.Add($"duration > {videoSortParams.MinDuration}");
                        if (videoSortParams.MaxDuration != -1)
                            commandTextWhere.Add($"duration < {videoSortParams.MaxDuration}");
                        if (videoSortParams.ChannelsId != null && videoSortParams.ChannelsId.Count != 0)
                            commandTextWhere.Add($"channel_id IN ({string.Join(", ", videoSortParams.ChannelsId)})");
                        switch (videoSortParams.Method)
                        {
                            case -1:
                                break;
                            case 0:
                                commandTextWhere.Add("(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id) != 0 ORDER BY published_at DESC");
                                break;
                            case 1:
                                commandTextWhere.Add("(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id) != 0 ORDER BY published_at ASC");
                                break;
                            case 2:
                                commandTextWhere.Add("(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id) != 0 ORDER BY RANDOM()");
                                break;
                            case 3:
                                commandTextWhere.Add("(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id) == 0 ORDER BY published_at DESC");
                                break;
                            case 4:
                                commandTextWhere.Add("(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id) == 0 ORDER BY published_at ASC");
                                break;
                            case 5:
                                commandTextWhere.Add("(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id) == 0 ORDER BY RANDOM()");
                                break;
                            default:
                                break;
                        }

                        if (commandTextWhere.Count != 0)
                            command.CommandText += " WHERE " + string.Join(" AND ", commandTextWhere);

                        switch (videoSortParams.Method)
                        {
                            case 6:
                                command.CommandText += " ORDER BY published_at DESC";
                                break;
                            case 7:
                                command.CommandText += " ORDER BY published_at ASC";
                                break;
                            case 8:
                                command.CommandText += " ORDER BY RANDOM()";
                                break;
                            default:
                                break;
                        }

                    }

                    object res = await command.ExecuteScalarAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении количества видео\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении количества видео\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<List<VideoDataSQLite>> GetVideos(VideoSortDataSQLite videoSortParams = null, int? limit = null, int? offset = null, HistoryDataSqlite history = null)
    {
        try
        {
            List<VideoDataSQLite> result = new List<VideoDataSQLite>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT * FROM videos
                    ";

                    if(videoSortParams != null)
                    {
                        List<string> commandTextWhere = new List<string>();

                        if (videoSortParams.ThemesId != null && videoSortParams.ThemesId.Count != 0)
                        {
                            string list_themes_id = "(" + string.Join(", ", videoSortParams.ThemesId) + ")";
                            command.CommandText += $" JOIN themes_videos ON themes_videos.video_id = videos.id AND themes_videos.theme_id IN {list_themes_id}";
                        }
                        if (videoSortParams.StoriesId != null && videoSortParams.StoriesId.Count != 0)
                        {
                            string list_stories_id = "(" + string.Join(", ", videoSortParams.StoriesId) + ")";
                            command.CommandText += $" JOIN history_videos ON history_videos.video_id = videos.id AND history_videos.history_id IN {list_stories_id}";
                        }

                        if (history != null)
                            commandTextWhere.Add($"(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id AND history_videos.history_id = {history.Id}) == 0");

                        if (videoSortParams.Title != "")
                            commandTextWhere.Add($"lower(title) LIKE '%{videoSortParams.Title.Replace("'", "")}%'");
                        if (videoSortParams.Id != "")
                            commandTextWhere.Add($"lower(id_video) LIKE '%{videoSortParams.Id}%'");
                        if (videoSortParams.StartDate != "" && videoSortParams.EndDate != "")
                            commandTextWhere.Add($"published_at BETWEEN '{videoSortParams.StartDate}' AND '{videoSortParams.EndDate}'");
                        if (videoSortParams.MinDuration != -1)
                            commandTextWhere.Add($"duration > {videoSortParams.MinDuration}");
                        if (videoSortParams.MaxDuration != -1)
                            commandTextWhere.Add($"duration < {videoSortParams.MaxDuration}");
                        if (videoSortParams.ChannelsId != null && videoSortParams.ChannelsId.Count != 0)
                            commandTextWhere.Add($"channel_id IN ({string.Join(", ", videoSortParams.ChannelsId)})");
                        switch (videoSortParams.Method)
                        {
                            case -1:
                                break;
                            case 0:
                                commandTextWhere.Add("(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id) != 0 ORDER BY published_at DESC");
                                break;
                            case 1:
                                commandTextWhere.Add("(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id) != 0 ORDER BY published_at ASC");
                                break;
                            case 2:
                                commandTextWhere.Add("(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id) != 0 ORDER BY RANDOM()");
                                break;
                            case 3:
                                commandTextWhere.Add("(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id) == 0 ORDER BY published_at DESC");
                                break;
                            case 4:
                                commandTextWhere.Add("(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id) == 0 ORDER BY published_at ASC");
                                break;
                            case 5:
                                commandTextWhere.Add("(SELECT COUNT(*) FROM history_videos WHERE history_videos.video_id = videos.id) == 0 ORDER BY RANDOM()");
                                break;
                            default:
                                break;
                        }

                        if (commandTextWhere.Count != 0)
                            command.CommandText += " WHERE " + string.Join(" AND ", commandTextWhere);

                        switch (videoSortParams.Method)
                        {
                            case 6:
                                command.CommandText += " ORDER BY published_at DESC";
                                break;
                            case 7:
                                command.CommandText += " ORDER BY published_at ASC";
                                break;
                            case 8:
                                command.CommandText += " ORDER BY RANDOM()";
                                break;
                            default:
                                break;
                        }
                    }

                    if (limit != null)
                        command.CommandText += $" LIMIT {limit}";

                    if (offset != null)
                        command.CommandText += $" OFFSET {offset}";

                    //MessageBox.Show(command.CommandText);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new VideoDataSQLite
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Title = reader.GetString(reader.GetOrdinal("title")),
                                IdVideo = reader.GetString(reader.GetOrdinal("id_video")),
                                PublishedAt = reader.GetString(reader.GetOrdinal("published_at")),
                                Duration = reader.GetInt32(reader.GetOrdinal("duration")),
                                NumViews = reader.GetInt32(reader.GetOrdinal("num_views")),
                                ChannelId = reader.GetInt32(reader.GetOrdinal("channel_id")),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении видео\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении видео\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<int?> InsertVideo(VideoDataSQLite video)
    {
        try
        {
            int? count_videos = await GetCountVideos(videoSortParams: new VideoSortDataSQLite() { Id = video.IdVideo});
            if (!count_videos.HasValue)
                return null;
            if (count_videos.Value != 0)
                return 0;

            int? countVIdeoInBlackList = await GetCountContentsInBlackList(video.IdVideo);
            if (!countVIdeoInBlackList.HasValue)
                return null;
            if (countVIdeoInBlackList.Value != 0)
                return 0;

            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"
                        INSERT INTO videos (title, id_video, published_at, duration, num_views, channel_id)
                        VALUES ('{video.Title.Replace("'", "")}', 
                            '{video.IdVideo}', 
                            '{video.PublishedAt}', 
                            {video.Duration}, 
                            {video.NumViews}, 
                            {video.ChannelId})
                    ";

                    object res = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при добавлении видео: {video.Title}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при добавлении видео: {video.Title}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<int?> DeleteVideo(VideoDataSQLite video)
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"
                        DELETE FROM history_videos WHERE video_id = '{video.Id}';

                        DELETE FROM themes_videos WHERE video_id = '{video.Id}';
                        
                        DELETE FROM videos WHERE id = '{video.Id}';
                    ";

                    object res = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при удалении видео: {video.Title}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при удалении видео: {video.Title}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<List<string>> GetThemesInVideo(VideoDataSQLite video)
    {
        try
        {
            List<string> result = new List<string>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT themes.title FROM themes 
                        JOIN themes_videos ON themes.id = themes_videos.theme_id AND themes_videos.video_id = @video_id
                    ";

                    command.Parameters.AddWithValue("@video_id", video.Id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(reader.GetString(reader.GetOrdinal("title")));
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении темы видео - {video.Title}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении темы видео - {video.Title}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    // BLACK LIST
    public static async Task<int?> GetCountContentsInBlackList(string idContent = null)
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT COUNT(*) FROM black_list
                    ";
                    if (idContent != null)
                        command.CommandText += $" WHERE id_content = '{idContent}'";

                    object res = await command.ExecuteScalarAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении количества заблокированного контента\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении количества заблокированного контента\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<List<ContentBlockDataSQLite>> GetContentsInBlackList(int? limit = null, int? offset = null)
    {
        try
        {
            List<ContentBlockDataSQLite> result = new List<ContentBlockDataSQLite>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT * FROM black_list
                    ";
                    if (limit != null)
                    {
                        command.CommandText += " LIMIT @limit";
                        command.Parameters.AddWithValue("@limit", limit);
                    }
                    if (offset != null)
                    {
                        command.CommandText += " OFFSET @offset";
                        command.Parameters.AddWithValue("@offset", offset);
                    }
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new ContentBlockDataSQLite
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Type = reader.GetString(reader.GetOrdinal("type")),
                                Title = reader.GetString(reader.GetOrdinal("title")),
                                IdContent = reader.GetString(reader.GetOrdinal("id_content"))
                            });
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении заблокированного контента\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении заблокированного контента\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<int?> InsertContentInBlackList(ContentBlockDataSQLite content)
    {
        try
        {
            int? countVIdeoInBlackList = await GetCountContentsInBlackList(content.IdContent);
            if (!countVIdeoInBlackList.HasValue)
                return null;
            if (countVIdeoInBlackList.Value != 0)
                return 0;

            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"
                        INSERT INTO black_list (type, title, id_content)
                        VALUES ('{content.Type}',
                            '{content.Title.Replace("'", "")}',
                            '{content.IdContent}')
                    ";

                    object res = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при добавлении контента в черный список: {content.Title}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при добавлении контента в черный список: {content.Title}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    // SEARCH VIDEOS
    public static async Task<int?> GetCountSearchVideos(HistoryDataSqlite history = null)
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT COUNT(*) FROM history_videos
                    ";
                    if (history != null)
                    {
                        command.CommandText += " WHERE history_id = @history_id";
                        command.Parameters.AddWithValue("@history_id", history.Id);
                    }

                    object res = await command.ExecuteScalarAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении количества видео в истории {history.Date}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении количества видео в истории {history.Date}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<List<VideoDataSQLite>> GetSearchVideos(HistoryDataSqlite history, int? limit = null, int? offset = null)
    {
        try
        {
            List<VideoDataSQLite> result = new List<VideoDataSQLite>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT * FROM videos 
                        JOIN history_videos ON videos.id = history_videos.video_id AND history_videos.history_id = @history_id
                    ";
                    command.Parameters.AddWithValue("@history_id", history.Id);
                    if (limit != null)
                    {
                        command.CommandText += " LIMIT @limit";
                        command.Parameters.AddWithValue("@limit", limit);
                    }
                    if (offset != null)
                    {
                        command.CommandText += " OFFSET @offset";
                        command.Parameters.AddWithValue("@offset", offset);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new VideoDataSQLite 
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Title = reader.GetString(reader.GetOrdinal("title")),
                                IdVideo = reader.GetString(reader.GetOrdinal("id_video")),
                                PublishedAt = reader.GetString(reader.GetOrdinal("published_at")),
                                Duration = reader.GetInt32(reader.GetOrdinal("duration")),
                                NumViews = reader.GetInt32(reader.GetOrdinal("num_views")),
                                ChannelId = reader.GetInt32(reader.GetOrdinal("channel_id")),
                            });
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении видео из истории {history.Date}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении видео из истории {history.Date}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    // HISTORY
    public static async Task<int?> GetCountHistory()
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT COUNT(*) FROM history
                    ";

                    object res = await command.ExecuteScalarAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении количества историй запросов\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении количества историй запросов\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<List<HistoryDataSqlite>> GetHistory(bool last_history = false, int? limit = null, int? offset = null)
    {
        try
        {
            List<HistoryDataSqlite> result = new List<HistoryDataSqlite>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT * FROM history
                    ";
                    if (last_history)
                    {
                        command.CommandText += " ORDER BY id DESC LIMIT 1";
                    }
                    if (limit != null)
                    {
                        command.CommandText += " LIMIT @limit";
                        command.Parameters.AddWithValue("@limit", limit);
                    }
                    if (offset != null)
                    {
                        command.CommandText += " OFFSET @offset";
                        command.Parameters.AddWithValue("@offset", offset);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new HistoryDataSqlite
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Date = reader.GetString(reader.GetOrdinal("date")),
                                MinDuration = reader.GetInt32(reader.GetOrdinal("min_duration")),
                                MaxDuration = reader.GetInt32(reader.GetOrdinal("max_duration"))
                            });
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении историй запросов\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении историй запросов\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<List<string>> GetThemesInHistory(HistoryDataSqlite history)
    {
        try
        {
            List<string> result = new List<string>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT title FROM themes 
                        JOIN history_themes ON themes.id = history_themes.theme_id AND history_themes.history_id = @history_id
                    ";

                    command.Parameters.AddWithValue("@history_id", history.Id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(reader.GetString(reader.GetOrdinal("title")));
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении тем в истории {history.Date}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении тем в истории {history.Date}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<int?> InsertHistory(HistoryDataSqlite history)
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"
                        INSERT INTO history (date, min_duration, max_duration)
                        VALUES ('{history.Date}', {history.MinDuration}, {history.MaxDuration});
                    ";

                    object res = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при добавлении истории {history.Date}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при добавлении истории {history.Date}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<int?> InsertHistoryThemes(HistoryDataSqlite history, ThemeDataSQLite theme)
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"
                        INSERT INTO history_themes (history_id, theme_id) 
                        VALUES ({history.Id}, {theme.Id});
                    ";

                    object res = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при добавлении тем истории {history.Date}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при добавлении тем истории {history.Date}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<int?> InsertHistoryVideos(HistoryDataSqlite history, VideoDataSQLite video)
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"
                        INSERT INTO history_videos (history_id, video_id)
                        VALUES ({history.Id}, {video.Id});
                    ";

                    object res = await command.ExecuteNonQueryAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при добавлении видео в историю {history.Date}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при добавлении видео в историю {history.Date}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<int?> GetCountHistoryVideos(HistoryDataSqlite history = null, VideoDataSQLite video = null)
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT COUNT(*) FROM history_videos
                    ";
                    if (history != null && video != null)
                    {
                        command.CommandText += $" WHERE history_videos.video_id = {video.Id} AND history_videos.history_id = {history.Id}";
                    }
                    else if (history != null)
                    {
                        command.CommandText += $" WHERE history_videos.history_id = {history.Id}";
                    }
                    else if (video != null)
                    {
                        command.CommandText += $" WHERE history_videos.video_id = {video.Id}";
                    }

                    object res = await command.ExecuteScalarAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении количества историй запросов\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении количества историй запросов\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    // THEMES
    public static async Task<int?> GetCountThemes()
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT COUNT(*) FROM themes
                    ";

                    object res = await command.ExecuteScalarAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении количества тем\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении количества тем\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<List<ThemeDataSQLite>> GetThemes(int? limit = null, int? offset = null)
    {
        try
        {
            List<ThemeDataSQLite> result = new List<ThemeDataSQLite>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT * FROM themes
                    ";
                    if (limit != null)
                    {
                        command.CommandText += " LIMIT @limit";
                        command.Parameters.AddWithValue("@limit", limit);
                    }
                    if (offset != null)
                    {
                        command.CommandText += " OFFSET @offset";
                        command.Parameters.AddWithValue("@offset", offset);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new ThemeDataSQLite
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Title = reader.GetString(reader.GetOrdinal("title"))
                            });
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении тем\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении тем\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<List<string>> GetKeyWordsInTheme(ThemeDataSQLite theme)
    {
        try
        {
            List<string> result = new List<string>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                         SELECT word FROM key_words WHERE theme_id = @theme_id
                    ";

                    command.Parameters.AddWithValue("@theme_id", theme.Id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(reader.GetString(reader.GetOrdinal("word")));
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении ключевых слов темы: {theme.Title}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении ключевых слов темы: {theme.Title}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public static async Task<int?> GetCountVideosInTheme(ThemeDataSQLite theme)
    {
        try
        {
            int? result;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT COUNT(*) FROM themes_videos WHERE theme_id = @theme_id
                    ";
                    command.Parameters.AddWithValue("@theme_id", theme.Id);
                    object res = await command.ExecuteScalarAsync();
                    result = Convert.ToInt32(res);
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении количества видео в теме {theme.Title}\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении количества видео в теме {theme.Title}\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    // API KEYS
    public static async Task<List<APIKeyDataSQLite>> GetAPIKeys()
    {
        try
        {
            List<APIKeyDataSQLite> result = new List<APIKeyDataSQLite>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
                        SELECT * FROM api_keys
                    ";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new APIKeyDataSQLite
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Value = reader.GetString(reader.GetOrdinal("value"))
                            });
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }
        catch (SQLiteException ex)
        {
            MessageBox.Show($"Ошибка при получении API ключей\nКод: {ex.ErrorCode}\nСообщение: {ex.Message}", "Ошибка SQLite", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при получении API ключей\nСообщение: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    // ........






    public static async Task QueryExecuteNonQuery(string queryString)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(queryString, connection))
            {
                await command.ExecuteNonQueryAsync();
            }
            connection.Close();
        }
    }
    public static async Task<object> QueryScalar(string queryString)
    {
        object result = null;
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(queryString, connection))
            {
                result = await command.ExecuteScalarAsync();
            }
            connection.Close();
        }
        return result;
    }
    public static async Task<object[][]> QueryReader(string queryString)
    {
        List<object[]> result = new List<object[]>();

        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(queryString, connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        object[] values = new object[reader.FieldCount];
                        reader.GetValues(values);
                        result.Add(values);
                    }
                }
            }
            connection.Close();
        }

        return result.ToArray();
    }
}