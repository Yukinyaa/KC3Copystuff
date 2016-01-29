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


        static public void overrideAppname(String appname_)
        {
            appname = appname_;
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
            catch (DirectoryNotFoundException) { };
            
            Directory.CreateDirectory(TmpDataDir);
        }



        static public void restore() //copy whole zip file into chrome data dir
        {
            setTmpDataDirectory();
            try
            {
                ZipFile.ExtractToDirectory(getOpenDir(),TmpDataDir);
            }
            catch (Exception) { return; };
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
                Path.Combine(chromeUserDir,@"Default\IndexedDB\chrome-extension_"+appname+"_0.indexeddb.leveldb"),
                Path.Combine(TmpDataDir, @"IndexedDB\chrome-extension_" + appname + "_0.indexeddb.leveldb")
                );
            /* ^ this is indexed db.
            Copy.directory(
                Path.Combine(chromeUserDir, @"Default\Managed Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh"),
                Path.Combine(TmpDataDir, @"Managed Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh")
                );
             //* /
            Copy.directory(
                Path.Combine(chromeUserDir, @"Default\Sync Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh"),
                Path.Combine(TmpDataDir, @"Sync Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh")
                );
             //* /
            Copy.directory(
                Path.Combine(chromeUserDir, @"Default\Local Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh"),
                Path.Combine(TmpDataDir, @"Local Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh")
                );
             //*/
            Directory.CreateDirectory(Path.Combine(TmpDataDir, @"Local Storage"));
            Copy.file(
                Path.Combine(chromeUserDir, @"Default\Local Storage\chrome-extension_" + appname + "_0.localstorage"),
                Path.Combine(TmpDataDir, @"Local Storage\chrome-extension_" + appname + "_0.localstorage")
                );
            try{
            ZipFile.CreateFromDirectory(TmpDataDir, getSaveDir());
            }
            catch (Exception) { return; };
            setTmpDataDirectory();
        }

        static String getOpenDir()
        {
            OpenFileDialog FileDialog1 = new OpenFileDialog();

            FileDialog1.Filter = "KC3改 backup data(.kcb)|*.kcb";
            FileDialog1.FilterIndex = 2;
            FileDialog1.RestoreDirectory = true;

                FileDialog1.ShowDialog();
            
            return FileDialog1.FileName;
        }
        static String getSaveDir()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "zip files (*.kcb)|*.kcb";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            try
            {
                saveFileDialog1.ShowDialog();
            }
            catch (Exception) { };
            return saveFileDialog1.FileName;
        }

    }
}
