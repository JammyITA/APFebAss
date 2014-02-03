﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace APFebAss
{
    [Serializable()]
    public class BindNotFoundException : System.Exception
    {
        public BindNotFoundException() : base() { }
        public BindNotFoundException(string message) : base(message) { }
        public BindNotFoundException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        protected BindNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
    
}