using DDST;
using System.Text.Json;
using System;
using System.Reflection.PortableExecutable;
using System.IO;

namespace DarkestDungeonSaveTrick
{
    public class SavesData
    {
        private const string NoGameDataErr = "Не найдена директория игры с сохранением!";
        private const string NoStashDirErr = "Не найдена директория хранилища сохранений!";
        private const string ConfigFileName = "ddst.json";
        public bool IsReady = false;
        private DataJSON Data;
        public SavesData()
        {
            try
            {
                Data = new DataJSON();
                ReadJSON();
                if (!Directory.Exists(Data.GameSavesPath))
                {
                    throw new Exception(NoGameDataErr);
                }                
                if (!Directory.Exists(Data.StashPath))
                {
                    throw new Exception(NoStashDirErr);
                }
                if (Data.StashPath[Data.StashPath.Length - 1] != '\\')
                {
                    Data.StashPath += "\\";
                }                
                IsReady = true;
            }
            catch (Exception e) 
            { 
                IsReady = false;                
            }
        }
        private void ReadJSON()
        {
            string json = File.ReadAllText(ConfigFileName);
            Data = JsonSerializer.Deserialize<DataJSON>(json)!;
        }      
        
        public bool Save()
        {
            try
            {
                if (IsReady)
                {
                    string newDirPath = Data.StashPath + GenerateDirName();
                    Directory.CreateDirectory(newDirPath);
                    CopyFiles(Data.GameSavesPath, newDirPath);
                    // скопировать в нее данные игры
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }        
        private string GenerateDirName()
        {
            DateTime dt = DateTime.Now;
            string res = "";
            res += dt.Year;
            res = (dt.Month >= 10) ? (res + dt.Month) : (res + "0" + dt.Month);
            res = (dt.Day >= 10) ? (res + dt.Day) : (res + "0" + dt.Day);
            res = (dt.Hour >= 10) ? (res + dt.Hour) : (res + "0" + dt.Hour);
            res = (dt.Minute >= 10) ? (res + dt.Minute) : (res + "0" + dt.Minute);
            res = (dt.Second >= 10) ? (res + dt.Second) : (res + "0" + dt.Second);                        
            return res;
        }
        private void CopyFiles(string aSourcePath, string aTargetPath) // https://stackoverflow.com/questions/58744/copy-the-entire-contents-of-a-directory-in-c-sharp/3822913#3822913
        {
            foreach (string dirPath in Directory.GetDirectories(aSourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(aSourcePath, aTargetPath));
            }
            foreach (string newPath in Directory.GetFiles(aSourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(aSourcePath, aTargetPath), true);
            }
        }
        public bool Load()
        {
            CopyFiles(FindLastSaveDir(), Data.GameSavesPath);
            return true;
        }
        private string FindLastSaveDir()
        {
            string res = "";
            foreach (string dir in Directory.GetDirectories(Data.StashPath, "*", SearchOption.TopDirectoryOnly))
            {
                int i = string.Compare(dir, res, StringComparison.OrdinalIgnoreCase);
                res = i > 0 ? dir : res;                
            }
            return res;
        }
    }
}