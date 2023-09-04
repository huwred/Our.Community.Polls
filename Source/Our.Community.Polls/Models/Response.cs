using NPoco;
using System;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;
using Umbraco.Community.Polls.PollConstants;

namespace Umbraco.Community.Polls.Models
{
    [TableName(TableConstants.Responses.TableName)]
    [ExplicitColumns]
    [PrimaryKey("Id", AutoIncrement=true)]
    public class Response
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("ResponseDate")]
        public DateTime ResponseDate { get; set; }

        /// <summary>
        /// Gets or sets the poll id.
        /// </summary>
        [Column("QuestionId")]
        [ForeignKey(typeof(Question))]
        public int QuestionId { get; set; }

        /// <summary>
        /// Gets or sets the answer id.
        /// </summary>
        [Column("AnswerId")]
        [ForeignKey(typeof(Answer))]
        public int AnswerId { get; set; }
    }
}