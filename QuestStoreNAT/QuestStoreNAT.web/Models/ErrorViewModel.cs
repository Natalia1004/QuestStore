using System;

namespace QuestStoreNAT.web.Models
{
    public class ErrorViewModel
    {
        public string Id { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
