using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CommonHelpers.Services.DataModels
{
    [DataContract]
    public class CharactersResult
    {
        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "number_of_page_results")]
        public int NumberOfPageResults { get; set; }

        [DataMember(Name = "number_of_total_results")]
        public int NumberOfTotalResults { get; set; }

        [DataMember(Name = "status_code")]
        public int StatusCode { get; set; }

        [DataMember(Name = "results")]
        public List<Character> Results { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; }
    }

    [DataContract]
    public class Character
    {
        [DataMember(Name = "aliases")]
        public string Aliases { get; set; }

        [DataMember(Name = "api_detail_url")]
        public string ApiDetailUrl { get; set; }

        [DataMember(Name = "birth")]
        public string Birth { get; set; }

        [DataMember(Name = "count_of_issue_appearances")]
        public int CountOfIssueAppearances { get; set; }

        [DataMember(Name = "date_added")]
        public string DateAdded { get; set; }

        [DataMember(Name = "date_last_updated")]
        public string DateLastUpdated { get; set; }

        [DataMember(Name = "deck")]
        public string Deck { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "first_appeared_in_issue")]
        public FirstAppearedInIssue FirstAppearedInIssue { get; set; }

        [DataMember(Name = "gender")]
        public int Gender { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "image")]
        public Image Image { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "origin")]
        public Origin Origin { get; set; }

        [DataMember(Name = "publisher")]
        public Publisher Publisher { get; set; }

        [DataMember(Name = "real_name")]
        public string RealName { get; set; }

        [DataMember(Name = "site_detail_url")]
        public string SiteDetailUrl { get; set; }
    }

    [DataContract]
    public class Image
    {
        [DataMember(Name = "icon_url")]
        public string IconUrl { get; set; }

        [DataMember(Name = "medium_url")]
        public string MediumUrl { get; set; }

        [DataMember(Name = "screen_url")]
        public string ScreenUrl { get; set; }

        [DataMember(Name = "screen_large_url")]
        public string ScreenLargeUrl { get; set; }

        [DataMember(Name = "small_url")]
        public string SmallUrl { get; set; }

        [DataMember(Name = "super_url")]
        public string SuperUrl { get; set; }

        [DataMember(Name = "thumb_url")]
        public string ThumbUrl { get; set; }

        [DataMember(Name = "tiny_url")]
        public string TinyUrl { get; set; }

        [DataMember(Name = "original_url")]
        public string OriginalUrl { get; set; }

        [DataMember(Name = "image_tags")]
        public string ImageTags { get; set; }
    }

    [DataContract]
    public class FirstAppearedInIssue
    {
        [DataMember(Name = "api_detail_url")]
        public string ApiDetailUrl { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "issue_number")]
        public string IssueNumber { get; set; }
    }

    [DataContract]
    public class Origin
    {
        [DataMember(Name = "api_detail_url")]
        public string ApiDetailUrl { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    [DataContract]
    public class Publisher
    {
        [DataMember(Name = "api_detail_url")]
        public string ApiDetailUrl { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    [DataContract]
    public class VideosResult
    {
        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "number_of_page_results")]
        public int NumberOfPageResults { get; set; }

        [DataMember(Name = "number_of_total_results")]
        public int NumberOfTotalResults { get; set; }

        [DataMember(Name = "status_code")]
        public int StatusCode { get; set; }

        [DataMember(Name = "results")]
        public List<Video> Results { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; }
    }

    [DataContract]
    public class Video
    {
        [DataMember(Name = "api_detail_url")]
        public string ApiDetailUrl { get; set; }

        [DataMember(Name = "deck")]
        public string Deck { get; set; }

        [DataMember(Name = "high_url")]
        public string HighUrl { get; set; }

        [DataMember(Name = "low_url")]
        public string LowUrl { get; set; }

        [DataMember(Name = "embed_player")]
        public string EmbedPlayer { get; set; }

        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "length_seconds")]
        public int LengthSeconds { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "publish_date")]
        public string PublishDate { get; set; }

        [DataMember(Name = "site_detail_url")]
        public string SiteDetailUrl { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "image")]
        public Image Image { get; set; }

        [DataMember(Name = "user")]
        public string User { get; set; }

        [DataMember(Name = "video_type")]
        public string VideoType { get; set; }

        [DataMember(Name = "video_show")]
        public object VideoShow { get; set; }

        [DataMember(Name = "video_categories")]
        public List<VideoCategories> VideoCategories { get; set; }

        [DataMember(Name = "saved_time")]
        public object SavedTime { get; set; }

        [DataMember(Name = "youtube_id")]
        public string YoutubeId { get; set; }
    }

    [DataContract]
    public class VideoCategories
    {
        [DataMember(Name = "api_detail_url")]
        public string ApiDetailUrl { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "site_detail_url")]
        public string SiteDetailUrl { get; set; }
    }
}