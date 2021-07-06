using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SE.DAL.Entities
{
    public class MessageInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int MessageId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Subject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Body { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime Date { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public List<SubmissionResult> submissionResults { get; set; }
    }
}
