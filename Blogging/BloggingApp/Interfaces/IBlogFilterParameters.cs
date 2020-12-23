using BloggingApp.Models;
using System;

namespace BloggingApp.Interfaces {
    public interface IBlogFilterParameters {
        DateTime ApprovalTimeStart { get; set; }
        int ApprovalUserLogin { get; set; }
        int AuthorId { get; set; }
        BlogStatus BlogStatus { get; set; }
        DateTime PublicationTimeEnd { get; set; }
        DateTime PublicationTimeStart { get; set; }
        string Title { get; set; }
        string Topic { get; set; }

        int page { get; set; }

        int size { get; set; }
    }
}