using NPoco;
using System;
using System.Collections.Generic;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;
using Umbraco.Community.Polls.PollConstants;

namespace Umbraco.Community.Polls.Models
{
    [TableName(TableConstants.Questions.TableName)]
    [ExplicitColumns]
    [PrimaryKey("Id", AutoIncrement=true)]
    public class Question
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Responses")]
        public int ResponseCount { get; set; }

        [Column("StartDate")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public DateTime? StartDate { get; set; }

        [Column("EndDate")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public DateTime? EndDate { get; set; }

        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Ignore]
        public IEnumerable<Answer> Answers { get; set; }
        [Ignore]
        public IEnumerable<Response> Responses { get; set; }
    }
}

