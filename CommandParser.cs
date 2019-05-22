using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Unix_tr
{
    class CommandParser
    {
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
            List<string> delete = ParseCommand(commands[0]);
            commands.Remove(commands[0]);
            string text = "";
            if (commands[0] == "<")
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
            for (int i = 0; i < delete.Count; i++)
            {
                text = text.Replace(delete[i], "");
            }
            Console.WriteLine(text);

        }
        public void CCommand(List<string> commands)
        {
            List<string> save = ParseCommand(commands[0]);
            List<string> change = ParseCommand(commands[1]);
            if(change.Count != 1)
            {
                throw new Exception("syntax error");
            }
            commands.Remove(commands[0]);
            commands.Remove(commands[0]);
            string text = "";
            if (commands[0] == "<")
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
            Console.WriteLine(text);
            bool isFound = false;
            string space = " ";
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < save.Count; j++)
                {
                    if (save[j][0] == text[i] || space[0] == text[i])
                    {
                        isFound = true;
                        break;
                    }

                }
                if (!isFound)
                {

                    text = text.Replace(text[i], change[0][0]);
                }
                isFound = false;


            }

            Console.WriteLine(text);


        }
        public void CDCommand(List<string> commands)
        {
            List<string> cdcommand = ParseCommand(commands[0]);
            commands.Remove(commands[0]);
            string text = "";
            if (commands[0] == "<")
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
            for (int i = 0; i < cdcommand.Count; i++)
            {
                text = Regex.Replace(text, @"[^0-9$,]", "");
            }
            Console.WriteLine(text);


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
                text = text.Replace(changeFrom[i], changeTo[i]);
            }
            Console.WriteLine(text);

        }
        public void Parse(string command)
        {
            List<string> commList = new List<string>(command.Split());
            if(commList[0] != "tr" && commList[0] != "-help")
            {
                throw new Exception("syntax error (wrong enter)!");
            }
            else if(commList[0] == "-help")
            {
                Console.WriteLine("Commands:");
                Console.WriteLine("-c - indicates the complement of the first set of characters;");
                Console.WriteLine("-cd - removes all non-alphanumeric characters;");
                Console.WriteLine("-d - delete all characters from first set;");
                Console.WriteLine("default - change all characters from first set into corresponding characters from second set;");
                commList[0] = "end";
            }
            else 
            {
                commList.Remove(commList[0]);
            }
            switch(commList[0])
            {
                case "end":
                    break;
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
