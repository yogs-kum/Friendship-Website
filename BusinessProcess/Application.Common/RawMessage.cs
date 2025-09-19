using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public struct RawMessage
    {
        public string Id;
        public string Text;
        public string Type;

        public RawMessage(string MessageId, string MessageText, string MessageType)
        {
            Id = MessageId;
            Text = MessageText;
            Type = MessageType; 
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
