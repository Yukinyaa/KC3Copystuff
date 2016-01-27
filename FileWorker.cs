using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;

namespace WindowsFormsApplication2
{
    class FileWorker
    {
        
        static private String userName = System.Environment.UserName;
        static private String chromeUserDir;// aka C:\Users\(username)\AppData\Local\Google\Chrome\User Data
        static private String TmpDataDir;
        static private String appname = "hkgmldnainaglpjngpajnnjfhpdjkohh";


        static public void setUserName(String username_)
        {
            userName = username_;
            setChromeDataDirectory();
        }
        static public void setChromeDataDirectory(String overrideDirectory=null)
        {
            if (overrideDirectory != null)
            {
                chromeUserDir = overrideDirectory;
            }
            else
                chromeUserDir = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        @"AppData\Local\Google\Chrome\User Data"
                    );
        }
        static void setTmpDataDirectory()
        {
            TmpDataDir = Path.Combine(Path.GetTempPath(),"kc3back");
            try {
                Directory.Delete(TmpDataDir, true);
            }
            catch (DirectoryNotFoundException dnf) { };
            
            Directory.CreateDirectory(TmpDataDir);
        }


        //copy whole zip file into chrome dir
        static public void restore()
        {
            setTmpDataDirectory();
            try
            {
                ZipFile.ExtractToDirectory(getOpenDir(),TmpDataDir);
            }
            catch (Exception e) { return; };
            Copy.directory(TmpDataDir, Path.Combine(chromeUserDir, "Default"));
            setTmpDataDirectory();
        }


        static public void backup()
        {
                /*
             * back up these
             * C:\Users\(username)\AppData\Local\Google\Chrome\User Data\Default\IndexedDB\chrome-extension_hkgmldnainaglpjngpajnnjfhpdjkohh_0.indexeddb.leveldb
             * C:\Users\(username)\AppData\Local\Google\Chrome\User Data\Default\Local Storage\chrome-extension_hkgmldnainaglpjngpajnnjfhpdjkohh_0.localstorage(file)
             * C:\Users\(username)\AppData\Local\Google\Chrome\User Data\Default\Managed Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh
             * C:\Users\(username)\AppData\Local\Google\Chrome\User Data\Default\Sync Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh
             * C:\Users\(username)\AppData\Local\Google\Chrome\User Data\Default\Local Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh
             * */
            setTmpDataDirectory();
            Copy.directory(
                Path.Combine(chromeUserDir,@"Default\IndexedDB\chrome-extension_hkgmldnainaglpjngpajnnjfhpdjkohh_0.indexeddb.leveldb"),
                Path.Combine(TmpDataDir, @"IndexedDB\hkgmldnainaglpjngpajnnjfhpdjkohh")
                );
            Copy.directory(
                Path.Combine(chromeUserDir, @"Default\Managed Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh"),
                Path.Combine(TmpDataDir, @"Managed Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh")
                );
            Copy.directory(
                Path.Combine(chromeUserDir, @"Default\Sync Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh"),
                Path.Combine(TmpDataDir, @"Sync Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh")
                );
            Copy.directory(
                Path.Combine(chromeUserDir, @"Default\Local Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh"),
                Path.Combine(TmpDataDir, @"Local Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh")
                );
            Directory.CreateDirectory(Path.Combine(TmpDataDir, @"Local Storage"));
            Copy.file(
                Path.Combine(chromeUserDir, @"Default\Local Storage\chrome-extension_hkgmldnainaglpjngpajnnjfhpdjkohh_0.localstorage"),
                Path.Combine(TmpDataDir, @"Local Storage\chrome-extension_hkgmldnainaglpjngpajnnjfhpdjkohh_0.localstorage")
                );
            try{
            ZipFile.CreateFromDirectory(TmpDataDir, getSaveDir());
            }
            catch (Exception e) { return; };
            setTmpDataDirectory();
        }

        static String getOpenDir()
        {
            OpenFileDialog FileDialog1 = new OpenFileDialog();

            FileDialog1.Filter = "zip files (*.zip)|*.zip";
            FileDialog1.FilterIndex = 2;
            FileDialog1.RestoreDirectory = true;

                FileDialog1.ShowDialog();
            
            return FileDialog1.FileName;
        }

        static String getSaveDir()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "zip files (*.zip)|*.zip";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            try
            {
                saveFileDialog1.ShowDialog();
            }
            catch (Exception e) { };
            return saveFileDialog1.FileName;
        }

    }
}
