// Guids.cs
// MUST match guids.h
using System;

namespace Switch2013
{
    static class GuidList
    {
        public const string guidSwitchPkgString = "49263471-5d3f-45ce-866a-2b12a10310ec";
        public const string guidSwitchCmdSetString = "96a0a949-083c-4e7c-9c36-7825f5c6c95b";

        public static readonly Guid guidSwitchCmdSet = new Guid(guidSwitchCmdSetString);
    };
}