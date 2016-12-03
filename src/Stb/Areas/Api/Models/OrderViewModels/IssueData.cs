using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.OrderViewModels
{
    public class IssueData
    {
        public int Id { get; set; }

        public string WorkerId { get; set; }

        public string WorkerName { get; set; }

        public string OrderId { get; set; }

        public int IssueType { get; set; }

        public int SolutionType { get; set; }

        public string Pics { get; set; }

        public string Audios { get; set; }

        public IssueData(Issue issue)
        {
            Id = issue.Id;
            WorkerId = issue.EndUserId;
            WorkerName = issue.EndUser?.Name;
            OrderId = issue.OrderId;
            IssueType = issue.IssueType;
            SolutionType = issue.SolutionType;
            Pics = issue.Pics;
            Audios = issue.Audios;
        }
    }
}
