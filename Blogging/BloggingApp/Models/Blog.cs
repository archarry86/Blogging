using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BloggingApp.Models {
    public class Blog {
        /// <summary>
        /// Indentifier of the blog
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Status of the blog
        /// </summary>
        [JsonIgnore]
        public BlogStatus BlogStatus { get;  set; }
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
        public User EditorUser { get; set; }
        /// <summary>
        /// Data of the user connection , a finger print
        /// </summary>
        public String FingerPrintUser { get; set; }

    }
}
