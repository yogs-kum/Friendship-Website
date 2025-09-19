using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using System.Xml;

namespace DataAccess.Base
{
    public class ProcessBase
    {
        protected object _theConnection;
        protected object _theTransaction;
        public ProcessBase()
        {
            _theConnection = null;
            _theTransaction = null;
        }

        public object Connection
        {
            get { return _theConnection; }
            set { _theConnection = value; }
        }

        public object Transaction
        {
            get { return _theTransaction; }
            set { _theTransaction = value; }
        }
   
    }
}
