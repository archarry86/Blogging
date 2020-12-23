using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApp.Models {
    public class Blog {
        /// <summary>
        /// Indentifier of the blog
        /// </summary>
        public long IdBlog { get; set; }
        /// <summary>
        /// Status of the blog
        /// </summary>
        public BlogStatus BlogStatus { get; set; }
        /// <summary>
        /// Author od the blog
        /// </summary>
        public User Author { get; set; }
        /// <summary>
        /// Title of the blog
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// Topic of the blog
        /// </summary>
        public String Topic { get; set; }
        /// <summary>
        /// Content of the blog
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// Publication Time of the blog
        /// </summary>
        public DateTime PublicationTime { get; set; }
        /// <summary>
        /// Approval Time of the blog
        /// </summary>
        public DateTime ApprovalTime{ get; set; }
        /// <summary>
        /// User Who Approved the blog publication
        /// </summary>
        public User ApprovalUser { get; set; }



    }
}
