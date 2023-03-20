using System;
using System.Collections.Generic;

namespace Encuesta.Models;

    public class CommentForAnalysis
    {
        public string? Comment { get; set; }
    }

    public class SentimentResponse
    {
        public Sentiment? DocumentSentiment { get; set; }
    }

    public class Sentiment
    {
        public float Magnitude { get; set; }
        public float Score { get; set; }
    }

    public partial class Usuario
    {
        public int Id { get; set; }
        public int? NpsScore { get; set; }
        public string? Comment { get; set; }
    }

