﻿/* Copyright 2010-2013 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Text;
using MongoDB.Bson.IO;

namespace MongoDB.Driver.Internal
{
    internal class MongoGetMoreMessage : MongoRequestMessage
    {
        // private fields
        private string _collectionFullName;
        private int _numberToReturn;
        private long _cursorId;

        // constructors
        internal MongoGetMoreMessage(string collectionFullName, int numberToReturn, long cursorId)
            : base(MessageOpcode.GetMore, null)
        {
            _collectionFullName = collectionFullName;
            _numberToReturn = numberToReturn;
            _cursorId = cursorId;
        }

        // protected methods
        protected override void WriteBody(BsonBuffer buffer)
        {
            buffer.WriteInt32(0); // reserved
            buffer.WriteCString(new UTF8Encoding(false, true), _collectionFullName);
            buffer.WriteInt32(_numberToReturn);
            buffer.WriteInt64(_cursorId);
        }
    }
}
