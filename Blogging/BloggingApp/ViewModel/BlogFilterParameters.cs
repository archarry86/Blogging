using BloggingApp.Interfaces;
using BloggingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApp.ViewModel {
    public class BlogFilterParameters : IBlogFilterParameters {

        /// <summary>
        /// Status to filter
        /// </summary>
        public BlogStatus BlogStatus { get; set; }
        /// <summary>
        /// Author to filter
        /// </summary>
        public int AuthorId { get; set; }
        /// <summary>
        /// Title to filter
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// Topic to filter
        /// </summary>
        public String Topic { get; set; }
        /// <summary>
        /// Publication to filter
        /// </summary>
        public DateTime PublicationTimeStart { get; set; }
        /// <summary>
        /// Publication to filter
        /// </summary>
        public DateTime PublicationTimeEnd { get; set; }
        /// <summary>
        /// Approval Time to filter
        /// </summary>
        public DateTime ApprovalTimeStart { get; set; }
        /// <summary>
        /// User Who Approved to filter
        /// </summary>
        public int ApprovalUserLogin { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}
