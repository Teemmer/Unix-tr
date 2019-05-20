using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unix_tr
{
    class CommandParser
    {
        //public string path;
        //public string Command { set; get; }
        string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };



        public List<string> SequenceToArray(string command)
        {
            bool isUpper;
            List<string> result = new List<string>();
            if(command[0] == '"' && command[command.Length-1] == '"')
            {
                string comm = command.Remove(0, 1);
                command = comm.Remove(comm.Length - 1, 1);
            }
            else
            {
                throw new Exception("syntax error");
            }
            if (command[0] == '[' && command[command.Length - 1] == ']')
            {
                string comm = command.Remove(0, 1);
                command = comm.Remove(comm.Length - 1, 1);
            }
            if(command[1] != '-' || command[0] >= command[2])
            {
                throw new Exception("syntax error");
            }
            if(command[0].ToString().ToUpper() == command[0].ToString() && command[2].ToString().ToUpper() == command[2].ToString())
            {
                isUpper = true;
            }
            else if(command[0].ToString().ToUpper() == command[0].ToString() && command[2].ToString().ToUpper() != command[2].ToString() || command[0].ToString().ToUpper() != command[0].ToString() && command[2].ToString().ToUpper() == command[2].ToString())
            {
                throw new Exception("syntax error");
            }
            else
            {
                isUpper = false;
            }

            int firstindex = 0;
            int secondindex = 0;
            for (int i = 0; i < alphabet.Length; i++)
            {
                if(command[0].ToString().ToLower() == alphabet[i])
                {
                    firstindex = i;
                }
                if (command[2].ToString().ToLower() == alphabet[i])
                {
                    secondindex = i;
                }
            }
            for (int i = firstindex; i <= secondindex; i++)
            {
                result.Add(isUpper ? alphabet[i].ToUpper() : alphabet[i]);
            }
            return result;
        }

        public List<string> StringToArray(string command)
        {
            List<string> result = new List<string>();
            if (command[0] == '"' && command[command.Length - 1] == '"')
            {
                string comm = command.Remove(0, 1);
                command = comm.Remove(comm.Length - 1, 1);
            }
            else
            {
                throw new Exception("syntax error");
            }
            for (int i = 0; i < command.Length; i++)
            {
                result.Add(command[i].ToString());
            }
            return result;
        }


        public List<string> ParseCommand(string command)
        {
            if(command[0] == '"')
            {
                if(command[2] == '-')
                {
                    return SequenceToArray(command);
                }
                else
                {
                    return StringToArray(command);
                }
            }
            else
            {
                throw new Exception("syntax error");
            }
        }
        public void SCommand(List<string> commands)
        {

        }
        public void DCommand(List<string> commands)
        {

        }
        public void CCommand(List<string> commands)
        {

        }
        public void CDCommand(List<string> commands)
        {

        }
        public void DefaultCommand(List<string> commands)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                Console.WriteLine(commands[i]);
            }
            List<string> changeFrom = ParseCommand(commands[0]);
            List<string> changeTo = ParseCommand(commands[1]);
            if(changeFrom.Count != changeTo.Count)
            {
                throw new Exception("can't convert. different size");
            }
            commands.Remove(commands[0]);
            commands.Remove(commands[0]);
            string text = "";
            if(commands[0] == "<")
            {
                string path = @"D:\C#\Unix-tr\Unix-tr\" + commands[1];
                using (StreamReader reader = new StreamReader(path, Encoding.Default))
                {
                    text = reader.ReadToEnd();
                }
            }
            else
            {
                for (int i = 0; i < commands.Count; i++)
                {
                    text += commands[i] + " ";
                }
            }
            for (int i = 0; i < changeTo.Count; i++)
            {
                Console.WriteLine(changeFrom[i] + " = " + changeTo[i]);
                text = text.Replace(changeFrom[i], changeTo[i]);
            }
            Console.WriteLine(text);

        }
        public void Parse(string command)
        {
            List<string> commList = new List<string>(command.Split());
            //for (int i = 1; i < commList.Count - 1; i++)
            //{
            //    if(commList[i][0] == '"' && commList[i + 1][0] == '"')
            //    {
            //        commList[i + 1] = "\" \"";
            //        commList.Remove(commList[i]);
            //        break;
            //    }  

            //}
            if(commList[0] != "tr")
            {
                throw new Exception("syntax error (wrong enter)!");
            }
            else
            {
                commList.Remove(commList[0]);
            }
            switch(commList[1])
            {
                case "-s":
                    commList.Remove(commList[0]);
                    SCommand(commList);
                    break;
                case "-d":
                    commList.Remove(commList[0]);
                    DCommand(commList);
                    break;
                case "-c":
                    commList.Remove(commList[0]);
                    CCommand(commList);
                    break;
                case "-cd":
                    commList.Remove(commList[0]);
                    CDCommand(commList);
                    break;
                default:

                    DefaultCommand(commList);
                    break;

            }
        }

        
    }
}
