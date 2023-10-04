using System;
using System.Collections.Generic;
using NPoco;
using Our.Community.Polls.PollConstants;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Our.Community.Polls.Models
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
        [NullSetting]
        public DateTime? StartDate { get; set; }

        [Column("EndDate")]
        [NullSetting]
        public DateTime? EndDate { get; set; }

        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [Ignore]
        public string CardClass { get; set; }

        [Ignore]
        public IEnumerable<Answer> Answers { get; set; }
        [Ignore]
        public IEnumerable<Response> Responses { get; set; }
    }
}

