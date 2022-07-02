using MelonLoader;

namespace MoreControl.Utilts
{
    class Log
    {
        public static void Msg(object Message)
        {
            MelonLogger.Msg(Message);
        }
        public static void Error(object Error_Message)
        {
            MelonLogger.Msg(System.ConsoleColor.Red, Error_Message);
        }
        public static void CompileMsg(object Compile_Message)
        {
            MelonLogger.Msg(System.ConsoleColor.Green, Compile_Message);
        }
        public static void UtilMessage(object UtilMessage)
        {
            MelonLogger.Msg(System.ConsoleColor.Blue, UtilMessage);
        }
        public static void CMsg(object message, System.ConsoleColor color)
        {
            MelonLogger.Msg(color, message);
        }
    }
}
