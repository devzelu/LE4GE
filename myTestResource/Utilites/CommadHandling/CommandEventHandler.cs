using AltV.Net;
using AltV.Net.Elements.Entities;
using le4ge.Utilites.Custom;
using System;
using System.Linq;
using System.Reflection;
using logger = le4ge.Utilites.Custom.CustomLogger;

namespace le4ge
{
    public class CommandEventHandler
    {
        private ChatHandler chat = new ChatHandler();

        public CommandEventHandler()
        {
        }

        public void OnPlayerSendCommand(IPlayer player, string cmd, object[] args)
        {
            if (args == null)
            {
                args = new object[] { };
            }
            InvokeCommand(player, cmd, args);
        }

        public MethodInfo ReturnCMDMethod(string cmd)
        {
            var cmdWithOutSlash = cmd.Split("/")[1];
            return Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods())
                .Where(m => m.GetCustomAttributes(typeof(CMDAttribute), false).Length > 0 &&
                m.GetCustomAttribute<CMDAttribute>().Command == cmdWithOutSlash).FirstOrDefault();
        }

        public void InvokeCommand(IPlayer player, string cmd, object[] args)
        {
            try
            {
                MethodInfo method = ReturnCMDMethod(cmd);
                var methodParams = method.GetParameters();//We got an error here; need to debug^^
                var obj = Activator.CreateInstance(method.DeclaringType);
                method.Invoke(obj, CommandTypeParser(player, methodParams, args));
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
                chat.Send(player, "Command not found..");
            }
        }

        public object[] CommandTypeParser(IPlayer player, ParameterInfo[] param, object[] EventArguments)
        {
            object[] array = new object[param.Length];
            array[0] = player;
            if (EventArguments.Length != 0)
            {
                for (int i = 1; i < param.Length; i++)
                {
                    try
                    {
                        if (param[i].ParameterType == typeof(IPlayer))
                        {
                            var plr = Alt.GetAllPlayers()
                                .Where(x => x.Name == EventArguments[i].ToString())
                                .FirstOrDefault();
                            if (plr == null)
                            {
                                player.Emit("notifyClientError", "Player doesn't exist!");
                                break;
                            }
                            else
                            {
                                array[i] = plr;
                            }
                        }
                        else if (param[i].ParameterType.IsEnum)
                        {
                            object enumObj;
                            try
                            {
                                enumObj = Enum.Parse(param[i].ParameterType, EventArguments[i].ToString(), ignoreCase: true);
                            }
                            catch (ArgumentException)
                            {
                                player.Emit("notifyClientError", "\"" + EventArguments[i].ToString() + "\" has not been found in \"" + param[i].Name + "\".");
                                break;
                            }
                            array[i] = enumObj;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        chat.SendErrorMessage(player, "Invalid command arguments..");
                    }
                }
            }
            return array;
        }
    }
}