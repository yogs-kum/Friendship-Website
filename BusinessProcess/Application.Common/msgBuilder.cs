using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Application.Common
{
    public class msgBuilder
    {
        protected NameValueCollection _dataElements;
        protected NameValueCollection _MsgRepository;

        public msgBuilder()
        {
            _dataElements = new NameValueCollection();
            _MsgRepository = new NameValueCollection();
        }

        public NameValueCollection DataElements
        { get { return _dataElements; } }

        public NameValueCollection MsgRepository
        { get { return _MsgRepository; } }

        public string BuildMessage(string MessageId)
        {
            string text = _MsgRepository[MessageId.Trim()];
            //ICollection<> theElements = _dataElements.Keys;
            foreach (string key in _dataElements.Keys)
            {
                text = text.Replace(string.Format("<DataElement>{0}</DataElement>", key), _dataElements[key]);
            }
            return text;
        }
    }
}
