using System;

namespace le4ge
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CMDAttribute : Attribute
    {
        public string Command { get; set; }

        public CMDAttribute(string cmd)
        {
            this.Command = cmd;
        }
    }
}