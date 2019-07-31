using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.YouTube.v3.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Youtube.NET;

namespace YoutubeDescriptionReplacer
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Transformations = JsonConvert.DeserializeObject<List<AdditionalMethods.StringTransformation>>(File.ReadAllText(TransformationPath));
            UpdateTransformationsInfo();
        }

        public Uploads Uploads { get; private set; }
        public List<VideoUpdatable> Videos { get; private set; }

        public static string GetLogFilePath()
        {

            var baseDir = AppContext.BaseDirectory;
            var file = Path.Combine(baseDir, "ProgramLog.txt");
            return file;
        }

        public static void EnsureFileExists()
        {
            var path = GetLogFilePath();
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public void LogMessage(string message)
        {
            lock (locker)
            {
                EnsureFileExists();
                message = $"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}] {message}\r\n";

                HistoryView.Invoke((MethodInvoker)delegate
                {
                    // Running on the UI thread
                    HistoryView.AppendText(message);
                });
                File.AppendAllText(GetLogFilePath(), message);
            }
        }

        static object locker = new object();

        private async void SelectSecrets_Click(object sender, EventArgs e)
        {
            var servicer = new Youtube.NET.Servicer();
            using (var dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                dialog.CheckFileExists = true;
                dialog.Filter = "json|*.json";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var service = await servicer.CreateService(dialog.FileName);
                    Uploads = new Uploads(service);
                    GetVideos.Enabled = true;
                    LoadBackup.Enabled = true;
                    LogMessage("Secrets File Loaded.");
                }
            }
        }

        public static string NewLineFix(string input)
        {
            return Regex.Replace(input, "(?<!\r)\n", "\r\n");
        }

        public class VideoUpdatable
        {
            public VideoUpdatable(Video video)
            {
                video.Snippet.Description = NewLineFix(video.Snippet.Description);
                Video = video;
                DisplayDescription = video.Snippet.Description;
            }

            public Video Video { get; set; }
            public string DisplayDescription { get; set; }

            [JsonIgnore]
            public bool DiscriptionChanged => !DisplayDescription.Equals(Video.Snippet.Description);
        }

        public void BackupVideos(string prefix)
        {
            var backup = JsonConvert.SerializeObject(Videos, Formatting.Indented);
            string fileName = $"{prefix}-Backup-{DateTime.UtcNow:yyyy-MM-dd HH-mm-ss fff}.json";
            var backupFile = Path.Combine(AppContext.BaseDirectory, fileName);
            File.WriteAllText(backupFile, backup);
            LogMessage($"Backup saved to {fileName}");
        }

        private async void GetVideos_ClickAsync(object sender, EventArgs e)
        {
            LogMessage("Loading videos...");
            GetVideos.Enabled = false;
            await Task.Run(async () =>
            {
                var uploads = await Uploads.GetMyVideos();
                Videos = uploads.Select(x => new VideoUpdatable(x)).ToList();
            });

            LogMessage($"{Videos.Count} Videos Loaded from channel.");
            MessageBox.Show("Videos loaded.");
            VideoInformation.Text = NewLineFix(string.Join("\n----------\n", Videos.Select(x => $"Title: {x.Video.Snippet.Title} ({x.Video.Id})\n----------\n{x.DisplayDescription}")));

            TestUpdate.Enabled = true;
            SearchVideosButton.Enabled = true;
            Domains.Enabled = true;
            ResolveDomains.Enabled = true;
            AdflyResolve.Enabled = true;
            BackupVideos("Load");
        }

        private void DescriptionUpdateOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            MethodAddButton.Enabled = true;
            if (DescriptionUpdateOptions.SelectedItem.ToString().Equals("Append"))
            {
                SecondaryValueBox.Enabled = false;
                PrimaryValueBox.Enabled = true;
                IgnoreCaseCheck.Enabled = false;
            }
            else if (DescriptionUpdateOptions.SelectedItem.ToString().Equals("Prepend"))
            {
                SecondaryValueBox.Enabled = false;
                PrimaryValueBox.Enabled = true;
                IgnoreCaseCheck.Enabled = false;
            }
            else if (DescriptionUpdateOptions.SelectedItem.ToString().Equals("Replace"))
            {
                SecondaryValueBox.Enabled = true;
                PrimaryValueBox.Enabled = true;
                IgnoreCaseCheck.Enabled = true;
            }
            else if (DescriptionUpdateOptions.SelectedItem.ToString().Equals("Remove"))
            {
                SecondaryValueBox.Enabled = false;
                PrimaryValueBox.Enabled = true;
                IgnoreCaseCheck.Enabled = true;
            }
        }

        private void TransformationClear_Click(object sender, EventArgs e)
        {
            Transformations = new List<AdditionalMethods.StringTransformation>();
            TransformationView.Text = "Transformations:";
            File.WriteAllText(TransformationPath, JsonConvert.SerializeObject(Transformations));
            LogMessage("Reset Transformations list.");
        }

        private List<AdditionalMethods.StringTransformation> Transformations { get; set; }
        public string TransformationPath { get; set; } = Path.Combine(AppContext.BaseDirectory, "Transformations.json");

        private void MethodAddButton_Click(object sender, EventArgs e)
        {
            var transform = new AdditionalMethods.StringTransformation();
            if (DescriptionUpdateOptions.SelectedItem.ToString().Equals("Append"))
            {
                transform.Method = AdditionalMethods.StringTransformation.TransformationMethod.Append;
                transform.PrimaryValue = PrimaryValueBox.Text;
            }
            else if (DescriptionUpdateOptions.SelectedItem.ToString().Equals("Prepend"))
            {
                transform.Method = AdditionalMethods.StringTransformation.TransformationMethod.Prepend;
                transform.PrimaryValue = PrimaryValueBox.Text;
            }
            else if (DescriptionUpdateOptions.SelectedItem.ToString().Equals("Replace"))
            {
                transform.Method = AdditionalMethods.StringTransformation.TransformationMethod.Replace;
                transform.IgnoreCase = IgnoreCaseCheck.Checked;
                transform.PrimaryValue = PrimaryValueBox.Text;
                transform.SecondaryValue = SecondaryValueBox.Text;
            }
            else if (DescriptionUpdateOptions.SelectedItem.ToString().Equals("Remove"))
            {
                transform.Method = AdditionalMethods.StringTransformation.TransformationMethod.Remove;
                transform.IgnoreCase = IgnoreCaseCheck.Checked;
                transform.PrimaryValue = PrimaryValueBox.Text;
            }
            Transformations.Add(transform);
            File.WriteAllText(TransformationPath, JsonConvert.SerializeObject(Transformations, Formatting.Indented));
            LogMessage($"Added new {DescriptionUpdateOptions.SelectedItem} Transformation");
            UpdateTransformationsInfo();
        }

        private void UpdateTransformationsInfo()
        {
            var text = Transformations.Select(x =>
            {
                string additional = "";
                if (x.Method == AdditionalMethods.StringTransformation.TransformationMethod.Replace)
                {
                    additional += $"\nReplaced with: {x.SecondaryValue}\nIgnore Case: {x.IgnoreCase}";
                }
                else if (x.Method == AdditionalMethods.StringTransformation.TransformationMethod.Remove)
                {
                    additional += $"\nIgnore Case: {x.IgnoreCase}";
                }

                return NewLineFix($"{x.Method}:\n{x.PrimaryValue}{additional}");
            }).ToArray();
            if (!text.Any())
            {
                TransformationView.Text = "Transformations:";
                return;
            }
            TransformationView.Text = NewLineFix(string.Join("\r\n-----\r\n", text));
        }

        public class TrackedTransformation
        {
            public AdditionalMethods.StringTransformation Method { get; set; }
            public int TransformCount { get; set; } = 0;
        }

        private void TestUpdate_Click(object sender, EventArgs e)
        {
            Indexes.Clear();
            indexesLength = 0;
            CurrentSearchIndex = 0;
            var transforms = Transformations.Select(x => new TrackedTransformation
            {
                Method = x
            }).ToArray();
            int updatedVideos = 0;
            foreach (var video in Videos)
            {
                bool updated = false;
                foreach (var transformation in transforms)
                {
                    var transformResult = AdditionalMethods.DoReplacements(video.DisplayDescription, transformation.Method);
                    if (transformResult.Item2)
                    {
                        updated = true;
                        video.DisplayDescription = transformResult.Item4;
                        transformation.TransformCount += transformResult.Item3;
                    }
                }

                if (updated)
                {
                    updatedVideos++;
                }
            }

            UpdateDescriptions.Text = $"Upload {updatedVideos} Changes";

            var response = $"Test Updated {updatedVideos} videos\r\n";
            foreach (var transform in transforms)
            {
                if (transform.Method.Method == AdditionalMethods.StringTransformation.TransformationMethod.Replace)
                {
                    response += $"Replaced {transform.TransformCount} occurrences of {transform.Method.PrimaryValue} with {transform.Method.SecondaryValue}\r\n";
                }
                else if (transform.Method.Method == AdditionalMethods.StringTransformation.TransformationMethod.Remove)
                {
                    response += $"Removed {transform.TransformCount} occurrences of {transform.Method.PrimaryValue}\r\n";
                }
            }
            VideoInformation.Text = NewLineFix(string.Join("\r\n----------\r\n", Videos.Select(x => $"Title: {x.Video.Snippet.Title} ({x.Video.Id})\r\n----------\r\n{x.DisplayDescription}")));
            LogMessage("Tested Descriptions update.");
            LogMessage(response);
            UpdateDescriptionTextChange();
        }

        public void UpdateDescriptionTextChange()
        {
            UpdateDescriptions.Text = $"Update {Videos.Count(x => x.DiscriptionChanged)} Videos";
            UpdateDescriptions.Enabled = true;
        }

        #region Indexing
        public List<int> Indexes = new List<int>();
        public int CurrentSearchIndex = 0;
        public int indexesLength = -1;

        public static IEnumerable<int> AllIndexesOf(string str, string value, bool ignoreCase) {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");

            
            for (int index = 0;; index += value.Length) {
                index = ignoreCase ? str.IndexOf(value, index, StringComparison.InvariantCultureIgnoreCase) : str.IndexOf(value, index);
                if (index == -1)
                    break;
                yield return index;
            }
        }
        #endregion

        private async void UpdateDescriptions_Click(object sender, EventArgs e)
        {
            UpdateDescriptions.Enabled = false;
            var transforms = Transformations.Select(x => new TrackedTransformation
            {
                Method = x
            }).ToArray();
            int updatedVideos = 0;
            int softError = 0;
            int index = 0;
            bool hardError = false;
            int updatable = Videos.Count(x => x.DiscriptionChanged);
            foreach (var video in Videos)
            {
                index++;
                if (!video.DiscriptionChanged) continue;

                try
                {
                    updatedVideos++;
                    await Uploads.UpdateDescription(video.Video, video.DisplayDescription);
                }
                catch (Exception ex)
                {
                    LogMessage(ex.ToString());
                    if (ex is Google.GoogleApiException gEx)
                    {
                        LogMessage($"{gEx.Error} on video: {video.Video.Snippet.Title} ({video.Video.Id})");

                        if (gEx.Error.Code == 400)
                        {
                            softError++;
                        }
                        else
                        {
                            hardError = true;
                            break;
                        }
                    }
                }

                UpdateProgress.Value = (int)(((double)index / Videos.Count) * 100);
                ProgressText.Text = $"{index}/{Videos.Count} Videos Checked. {updatedVideos}/{updatable} Updated. {softError} Issues detected.";
            }

            UpdateProgress.Value = 100;
            if (!hardError)
            {
                ProgressText.Text = $"{Videos.Count}/{Videos.Count} Videos Checked. {updatedVideos}/{updatable} Updated. {softError} Issues detected.";
            }
            else
            {
                ProgressText.Text = $"{Videos.Count}/{Videos.Count} Videos Checked. {updatedVideos}/{updatable} Updated. {softError} Issues detected. Fatal Error Encountered.";
            }
            var response = $"Updated {updatedVideos} videos\r\n";
            foreach (var transform in transforms)
            {
                if (transform.Method.Method == AdditionalMethods.StringTransformation.TransformationMethod.Replace)
                {
                    response += $"Replaced {transform.TransformCount} occurrences of {transform.Method.PrimaryValue} with {transform.Method.SecondaryValue}\r\n";
                }
                else if (transform.Method.Method == AdditionalMethods.StringTransformation.TransformationMethod.Remove)
                {
                    response += $"Removed {transform.TransformCount} occurrences of {transform.Method.PrimaryValue}\r\n";
                }
            }

            VideoInformation.Text = NewLineFix(string.Join("\r\n----------\r\n", Videos.Select(x => $"Title: {x.Video.Snippet.Title} ({x.Video.Id})\r\n----------\r\n{x.DisplayDescription}")));
            LogMessage("Descriptions update.");
            LogMessage(response);
            UpdateDescriptions.Enabled = true;
            BackupVideos("Update");
        }

        private async void ResolveDomains_Click(object sender, EventArgs e)
        {
            var results = new List<(string, string)>();
            var results404 = new List<(string, string)>();
            await Task.Run(async () =>
            {
                foreach (var line in Domains.Lines)
                {
                    foreach (var video in Videos)
                    {
                        var desc = video.DisplayDescription;
                        if (desc.IndexOf(line, StringComparison.InvariantCultureIgnoreCase) != -1)
                        {
                            var matches = Regex.Matches(desc, $@"(https?:\/\/)?(w{3}\.)?{Regex.Escape(line)}(\S+)?");
                            foreach (Match match in matches)
                            {
                                if (results.Any(x => x.Item1.Equals(match.Value)) || results404.Any(x => x.Item1.Equals(match.Value)))
                                {
                                    continue;
                                }

                                HttpWebRequest request = (HttpWebRequest)(WebRequest.Create(match.Value));
                                try
                                {
                                    using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                                    {

                                        string uriString = response.ResponseUri.AbsoluteUri;
                                        results.Add((match.Value, uriString));
                                        LogMessage($"Resolved: {match.Value} to {uriString}");
                                    }
                                }
                                catch (WebException we)
                                {
                                    HttpWebResponse errorResponse = we.Response as HttpWebResponse;
                                    if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                                    {
                                        LogMessage($"404 Error for: {match.Value} Resolved to: {we.Response.ResponseUri.AbsoluteUri}");
                                        results404.Add((match.Value, we.Response.ResponseUri.AbsoluteUri));
                                    }
                                    else
                                    {
                                        LogMessage($"Web error for: {match.Value} {errorResponse.StatusCode}");
                                    }
                                }

                            }

                        }
                    }
                }
            });

            LogMessage("Finished resolving domains.");
            var result = MessageBox.Show("Would you like to replace these domains with their resolved values?", "This will be applied when you choose to update descriptions by pressing the \"Update Video Descriptions\" button. Note: if their are 404 domains another message box will be displayed asking for input on that.", MessageBoxButtons.YesNo);
            var replace404 = new List<(string, string)>();

            if (results404.Any())
            {
                MessageBox.Show("There were 404 results detected, please select whether you would like to replace the shortlinks with their resolved values.");
                foreach (var tuple in results404)
                {
                    var response = MessageBox.Show($"Would you like to replace {tuple.Item1} with {tuple.Item2} ?", "", MessageBoxButtons.YesNo);
                    if (response != DialogResult.Yes)
                    {
                        replace404.Add(tuple);
                    }
                }
            }

            if (result == DialogResult.Yes)
            {
                foreach (var video in Videos)
                {
                    foreach (var resolve in results)
                    {
                        video.DisplayDescription = video.DisplayDescription.Replace(resolve.Item1, resolve.Item2, true).Item2;
                    }

                    foreach (var result404 in replace404)
                    {
                        video.DisplayDescription = video.DisplayDescription.Replace(result404.Item1, result404.Item2, true).Item2;
                    }
                }

                VideoInformation.Text = NewLineFix(string.Join("\r\n----------\r\n", Videos.Select(x => $"Title: {x.Video.Snippet.Title} ({x.Video.Id})\r\n----------\r\n{x.DisplayDescription}")));
            }
            
            UpdateDescriptionTextChange();
        }

        public static IEnumerable<List<T>> SplitList<T>(IEnumerable<T> list, int groupSize = 30)
        {
            var splitList = new List<List<T>>();
            for (var i = 0; i < list.Count(); i += groupSize)
            {
                splitList.Add(list.Skip(i).Take(groupSize).ToList());
                //yield return list.Skip(i).Take(groupSize);
            }

            return splitList;
        }

        //Returns in the format List<(resolve_link, adfly_link)>
        public async Task<(List<(string, string)>, List<string>)> ADFLYGet(string userId, string apiKey, string[] urls)
        {
            var pairs = new List<(string, string)>();
            using (var client = new HttpClient())
            {
                //Split urls into groups of 20 as adf.ly restricts to 20 per request
                foreach (var urlGroup in SplitList(urls, 20).ToArray())
                {
                    var baseUrl = $"http://api.adf.ly/v1/expand?_user_id={userId}&_api_key={apiKey}";

                    for (int i = 0; i < urlGroup.Count; i++)
                    {
                        baseUrl += $"&url[{i}]={urlGroup[i]}";
                    }
                    var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}");

                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<JObject>(content);
                        var data = res.GetValue("data");
                        var res2 = data.Children().Select(x => (x.Value<string>("url"), x.Value<string>("short_url"))).ToArray();
                        //Pair up the original urls and the destinations
                        for (int i = 0; i < res2.Length; i++)
                        {
                            //Use substrings to find the 'short' url
                            //for some reason the short url is actually http:///12345
                            //which represents the extension and nothing else.
                            var indx = res2[i].Item2.LastIndexOf("/");
                            if (indx != -1)
                            {
                                var shortVal = res2[i].Item2.Substring(indx);
                                var match = urlGroup.FirstOrDefault(x => x.EndsWith(shortVal));
                                if (match != null)
                                {
                                    pairs.Add((res2[i].Item1, match));
                                }
                            }
                        }
                    }
                }
            }

            var nonMatches = urls.Where(x => !pairs.Any(p => p.Item2.Equals(x))).Distinct().ToList();


            return (pairs, nonMatches);
        }

        private async void AdflyResolve_Click(object sender, EventArgs e)
        {
            var urlMatches = new List<string>();
            foreach (var video in Videos)
            {
                var matches = Regex.Matches(video.DisplayDescription, $@"(https?:\/\/)?(w{3}\.)?{Regex.Escape("adf.ly")}(\S+)?");
                foreach (Match match in matches)
                {
                    urlMatches.Add(match.Value);
                }
            }

            var response = await ADFLYGet(adflyUserId.Text, adflyApiKey.Text, urlMatches.ToArray());
            LogMessage(string.Join("\r\n", response.Item1.Select(x => $"Resolved {x.Item2} to {x.Item1}")));
            
            var result = MessageBox.Show("Would you like to replace these domains with their resolved values?", "This will be applied when you choose to update descriptions by pressing the \"Update Video Descriptions\" button.", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                foreach (var video in Videos)
                {
                    foreach (var resolve in response.Item1)
                    {
                        video.DisplayDescription = video.DisplayDescription.Replace(resolve.Item2, resolve.Item1);
                    }
                }

                VideoInformation.Text = NewLineFix(string.Join("\r\n----------\r\n", Videos.Select(x => $"Title: {x.Video.Snippet.Title} ({x.Video.Id})\r\n----------\r\n{x.DisplayDescription}")));
            }

            LogMessage($"Unable to resolve the following urls: \r\n{string.Join("\r\n", response.Item2)}");

            UpdateDescriptionTextChange();
        }

        private void SearchVideosButton_Click(object sender, EventArgs e)
        {
            var searchContent = VideoSearchText.Text;
            Indexes = AllIndexesOf(VideoInformation.Text, searchContent, IgnoreCaseSearch.Checked).ToList();
            indexesLength = searchContent.Length;
            if (Indexes.Any())
            {
                var firstIndex = Indexes[0];
                VideoInformation.SelectionStart = firstIndex;
                VideoInformation.SelectionLength = searchContent.Length;
                VideoInformation.HideSelection = false;
                VideoInformation.ScrollToCaret();
                prevIndex.Enabled = true;
                nextIndex.Enabled = true;
            }
            int sum = 0;
            int videoMatches = 0;
            foreach (var video in Videos)
            {
                int amout = RegMatches(video.DisplayDescription, searchContent, IgnoreCaseSearch.Checked);
                if (amout > 0)
                {
                    sum += amout;
                    videoMatches++;
                }
            }

            MessageBox.Show($"Found {sum} occurrences of {searchContent} in {videoMatches}/{Videos.Count} videos");
            LogMessage($"Found {sum} occurrences of {searchContent} in {videoMatches}/{Videos.Count} videos");
        }
        public int RegMatches(string content, string search, bool ignoreCase)
        {
            search = Regex.Escape(search);
            return ignoreCase ? Regex.Matches(content, search, RegexOptions.IgnoreCase).Count : Regex.Matches(content, search).Count;
        }

        private void prevIndex_Click(object sender, EventArgs e)
        {
            if (Indexes.Any())
            {
                if (indexesLength > 0)
                {
                    if (CurrentSearchIndex == 0)
                    {
                        CurrentSearchIndex = Indexes.Count - 1;
                    }
                    else
                    {
                        CurrentSearchIndex -= 1;
                    }

                    var selectedIndex = Indexes[CurrentSearchIndex];
                    VideoInformation.SelectionStart = selectedIndex;
                    VideoInformation.SelectionLength = indexesLength;
                    VideoInformation.HideSelection = false;
                    VideoInformation.ScrollToCaret();
                }
            }
        }

        private void nextIndex_Click(object sender, EventArgs e)
        {
            if (Indexes.Any())
            {
                if (indexesLength > 0)
                {
                    if (CurrentSearchIndex == Indexes.Count - 1)
                    {
                        CurrentSearchIndex = 0;
                    }
                    else
                    {
                        CurrentSearchIndex++;
                    }

                    var selectedIndex = Indexes[CurrentSearchIndex];
                    VideoInformation.SelectionStart = selectedIndex;
                    VideoInformation.SelectionLength = indexesLength;
                    VideoInformation.HideSelection = false;
                    VideoInformation.ScrollToCaret();
                }
            }
        }

        private void LoadBackup_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                dialog.CheckFileExists = true;
                dialog.Filter = "json|*.json";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var videos = JsonConvert.DeserializeObject<List<Video>>(File.ReadAllText(dialog.FileName));
                        Videos = videos.Select(x => new VideoUpdatable(x)).ToList();
                    }
                    catch
                    {
                        Videos = JsonConvert.DeserializeObject<List<VideoUpdatable>>(File.ReadAllText(dialog.FileName));
                    }

                    LogMessage($"{Videos.Count} Videos Loaded from File.");
                    MessageBox.Show("Videos loaded.");
                    VideoInformation.Text = NewLineFix(string.Join("\n----------\n", Videos.Select(x => $"Title: {x.Video.Snippet.Title} ({x.Video.Id})\n----------\n{x.DisplayDescription}")));

                    TestUpdate.Enabled = true;
                    SearchVideosButton.Enabled = true;
                    Domains.Enabled = true;
                    ResolveDomains.Enabled = true;
                    AdflyResolve.Enabled = true;
                }
            }
        }

        private void LoadTransforms_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Multiselect = false;
                dialog.CheckFileExists = true;
                dialog.Filter = "json|*.json";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Transformations = JsonConvert.DeserializeObject<List<AdditionalMethods.StringTransformation>>(File.ReadAllText(dialog.FileName));
                    UpdateTransformationsInfo();
                }
            }
        }
    }
}
