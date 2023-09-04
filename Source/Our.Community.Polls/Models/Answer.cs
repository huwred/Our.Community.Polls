﻿using NPoco;
using System.Collections.Generic;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;
using Umbraco.Community.Polls.PollConstants;

namespace Umbraco.Community.Polls.Models
{
    [TableName(TableConstants.Answers.TableName)]
    [ExplicitColumns]
    [PrimaryKey("Id", AutoIncrement=true)]
    public class Answer
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("Value")]
        public string Value { get; set; }

        [Column("Index")]
        public int Index { get; set; }

        [ForeignKey(typeof(Question))]
        [Column("QuestionId")]
        public int QuestionId { get; set; }

        [Ignore]
        public double Percentage { get; set; }
        [Ignore]
        public IEnumerable<Response> Responses { get; set; }
    }
}