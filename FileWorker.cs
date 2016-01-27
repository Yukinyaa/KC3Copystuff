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
        /*
         * back up/load these
         * C:\Users\(username)\AppData\Local\Google\Chrome\User Data\Default\IndexedDB\chrome-extension_hkgmldnainaglpjngpajnnjfhpdjkohh_0.indexeddb.leveldb
         * C:\Users\(username)\AppData\Local\Google\Chrome\User Data\Default\Local Storage\chrome-extension_hkgmldnainaglpjngpajnnjfhpdjkohh_0.localstorage(file)
         * C:\Users\(username)\AppData\Local\Google\Chrome\User Data\Default\Managed Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh
         * C:\Users\(username)\AppData\Local\Google\Chrome\User Data\Default\Sync Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh
         * C:\Users\(username)\AppData\Local\Google\Chrome\User Data\Default\Local Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh
         * */
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

        static public void backup()
        {
            setTmpDataDirectory();
            Copy.directory(
                Path.Combine(chromeUserDir,@"Default\IndexedDB\chrome-extension_hkgmldnainaglpjngpajnnjfhpdjkohh_0.indexeddb.leveldb"),
                TmpDataDir
                );
            Copy.directory(
                Path.Combine(chromeUserDir, @"Default\Managed Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh"),
                TmpDataDir
                );
            Copy.directory(
                Path.Combine(chromeUserDir, @"Default\Sync Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh"),
                TmpDataDir
                );
            Copy.directory(
                Path.Combine(chromeUserDir, @"Default\Local Extension Settings\hkgmldnainaglpjngpajnnjfhpdjkohh"),
                TmpDataDir
                );
            Copy.file(
                Path.Combine(chromeUserDir, @"Default\Local Storage\chrome-extension_hkgmldnainaglpjngpajnnjfhpdjkohh_0.localstorage"),
                TmpDataDir
                );
            ZipFile.CreateFromDirectory(TmpDataDir, getSaveDir());
            setTmpDataDirectory();
        }

        static String getSaveDir()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "zip files (*.zip)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            while (saveFileDialog1.ShowDialog() == DialogResult.OK) ;
            return saveFileDialog1.FileName;
        }

    }
}
