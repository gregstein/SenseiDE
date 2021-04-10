using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Ionic.Zip;
using Microsoft.Win32;
using Steamworks;


namespace DE_Sensei
{
    
    public partial class Sensei : OfficeForm
    {

        perfCLASS pf = new perfCLASS();
        public string dePATH = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Games\Age of Empires 2 DE";
        public Dictionary<string, string> ModMenu = new Dictionary<string, string>();
        private string saveDirectoryPath = "";
        private string UserDataDir = "";
        private string UserSteamID = "";
        private string DecompTMP;
        private static string zoomPhrase2 = @"Default Zoom:(\d+)";
        List<string> aopDICT = new List<string>()
{
"Health Bar Visibility:1",
"Health Bar Visibility:0",
"Health Bars:1",
"Health Bars:0",
"Click Drag Scroll Button:1", 
"Click Drag Scroll Button:0", 
"VillagerDoubleClick:1", 
"VillagerDoubleClick:0",
"ZoomToCursor:1", 
"ZoomToCursor:0", 
"ings:0", 
"ings:1",
"AppendGroups:0",
"AppendGroups:1",
"Default Zoom"
};
        public Sensei()
        {

            InitializeComponent();

            if (!pf.senseiEXISTS())
                pf.senseiBORN();

            //if(!pf.KidsHERE())
            //    pf.senseiBORN();
            
            
            pf.RetSETTINGS("Splash Screen Pixelate", splashswitch);
            pf.RetSETTINGS2("Shrink", shrinktrack, shrinklbl);
            pf.RetSETTINGS("Skip Intro", introswitch);
            pf.RetSETTINGS("Disable Effects", effectswitch);
            pf.RetSETTINGS3("Performance", perfcb);
            pf.RetSETTINGS("Menu Effects",menueff);
            pf.RetSETTINGS("WinDef", winDEFENDER);

        }
        private static void RunAsDesktopUser(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileName));

            // To start process as shell user you will need to carry out these steps:
            // 1. Enable the SeIncreaseQuotaPrivilege in your current token
            // 2. Get an HWND representing the desktop shell (GetShellWindow)
            // 3. Get the Process ID(PID) of the process associated with that window(GetWindowThreadProcessId)
            // 4. Open that process(OpenProcess)
            // 5. Get the access token from that process (OpenProcessToken)
            // 6. Make a primary token with that token(DuplicateTokenEx)
            // 7. Start the new process with that primary token(CreateProcessWithTokenW)

            var hProcessToken = IntPtr.Zero;
            // Enable SeIncreaseQuotaPrivilege in this process.  (This won't work if current process is not elevated.)
            try
            {
                var process = GetCurrentProcess();
                if (!OpenProcessToken(process, 0x0020, ref hProcessToken))
                    return;

                var tkp = new TOKEN_PRIVILEGES
                {
                    PrivilegeCount = 1,
                    Privileges = new LUID_AND_ATTRIBUTES[1]
                };

                if (!LookupPrivilegeValue(null, "SeIncreaseQuotaPrivilege", ref tkp.Privileges[0].Luid))
                    return;

                tkp.Privileges[0].Attributes = 0x00000002;

                if (!AdjustTokenPrivileges(hProcessToken, false, ref tkp, 0, IntPtr.Zero, IntPtr.Zero))
                    return;
            }
            finally
            {
                CloseHandle(hProcessToken);
            }

            // Get an HWND representing the desktop shell.
            // CAVEATS:  This will fail if the shell is not running (crashed or terminated), or the default shell has been
            // replaced with a custom shell.  This also won't return what you probably want if Explorer has been terminated and
            // restarted elevated.
            var hwnd = GetShellWindow();
            if (hwnd == IntPtr.Zero)
                return;

            var hShellProcess = IntPtr.Zero;
            var hShellProcessToken = IntPtr.Zero;
            var hPrimaryToken = IntPtr.Zero;
            try
            {
                // Get the PID of the desktop shell process.
                uint dwPID;
                if (GetWindowThreadProcessId(hwnd, out dwPID) == 0)
                    return;

                // Open the desktop shell process in order to query it (get the token)
                hShellProcess = OpenProcess(ProcessAccessFlags.QueryInformation, false, dwPID);
                if (hShellProcess == IntPtr.Zero)
                    return;

                // Get the process token of the desktop shell.
                if (!OpenProcessToken(hShellProcess, 0x0002, ref hShellProcessToken))
                    return;

                var dwTokenRights = 395U;

                // Duplicate the shell's process token to get a primary token.
                // Based on experimentation, this is the minimal set of rights required for CreateProcessWithTokenW (contrary to current documentation).
                if (!DuplicateTokenEx(hShellProcessToken, dwTokenRights, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, TOKEN_TYPE.TokenPrimary, out hPrimaryToken))
                    return;

                // Start the target process with the new token.
                var si = new STARTUPINFO();
                var pi = new PROCESS_INFORMATION();
                if (!CreateProcessWithTokenW(hPrimaryToken, 0, fileName, "", 0, IntPtr.Zero, Path.GetDirectoryName(fileName), ref si, out pi))
                    return;
            }
            finally
            {
                CloseHandle(hShellProcessToken);
                CloseHandle(hPrimaryToken);
                CloseHandle(hShellProcess);
            }

        }

        #region Interop

        private struct TOKEN_PRIVILEGES
        {
            public UInt32 PrivilegeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public LUID_AND_ATTRIBUTES[] Privileges;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public UInt32 Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LUID
        {
            public uint LowPart;
            public int HighPart;
        }

        [Flags]
        private enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        private enum SECURITY_IMPERSONATION_LEVEL
        {
            SecurityAnonymous,
            SecurityIdentification,
            SecurityImpersonation,
            SecurityDelegation
        }

        private enum TOKEN_TYPE
        {
            TokenPrimary = 1,
            TokenImpersonation
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string host, string name, ref LUID pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TOKEN_PRIVILEGES newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);


        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, uint processId);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DuplicateTokenEx(IntPtr hExistingToken, uint dwDesiredAccess, IntPtr lpTokenAttributes, SECURITY_IMPERSONATION_LEVEL impersonationLevel, TOKEN_TYPE tokenType, out IntPtr phNewToken);

        [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CreateProcessWithTokenW(IntPtr hToken, int dwLogonFlags, string lpApplicationName, string lpCommandLine, int dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        #endregion
        public string SaveDirectoryPath()
        {
            if (this.saveDirectoryPath == "")
            {
                uint size = SteamApps.GetAppInstallDir((AppId_t)813780, out this.saveDirectoryPath, 500u);

                if (size <= 0)
                {

                }
                else
                {

                }

            }

            return this.saveDirectoryPath;
        }
        public string UserDataDirectory()
        {
            if(this.UserDataDir == "")
            {
                string Buffer;
                var idUSR = SteamUser.GetUserDataFolder(out Buffer, 260);
                return Buffer.ToString().Replace(@"\local", "");
            }
            return this.UserDataDir;
        }
        public string _UserSteamID()
        {
            if (this.UserSteamID == "")
            {
                
                var idUSR = SteamUser.GetSteamID();
                return idUSR.ToString();
            }
            return this.UserSteamID;
        }
        private void RemoveDirectories(string strpath)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                if (Directory.Exists(strpath))
                {
                    
                        DirectoryInfo dirInfo = new DirectoryInfo(strpath);
                    var files = dirInfo.GetFiles();

                    this.BeginInvoke(new Action(() =>
                    {
                        pBAR.Minimum = 0;
                        pBAR.Value = 0;
                        pBAR.Maximum = files.Length;
                        pBAR.Step = 1;
                    }));

                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                        this.BeginInvoke(new Action(() => pBAR.PerformStep())); 

                    }

                    var dirs = dirInfo.GetDirectories();

                    this.BeginInvoke(new Action(() =>
                    {
                        pBAR.Value = 0;
                        pBAR.Maximum = dirs.Length;
                    }));

                    foreach (DirectoryInfo dir in dirs)
                    {
                        
                        dir.Delete(true);

                        this.BeginInvoke(new Action(() => pBAR.PerformStep())); 
                    }
                  
            }
            }, null);

        }
        public void Compress(FileInfo fileToDecompress)
        {

            
                using (FileStream originalFileStream = fileToDecompress.OpenRead())
                {
                    
                        using (FileStream compressedFileStream = File.Create(fileToDecompress.FullName + ".cm"))
                        {
                            using (DeflateStream compressionStream = new DeflateStream(compressedFileStream, CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);
                            }
                        }

                        FileInfo info = new FileInfo(fileToDecompress + ".cm");
                        
                   
                }
        }
        public void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length) + ".tmp";

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (DeflateStream decompressionStream = new DeflateStream(originalFileStream, CompressionMode.Decompress))
                    {
                        DecompTMP = newFileName;
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
            
        }
        public int FindBytes(byte[] src, byte[] find)
        {
            int index = -1;
            int matchIndex = 0;
            // handle the complete source array
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == find[matchIndex])
                {
                    if (matchIndex == (find.Length - 1))
                    {
                        index = i - matchIndex;
                        break;
                    }
                    matchIndex++;
                }
                else if (src[i] == find[0])
                {
                    matchIndex = 1;
                }
                else
                {
                    matchIndex = 0;
                }

            }
            return index;
        }

        public byte[] ReplaceBytes(byte[] src, byte[] search, byte[] repl)
        {
            byte[] dst = null;
            int index = FindBytes(src, search);
            if (index >= 0)
            {
                dst = new byte[src.Length - search.Length + repl.Length];
                // before found array
                Buffer.BlockCopy(src, 0, dst, 0, index);
                // repl copy
                Buffer.BlockCopy(repl, 0, dst, index, repl.Length);
                // rest of src array
                Buffer.BlockCopy(
                    src,
                    index + search.Length,
                    dst,
                    index + repl.Length,
                    src.Length - (index + search.Length));
            }
            return dst;
        }
        private void Sensei_Load(object sender, EventArgs e)
        {
            int _sw = this.Width;
            int _sh = this.Height;
            this.MinimumSize = new Size(_sw, _sh);
            this.MaximumSize = new Size(_sw, _sh);
            //byte[] test = File.ReadAllBytes(@"C:\Users\shock\Games\Age of Empires 2 DE\76561198079200175\profile\Player").Skip(0x17).Take(150).ToArray();
            //MessageBox.Show(System.Text.Encoding.UTF8.GetString(test));
            //Decompress(new FileInfo(@"C:\Users\shock\Games\Age of Empires 2 DE\76561198079200175\profile\AdditionalOptions.aop"));
            //pf.HKIwriter(@"C:\Users\shock\Games\Age of Empires 2 DE\76561198079200175\profile\PlayerDefault");


        }

        private async void perfcb_SelectedValueChanged(object sender, EventArgs e)
        {
            if(perfcb.SelectedItem.ToString() == "High Performance" && perfcb.Focus())
            {
                File.WriteAllText(System.IO.Path.GetTempPath() + "perf.reg", Properties.Resources.low);
                Task<int> ImpRegistry = pf.importRegistry(System.IO.Path.GetTempPath() + "perf.reg");
                int resultimp = await ImpRegistry;
                if (resultimp == 1)
                {
                    pf.SenseiREG("Performance", 1, Microsoft.Win32.RegistryValueKind.DWord);
                    MessageBoxEx.Show("Graphics Settings are set to LOW.", "High Performance Activated!");
                }
            }
            else if (perfcb.SelectedItem.ToString() == "Default" && perfcb.Focus())
            {
                File.WriteAllText(System.IO.Path.GetTempPath() + "perf.reg", Properties.Resources.def);
                Task<int> ImpRegistry = pf.importRegistry(System.IO.Path.GetTempPath() + "perf.reg");
                int resultimp = await ImpRegistry;
                if (resultimp == 1)
                {
                    pf.SenseiREG("Performance", 0, Microsoft.Win32.RegistryValueKind.DWord);
                    MessageBoxEx.Show("Graphics Settings are set back to DEFAULT.", "Default Activated!");
                }
            }

        }

        private void resocb_SelectedValueChanged(object sender, EventArgs e)
        {
          
        }

        private void effectswitch_ValueChanged(object sender, EventArgs e)
        {
            if(effectswitch.ValueObject.ToString() == "Y")
            {
                efconfig.Enabled = true;
                if(effectswitch.Focus())
                {
                //Disabling cool destructions, particles
                pf.RegEdit("Cool Destructions", 0, Microsoft.Win32.RegistryValueKind.DWord);
                pf.RegEdit("Particles", 0, Microsoft.Win32.RegistryValueKind.DWord);
                pf.SenseiREG("Disable Effects", 1, Microsoft.Win32.RegistryValueKind.DWord);
                
                DialogResult dres = MessageBoxEx.Show("Cool Destructions & Particles are Disabled.\n Press OK to Disable/Enable Other Hidden Game Effects & Animations.", "Success!");
                if(dres == DialogResult.OK)
                    {
                        //More Effects to disable window
                        Main mn = new Main();
                        mn.GetDEPath = SaveDirectoryPath();
                        mn.GetGames = dePATH;
                        mn.Show();
                    }
              
                }
            }
            else if (effectswitch.ValueObject.ToString() == "N" && effectswitch.Focus())
            {
                //Enabling cool destructions, particles
                pf.RegEdit("Cool Destructions", 1, Microsoft.Win32.RegistryValueKind.DWord);
                pf.RegEdit("Particles", 0, Microsoft.Win32.RegistryValueKind.DWord);
                pf.SenseiREG("Disable Effects", 0, Microsoft.Win32.RegistryValueKind.DWord);
                MessageBoxEx.Show("Cool Destructions & Particles are Enabled.", "Success!");
            }
        }
        public bool PSubmiter(ListBoxItem rec, string title)
        {
            //ListBoxItem rec = new ListBoxItem();
            rec.Text = title;
            rec.Image = DE_Sensei.Properties.Resources.cloud;
            listsubmitee.Items.Add(rec);
            return true;
        }
        public void RefreshMods()
        {
            ModMenu.Clear();
            listsubmitee.Items.Clear();
            if (!Directory.Exists(dePATH))
            {
                MessageBoxEx.Show("Your AoE2 Profile Folder Do Not Exist! Try to Run AoE2 DE Then Try Again!", "Alert!");
                return;
            }
            var subdirectoryEntries = new DirectoryInfo(dePATH).GetDirectories("*", SearchOption.AllDirectories).OrderByDescending(x => x.LastWriteTime);
            if (subdirectoryEntries.Count() == 0)
            {
                MessageBoxEx.Show("Your AoE2 Profile Folder Do Not Exist! Try to Run AoE2 DE Then Try Again!", "Alert!");
                return;
            }
            //foreach (DirectoryInfo subdirectory in subdirectoryEntries)
            //{

            //    if (pf.IsDigitsOnly(subdirectory.FullName.Replace(dePATH + "\\", "")) && subdirectory.FullName.Length > 4 && subdirectory.FullName.Replace(dePATH + "\\", "") != "0")
            //    {
                    var allmods = new DirectoryInfo(dePATH + @"\" + _UserSteamID() + @"\mods\subscribed").GetDirectories("*", SearchOption.TopDirectoryOnly).OrderByDescending(x => x.LastWriteTime);
                    foreach (DirectoryInfo dirmod in allmods)
                    {

                        //Store mods in dictionary to save resources
                        if (!ModMenu.ContainsKey(Regex.Replace(dirmod.Name, @"[0-9]+_", "")))
                            ModMenu.Add(Regex.Replace(dirmod.Name, @"[0-9]+_", ""), dirmod.FullName);

                        ListBoxItem rec = new ListBoxItem();
                        PSubmiter(rec, Regex.Replace(dirmod.Name, @"[0-9]+_", ""));

                    }

            //    }
            //}
            //set mod counter
            modcountB.Text = listsubmitee.Items.Count.ToString();
            itemselecB.Text = listsubmitee.SelectedItems.Count.ToString();
        }
        private void modswitch_ValueChanged(object sender, EventArgs e)
        {
            if (modswitch.ValueObject.ToString() == "Y" && modswitch.Focus())
            {
                RefreshMods();
                pf.SenseiREG("Mods Manager", 1, Microsoft.Win32.RegistryValueKind.DWord);

            }
            else if(modswitch.ValueObject.ToString() == "N" && modswitch.Focus())
            {
                listsubmitee.Items.Clear();
                //clear mod counter
                modcountB.Text = "0";
                itemselecB.Text = "0";
                pf.SenseiREG("Mods Manager", 0, Microsoft.Win32.RegistryValueKind.DWord);
            }
        }

        private void modslist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public async Task<int> xCopy(string SourcePath, string DestinationPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all the files & Replaces any files with the same name
            List<string> exts = new List<string> { "thumbnail.png", "thumbnail.jpg", "info.json", "_preview-icon.png", "_preview-icon.jpg" };
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                SearchOption.AllDirectories))
            {
                if(!newPath.Contains(exts[0]) && !newPath.Contains(exts[1]) && !newPath.Contains(exts[2]))
                {
                    using (Stream source = File.OpenRead(newPath))
                    {
                        using (Stream destination = File.Create(newPath.Replace(SourcePath, DestinationPath)))
                        {
                            await source.CopyToAsync(destination);
                            pBAR.Value++;
                        }
                    }
                }
                //File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
            }
            return 0;
               
        }
        private async void offinstall_Click(object sender, EventArgs e)
        {
            int filesCOUNT = 0;
            var miMOD = listsubmitee.SelectedItems.ToList();
            foreach (var item in listsubmitee.SelectedItems)
            {
                if (ModMenu.ContainsKey(item.Text))
                {
                    string mpath;
                    if (ModMenu.TryGetValue(item.Text, out mpath))
                        filesCOUNT = Directory.GetFiles(mpath,"*.*").Count();
                }
             }
            //Set Progress
            pBAR.Maximum = filesCOUNT;

            foreach (var item in listsubmitee.SelectedItems)
            {
                if (ModMenu.ContainsKey(item.Text))
                {
                    string mpath;
                    if (ModMenu.TryGetValue(item.Text, out mpath))
                        await xCopy(mpath, saveDirectoryPath);
                    //await pf.CopyFileAsync(mpath, saveDirectoryPath, pBAR);



                    
                }
                
                //xCopy(item.Name, saveDirectoryPath);
            }
            pBAR.Value = pBAR.Maximum;
            pBAR.Value = 0;
            DialogResult dr = MessageBoxEx.Show("Installation of Following Mods is Successful: \n" + miMOD.Aggregate("", (current, next) =>  current + @"[x]" + next + "\n") + "\n Would You Like To Delete Online Ones To Speed Up AoE2 DE?", "Success!",MessageBoxButtons.YesNo);
            if(dr == DialogResult.Yes)
            {
                deleteit.PerformClick();
            }
            else { }
        }

        private void listsubmitee_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemselecB.Text = listsubmitee.SelectedItems.Count.ToString();
        }

        private void listsubmitee_ItemClick(object sender, EventArgs e)
        {

        }

        private void listsubmitee_ItemAdded(object sender, EventArgs e)
        {
            modcountB.Text = listsubmitee.Items.Count.ToString();
        }

        private async void TickWorks_Tick(object sender, EventArgs e)
        {
            try
            {

               
                    if (!SteamAPI.Init())
                {
                    if (SteamAPI.IsSteamRunning())
                    {
                        steamLBL.ForeColor = Color.DarkBlue;
                        steamLBL.Text = "Steam Running..";
                        steamSTATUS.Image = DE_Sensei.Properties.Resources.steam_red;
                    }
                    else
                    {
                        steamLBL.ForeColor = Color.Gold;
                        await Task.Delay(200);
                        steamLBL.Text = "Steam OFF";
                        await Task.Delay(100);
                        steamLBL.ForeColor = Color.Maroon;
                        steamSTATUS.Image = DE_Sensei.Properties.Resources.steam_red;
                    }

                    //SteamUtils.GetAppID();

                }
                else
                {
                    TickWorks.Stop();
                    steamLBL.ForeColor = Color.ForestGreen;
                    steamLBL.Text = "Steam ON";
                    steamSTATUS.Image = DE_Sensei.Properties.Resources.steam_green;
                    await Task.Delay(200);

                    bool IsDE = SteamApps.BIsAppInstalled(new AppId_t(813780));
                    if (!IsDE)
                    {
                        MessageBoxEx.Show("Please Buy or Install Age of Empires II: Definitive Edition before you proceed!", "Game Missing");
                        TickWorks.Stop();
                    }
                    //Retrieve game folder
                    Additional.Enabled = true;
                    modsTAB.Enabled = true;
                    //File.WriteAllBytes("file.txt", fileContents);
                    SaveDirectoryPath();
                    if (pf.CheckGamePower(saveDirectoryPath + @"\AoE2DE_s.exe"))
                        highPERFORMANCE.Checked = true;
                    else if (!pf.CheckGamePower(saveDirectoryPath + @"\AoE2DE_s.exe"))
                        powerSAVING.Checked = true;
                    else
                    { }

                    Thread.Sleep(100);
                    //Enable Sensei features
                    //perfcb.Enabled = true;
                    //shrinktrack.Enabled = true;
                    //splashswitch.Enabled = true;
                    //modswitch.Enabled = true;
                    //introswitch.Enabled = true;
                    //effectswitch.Enabled = true;

                    foreach (Control ctrl in flowLayoutPanel2.Controls)
                        ctrl.Enabled = true;
                    foreach (Control ctrl in flowLayoutPanel3.Controls)
                        ctrl.Enabled = true;
                    //Retrieve settings
                    pf.CheckLayers(saveDirectoryPath + @"\AoE2DE_s.exe");

                    pf.RetSETTINGS4(saveDirectoryPath + @"\AoE2DE_s.exe", runas, "RUNASADMIN");
                    pf.RetSETTINGS4(saveDirectoryPath + @"\AoE2DE_s.exe", screenopt, "DISABLEDXMAXIMIZEDWINDOWEDMODE");
                    pf.RetSETTINGS4(saveDirectoryPath + @"\AoE2DE_s.exe", dpiscal, "HIGHDPIAWARE");
                    //Stop ticker
                    
                }
            }
            catch (SystemException)
            {
                steamLBL.ForeColor = Color.Black;
                steamLBL.Text = "Steam OFF";
                steamSTATUS.Image = DE_Sensei.Properties.Resources.steam_red;

            }

        }
        private void selectmods_CheckedChanged(object sender, EventArgs e)
        {
            if(selectmodsB.Checked == true && listsubmitee.Items.Count > 0)
            {
                for (int i = 0; i < listsubmitee.Items.Count; i++)
                {
                    listsubmitee.SetSelected(i, true);
                }
                
                //foreach(var mod in listsubmitee.Items)
                //    mod.SetSelected
            }
            else if(selectmodsB.Checked == false && listsubmitee.Items.Count > 0)
            {
                for (int i = 0; i < listsubmitee.Items.Count; i++)
                {
                    listsubmitee.SetSelected(i, false);
                }
            }
        }

        private void browsemod_Click(object sender, EventArgs e)
        {
            if (listsubmitee.SelectedItems.Count == 1)
            {
                string mpath;
                if (ModMenu.TryGetValue(listsubmitee.SelectedItem.ToString(), out mpath))
                {
                    if(Directory.Exists(mpath))
                        Process.Start(mpath);
                }
                    
            }
            else if(listsubmitee.SelectedItems.Count > 1)
            {
                MessageBoxEx.Show("Please Select Only One Mod! Multiple selections not allowed.", "Multiple Selections Detected!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void efconfig_Click(object sender, EventArgs e)
        {
            if(SteamAPI.Init())
            {
                Main mn = new Main();
                mn.GetDEPath = SaveDirectoryPath();
                mn.GetGames = dePATH;
                mn.Show();
            }
            else
            {
                MessageBox.Show("Sign into your steam client to use this feature.", "Steam not running!");
            }
           
        }

        private void efconfig_MouseHover(object sender, EventArgs e)
        {
            if(SteamAPI.Init())
                efconfig.Image = Properties.Resources.gear2;
        }

        private void efconfig_MouseLeave(object sender, EventArgs e)
        {
            if (SteamAPI.Init())
                efconfig.Image = Properties.Resources.gear;
        }

        private void introswitch_ValueChanged(object sender, EventArgs e)
        {
            if (introswitch.ValueObject.ToString() == "Y" && introswitch.Focus())
            {
                if (File.Exists(saveDirectoryPath + @"\resources\_common\movies\aoeii4K.wmv"))
                    File.Move(saveDirectoryPath + @"\resources\_common\movies\aoeii4K.wmv", saveDirectoryPath + @"\resources\_common\movies\aoeii4K.wmv.old");
                    
                if (File.Exists(saveDirectoryPath + @"\resources\_common\movies\aoeiide_titlevideo.wmv"))
                    File.Move(saveDirectoryPath + @"\resources\_common\movies\aoeiide_titlevideo.wmv", saveDirectoryPath + @"\resources\_common\movies\aoeiide_titlevideo.wmv.old");

                pf.SenseiREG("Skip Intro", 1, Microsoft.Win32.RegistryValueKind.DWord);
                MessageBoxEx.Show("Intro Videos Successfully Disabled!", "Intro Disabled!");

            }
            else if (introswitch.ValueObject.ToString() == "N" && introswitch.Focus())
            {
                if (File.Exists(saveDirectoryPath + @"\resources\_common\movies\aoeii4K.wmv.old"))
                    File.Move(saveDirectoryPath + @"\resources\_common\movies\aoeii4K.wmv.old", saveDirectoryPath + @"\resources\_common\movies\aoeii4K.wmv");

                if (File.Exists(saveDirectoryPath + @"\resources\_common\movies\aoeiide_titlevideo.wmv.old"))
                    File.Move(saveDirectoryPath + @"\resources\_common\movies\aoeiide_titlevideo.wmv.old", saveDirectoryPath + @"\resources\_common\movies\aoeiide_titlevideo.wmv");

                pf.SenseiREG("Skip Intro", 0, Microsoft.Win32.RegistryValueKind.DWord);
                MessageBoxEx.Show("Intro Videos Enabled!", "Intro Enabled!");
            }
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        static string ReadableFileSize(double size, int unit = 0)
        {
            string[] units = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

            while (size >= 1024)
            {
                size /= 1024;
                ++unit;
            }

            return String.Format("{0:0.#} {1}.", size, units[unit]);
        }
        
        private void splashswitch_ValueChanged(object sender, EventArgs e)
        {
            if (splashswitch.ValueObject.ToString() == "Y" && splashswitch.Focus())
            {
                if (File.Exists(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp"))
                {
                    string sizeSPLA;
                    if (!File.Exists(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp.old"))
                        File.Copy(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp", saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp.old", true);

                    if (new FileInfo(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp").Length > 500000)
                    {
                        sizeSPLA = ReadableFileSize(new FileInfo(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp").Length);
                        File.WriteAllBytes(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp", ImageToByte(Properties.Resources.loading_slash_sml));
                        pf.SenseiREG("Splash Screen Pixelate",1,Microsoft.Win32.RegistryValueKind.DWord);
                        MessageBoxEx.Show("Success! Splash Screen is Pixelated to 7 KB - From " + sizeSPLA??"900 KB.", "Results");
                    }
                    else
                    {
                        sizeSPLA = ReadableFileSize(new FileInfo(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp").Length);
                        pf.SenseiREG("Splash Screen Pixelate", 1, Microsoft.Win32.RegistryValueKind.DWord);
                        MessageBoxEx.Show("Success! Splash Screen is Pixelated to 7 KB - From " + sizeSPLA ?? "900 KB.", "Results");
                    }

                }
            }
            else if (splashswitch.ValueObject.ToString() == "N" && splashswitch.Focus())
            {
                if (File.Exists(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp.old"))
                {
                    File.WriteAllBytes(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp", File.ReadAllBytes(saveDirectoryPath + @"\resources\_launcher\loading_slash_sml.bmp.old"));
                    pf.SenseiREG("Splash Screen Pixelate", 0, Microsoft.Win32.RegistryValueKind.DWord);
                    MessageBoxEx.Show("Success! Splash Screen Restored.", "Results");
                }

            }
            
            
        }

        private void perfcb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void resocb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rangeSlider1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void shrinktrack_ValueChanged(object sender, EventArgs e)
        {
            if(shrinktrack.Focus())
            {
                shrinklbl.Text = shrinktrack.Value.ToString() + "%";
                Task.Delay(800);
                pf.RegEdit("Game Render Scale", shrinktrack.Value, Microsoft.Win32.RegistryValueKind.DWord);
                pf.SenseiREG("Shrink", shrinktrack.Value, Microsoft.Win32.RegistryValueKind.DWord);
                
            }
            
        }
        public void DeleteDirectory(string target_dir)
        {
            try
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);
                pBAR.Maximum += files.Count() + dirs.Count();
                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                    pBAR.Value++;
                }

                foreach (string dir in dirs)
                {
                    Directory.Delete(dir, true);
                    pBAR.Value++;
                }
                Directory.Delete(target_dir, true);
            }
            catch(System.IO.DirectoryNotFoundException)
            {
                
            }
            

        }
        private void deleteit_Click(object sender, EventArgs e)
        {
            if(listsubmitee.SelectedItems.Count>0)
            {

            
            DialogResult dr = MessageBoxEx.Show("Confirm Deletion of Mod(s).", "Alert", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    {
                pBAR.Maximum = 0;
                pBAR.Value = 0;
                //Directory.Delete(target_dir, false);
                foreach (var item in listsubmitee.SelectedItems)
                {
                    if (ModMenu.ContainsKey(item.Text))
                    {
                        string mpath;
                        if (ModMenu.TryGetValue(item.Text, out mpath))
                        {
                            DeleteDirectory(mpath);
                            //filesCOUNT = Directory.GetFiles(mpath, "*").Count();
                            //dirsCOUNT = Directory.GetDirectories(mpath, "*").Count();
                        }

                    }
                }
                RefreshMods();
                pBAR.Value = 0;
            }
                else return;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void refreshmods_Click(object sender, EventArgs e)
        {
            if((string)modswitch.ValueObject == "Y")
                RefreshMods();
        }
        private static string FormatByteSize(double bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "TB" };
            int index = 0;
            do { bytes /= 1024; index++; }
            while (bytes >= 1024);
            return String.Format("{0:0.00} {1}", bytes, Suffix[index]);

        }

        public void SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == ZipProgressEventType.Saving_Started)
            {
                //KryptonMessageBox.Show("Begin Saving: " + e.ArchiveName);
            }
            else if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry || e.EventType == ZipProgressEventType.Extracting_BeforeExtractAll)
            {
                //labelCompressionStatus.Text = "Writing: " + e.CurrentEntry.FileName + " (" + (e.EntriesSaved + 1) + "/" + e.EntriesTotal + ")";
                //labelFilename.Text = "Filename:" + e.CurrentEntry.LocalFileName;

                pBAR2.Maximum = e.EntriesTotal;
                pBAR2.Value = e.EntriesSaved + 1;
            }
            else if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead || e.EventType == ZipProgressEventType.Extracting_AfterExtractEntry)
            {
                pBAR.Value = (int)((e.BytesTransferred * 100) / e.TotalBytesToTransfer);
            }
            else if (e.EventType == ZipProgressEventType.Saving_Completed || e.EventType == ZipProgressEventType.Extracting_AfterExtractAll)
            {
                //KryptonMessageBox.Show("Done: " + e.ArchiveName);
                pBAR.Value = 0;
                pBAR2.Value = 0;
            }
           
        }
        private void exportit_Click(object sender, EventArgs e)
        {

            if (listsubmitee.SelectedIndex >= 0 || listsubmitee.CheckedItems.Count >= 0)
            {
                // Show the FolderBrowserDialog.
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Archive Zip|*.Zip";
                saveFileDialog1.Title = "Save Zip File To";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    String ZipFileToCreate = saveFileDialog1.FileName;

                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Default;
                        zip.SaveProgress += SaveProgress;

                        zip.StatusMessageTextWriter = System.Console.Out;
                        foreach (var mod in listsubmitee.SelectedItems)
                        {
                            if (ModMenu.ContainsKey(mod.ToString()))
                            {
                                string mpath;
                                if (ModMenu.TryGetValue(mod.ToString(), out mpath))
                                    zip.AddDirectory(mpath, mod.ToString());
                            }
                            
                        }

                        zip.Save(ZipFileToCreate);
                    }

                    FileInfo sizez = new FileInfo(saveFileDialog1.FileName);
                    KryptonMessageBox.Show("Mods Size: " + FormatByteSize(sizez.Length), "Success");
                    //end dialog
                }

                else
                {

                }
            }
        }

        private void runas_CheckedChanged(object sender, EventArgs e)
        {

            string EXEinfo = pf.EXEread(saveDirectoryPath + @"\AoE2DE_s.exe");
            string EXEpath = saveDirectoryPath + @"\AoE2DE_s.exe";
            if (runas.Checked && !EXEinfo.StartsWith(@"~"))
                pf.EXEsettings(EXEpath, EXEinfo + @"~ RUNASADMIN", Microsoft.Win32.RegistryValueKind.String);
            else if (runas.Checked && EXEinfo.StartsWith(@"~") && !EXEinfo.Contains("RUNASADMIN"))
                pf.EXEsettings(EXEpath, EXEinfo + @" RUNASADMIN", Microsoft.Win32.RegistryValueKind.String);
            else if(!runas.Checked && !EXEinfo.StartsWith(@"~ RUNASADMIN"))
                pf.EXEsettings(EXEpath, EXEinfo.Replace(@" RUNASADMIN",""), Microsoft.Win32.RegistryValueKind.String);
            else if (!runas.Checked && EXEinfo.StartsWith(@"~ RUNASADMIN"))
                pf.EXEsettings(EXEpath, @"~" + EXEinfo.Replace(@"~ RUNASADMIN", ""), Microsoft.Win32.RegistryValueKind.String);


        }
        
        private void screenopt_CheckedChanged(object sender, EventArgs e)
        {
            string EXEinfo = pf.EXEread(saveDirectoryPath + @"\AoE2DE_s.exe");
            string EXEpath = saveDirectoryPath + @"\AoE2DE_s.exe";
            if (runas.Checked && !EXEinfo.StartsWith(@"~"))
                pf.EXEsettings(EXEpath, EXEinfo + @"~ DISABLEDXMAXIMIZEDWINDOWEDMODE", Microsoft.Win32.RegistryValueKind.String);
            else if (runas.Checked && EXEinfo.StartsWith(@"~") && !EXEinfo.Contains("DISABLEDXMAXIMIZEDWINDOWEDMODE"))
                pf.EXEsettings(EXEpath, EXEinfo + @" DISABLEDXMAXIMIZEDWINDOWEDMODE", Microsoft.Win32.RegistryValueKind.String);
            else if (!runas.Checked && !EXEinfo.StartsWith(@"~ DISABLEDXMAXIMIZEDWINDOWEDMODE"))
                pf.EXEsettings(EXEpath, EXEinfo.Replace(@" DISABLEDXMAXIMIZEDWINDOWEDMODE", ""), Microsoft.Win32.RegistryValueKind.String);
            else if (!runas.Checked && EXEinfo.StartsWith(@"~ DISABLEDXMAXIMIZEDWINDOWEDMODE"))
                pf.EXEsettings(EXEpath, @"~" + EXEinfo.Replace(@"~ DISABLEDXMAXIMIZEDWINDOWEDMODE", ""), Microsoft.Win32.RegistryValueKind.String);
        }

        private void dpiscal_CheckedChanged(object sender, EventArgs e)
        {
            string EXEinfo = pf.EXEread(saveDirectoryPath + @"\AoE2DE_s.exe");
            string EXEpath = saveDirectoryPath + @"\AoE2DE_s.exe";
            if (runas.Checked && !EXEinfo.StartsWith(@"~"))
                pf.EXEsettings(EXEpath, EXEinfo + @"~ HIGHDPIAWARE", Microsoft.Win32.RegistryValueKind.String);
            else if (runas.Checked && EXEinfo.StartsWith(@"~") && !EXEinfo.Contains("HIGHDPIAWARE"))
                pf.EXEsettings(EXEpath, EXEinfo + @" HIGHDPIAWARE", Microsoft.Win32.RegistryValueKind.String);
            else if (!runas.Checked && !EXEinfo.StartsWith(@"~ HIGHDPIAWARE"))
                pf.EXEsettings(EXEpath, EXEinfo.Replace(@" HIGHDPIAWARE", ""), Microsoft.Win32.RegistryValueKind.String);
            else if (!runas.Checked && EXEinfo.StartsWith(@"~ HIGHDPIAWARE"))
                pf.EXEsettings(EXEpath, @"~" + EXEinfo.Replace(@"~ HIGHDPIAWARE", ""), Microsoft.Win32.RegistryValueKind.String);
        }

        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void powerSAVING_CheckedChanged(object sender, EventArgs e)
        {
            if (powerSAVING.Checked)
            {
                pf.RegEdit(saveDirectoryPath + @"\AoE2DE_s.exe", "GpuPreference=1;", RegistryValueKind.String, @"Software\Microsoft\DirectX\UserGpuPreferences");
                powerSTATE.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Red;
            }
               
            
        }

        private void highPERFORMANCE_CheckedChanged(object sender, EventArgs e)
        {
            if (highPERFORMANCE.Checked)
            {
                pf.RegEdit(saveDirectoryPath + @"\AoE2DE_s.exe", "GpuPreference=2;", RegistryValueKind.String, @"Software\Microsoft\DirectX\UserGpuPreferences");
                powerSTATE.ColorTable = DevComponents.DotNetBar.Controls.ePanelColorTable.Green;
            }
                
        }
        void zip_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.TotalBytesToTransfer > 0)
            {
                pBAR.Value = Convert.ToInt32(100 * e.BytesTransferred / e.TotalBytesToTransfer);
            }
        }
        private void menueff_ValueChanged(object sender, EventArgs e)
        {
            if(menueff.Focus())
            {
            if(menueff.ValueObject.ToString() == "N")
            {
                try
                {
                    menueff.Enabled = false;
                    File.WriteAllBytes(Path.Combine(Path.GetTempPath(), "Defaultxaml"), Properties.Resources.DF);
                using(Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(Path.Combine(Path.GetTempPath(), "Defaultxaml")))
                {
                    zip.ExtractProgress +=
               new EventHandler<ExtractProgressEventArgs>(zip_ExtractProgress);
                   zip.ExtractAll(saveDirectoryPath + @"\resources\_common\wpfg\", ExtractExistingFileAction.OverwriteSilently);
                }
                    pBAR.Value = 0;
                    pf.SenseiREG("Menu Effects", 0, Microsoft.Win32.RegistryValueKind.DWord);
                    menueff.Enabled = true;
                }
                catch (SystemException ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                    menueff.Enabled = true;
                }
            }
            else if (menueff.ValueObject.ToString() == "Y")
            {
                try
                {
                    menueff.Enabled = false;
                    File.WriteAllBytes(Path.Combine(Path.GetTempPath(), "Optimalxaml"), Properties.Resources.MO);
                    using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(Path.Combine(Path.GetTempPath(), "Optimalxaml")))
                    {
                        zip.ExtractProgress +=
                   new EventHandler<ExtractProgressEventArgs>(zip_ExtractProgress);
                        zip.ExtractAll(saveDirectoryPath + @"\resources\_common\wpfg\", ExtractExistingFileAction.OverwriteSilently);
                    }
                    pBAR.Value = 0;
                    pf.SenseiREG("Menu Effects", 1, Microsoft.Win32.RegistryValueKind.DWord);
                    menueff.Enabled = true;
                }
                catch(SystemException ex)
                {
                    MessageBox.Show(ex.InnerException.ToString());
                    menueff.Enabled = true;
                }
                
            }
            }
        }

        private void flowLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openHOTKEYFOLDER_Click(object sender, EventArgs e)
        {
            Process.Start(dePATH + @"\" + _UserSteamID() + @"\profile");
        }

        private void importHOTKEYFILE_Click(object sender, EventArgs e)
        {
            OpenFileDialog importfile = new OpenFileDialog();
            importfile.Filter = "Hotkeys Profile HKI|*.hki";
            importfile.Title = "Select a .hki file To Import";
            importfile.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (importfile.FileName != "")
            {
                string idNAME = SteamFriends.GetFriendPersonaName(new CSteamID(ulong.Parse(_UserSteamID())));
                //Deflating hotkey file
                Decompress(new FileInfo(dePATH + @"\" + _UserSteamID() + @"\profile\Player.nfp"));
                //Edit hotkey file
                string hkiN = Regex.Replace(idNAME, @"[^0-9a-zA-Z]+", "") + @"-" + DateTime.Now.Year.ToString() + ".hki";
                pf.HKIwriter(DecompTMP, hkiN);
                //Compress hotkey file
                Compress(new FileInfo(DecompTMP));
                //move hotkey file back
                if (File.Exists(dePATH + @"\" + _UserSteamID() + @"\profile\" + hkiN))
                    File.Delete(dePATH + @"\" + _UserSteamID() + @"\profile\" + hkiN);

                File.Copy(DecompTMP + ".cm", dePATH + @"\" + _UserSteamID() + @"\profile\Player.nfp",true);
                File.Copy(importfile.FileName, dePATH + @"\" + _UserSteamID() + @"\profile\" + hkiN);
                //Clear User data cloud files
                try
                {
                    if (Directory.Exists(UserDataDirectory() + @"\remote"))
                        Directory.Delete(UserDataDirectory() + @"\remote",true);

                }
                catch (SystemException)
                {
                    //null
                }
                try
                {
                    if (File.Exists(UserDataDirectory() + @"\remotecache.vdf"))
                        File.Delete(UserDataDirectory() + @"\remotecache.vdf");

                    
                }
                catch(SystemException)
                {
                    //null
                }
               
                //Clear temp files
                if (File.Exists(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp"))
                    File.Delete(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp");

                if (File.Exists(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp.cm"))
                    File.Delete(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp.cm");
                //Restart Steam
                MessageBox.Show("Success! Your hotkey name is: " + hkiN + "\n\n" + "[+] STEAM will restart." + "\n" + "[+] SENSEI DE will Close." + "\n\n It's important for applying your new hotkey profile: " + hkiN, "Imported Successfully! Initiating Steam Restart");
                var process = Process.GetProcessesByName("steam")[0];
                process.Kill();
                var path = process.MainModule.FileName;
                process.Close();
                RunAsDesktopUser(path);
                //Process.Start(path);

                this.Close();

            }
            }
        public static string ReadNFP(string path)
        {
            using (BinaryReader b = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                // Variables for our position.
                int pos = 24;
                int required = 282;
                // Seek required position.
                b.BaseStream.Seek(pos, SeekOrigin.Begin);
                // Read the next bytes.
                byte[] by = b.ReadBytes(required);
                // Read HKI profile string and store byte piece.
                var HKIname = System.Text.Encoding.ASCII.GetString(by).Trim();
                return HKIname;
            }
        }
        private void RefreshHotkeys()
        {
            if (!Directory.Exists(dePATH + @"\" + _UserSteamID() + @"\profile"))
                return;
            if (!File.Exists(dePATH + @"\" + _UserSteamID() + @"\profile\Player.nfp"))
            {
                MessageBox.Show("Player.nfp not found! Please close Sensei DE and Start AOE2 DE through Steam to generate the file.", "Player nfp not found!");
                return;
            }
                

            hotkeyLIST.Items.Clear();
            foreach (var hki in Directory.GetFiles(dePATH + @"\" + _UserSteamID() + @"\profile", "*.hki"))
            {
                DevComponents.DotNetBar.ComboBoxItem item = new DevComponents.DotNetBar.ComboBoxItem();
                item.Text = Path.GetFileNameWithoutExtension(hki);
                hotkeyLIST.Items.Add(item);
            }
            Decompress(new FileInfo(dePATH + @"\" + _UserSteamID() + @"\profile\Player.nfp"));
            string los = ReadNFP(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp");
            hotkeyLIST.SelectedIndex = hotkeyLIST.FindStringExact(Regex.Replace(los, @"[^\w\.@-]", string.Empty).Replace(".hki", ""));
        }
        public static Match MatchesBOX(string option, string textfile)
        {
            Match m = Regex.Match(textfile, option + @"(\d+)");
            return m;
        }
        private void Additional_Click(object sender, EventArgs e)
        {
            RefreshHotkeys();
            //Decompress aop
            Decompress(new FileInfo(dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.aop"));

            string text = File.ReadAllText(dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.tmp");
            Match mHEALTHV = MatchesBOX("Health Bar Visibility:", text);
            Match mHEALTHB = MatchesBOX("Health Bars:", text);
            Match mVILLAGERD = MatchesBOX("VillagerDoubleClick:", text);
            Match mClickDRAGS = MatchesBOX("Click Drag Scroll Button:", text);
            Match mZOOMTOC = MatchesBOX("ZoomToCursor:", text);
            Match mINGS = MatchesBOX("ings:", text);
            Match mSHIFTA = MatchesBOX("ShiftAppendGroups:", text);
            Match mZOOMD = MatchesBOX("Default Zoom:", text);


            if (mHEALTHV.Success)
            {
                
                if (mHEALTHV.Groups[1].Value == "1")
                    nohealthBARS.Checked = false;
                else if (mHEALTHV.Groups[1].Value == "0")
                    nohealthBARS.Checked = true;

            }
            if (mHEALTHB.Success)
            {
                if (mHEALTHB.Groups[1].Value == "1")
                    nohealthBARS.Checked = false;
                else if (mHEALTHB.Groups[1].Value == "0")
                    nohealthBARS.Checked = true;

            }
            if (mVILLAGERD.Success)
            {
                if (mVILLAGERD.Groups[1].Value == "1")
                    villagerDOUBLECLICK.Checked = true;
                else if (mVILLAGERD.Groups[1].Value == "0")
                    villagerDOUBLECLICK.Checked = false;

            }
            if (mClickDRAGS.Success)
            {
                if (mClickDRAGS.Groups[1].Value == "1")
                    noclickDRAG.Checked = false;
                else if (mClickDRAGS.Groups[1].Value == "0")
                    noclickDRAG.Checked = true;

            }
            if (mZOOMTOC.Success)
            {
                if (mZOOMTOC.Groups[1].Value == "1")
                    noingameZOOM.Checked = false;
                else if (mZOOMTOC.Groups[1].Value == "0")
                    noingameZOOM.Checked = true;

            }
            if (mINGS.Success)
            {
               

                if (mINGS.Groups[1].Value == "1")
                    confirmDELETION.Checked = true;
                else if (mINGS.Groups[1].Value == "0")
                    confirmDELETION.Checked = false;

            }
            if (mSHIFTA.Success)
            {
               

                if (mSHIFTA.Groups[1].Value == "1")
                    shiftAPPEND.Checked = true;
                else if (mSHIFTA.Groups[1].Value == "0")
                    shiftAPPEND.Checked = false;

            }
            if (mZOOMD.Success)
            {
                zoomSLIDER.Value = Int32.Parse(mZOOMD.Groups[1].Value);

            }
            Thread.Sleep(100);
            File.Delete(dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.tmp");
        }

        private void refreshHOTKEYS_Click(object sender, EventArgs e)
        {
            RefreshHotkeys();
        }

        private void hotkeyLIST_SelectedValueChanged(object sender, EventArgs e)
        {
            if(hotkeyLIST.Focused)
            {

            
            DialogResult dlg = MessageBox.Show("Set < " + hotkeyLIST.SelectedItem.ToString() + ".hki" + " > As Your Default Hotkey Preset?\n [+] Steam will restart to apply this change.", "Set Default Hotkey", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dlg == DialogResult.Yes)
                {
            string idNAME = SteamFriends.GetFriendPersonaName(new CSteamID(ulong.Parse(_UserSteamID())));
            //Deflating hotkey file
            Decompress(new FileInfo(dePATH + @"\" + _UserSteamID() + @"\profile\Player.nfp"));
            //Edit hotkey file
            string hkiN = hotkeyLIST.SelectedItem.ToString() + ".hki";
            pf.HKIwriter(DecompTMP, hkiN);
            //Compress hotkey file
            Compress(new FileInfo(DecompTMP));

            File.Copy(DecompTMP + ".cm", dePATH + @"\" + _UserSteamID() + @"\profile\Player.nfp", true);
            
            //Clear User data cloud files
            try
            {
                if (Directory.Exists(UserDataDirectory() + @"\remote"))
                    Directory.Delete(UserDataDirectory() + @"\remote", true);

            }
            catch (SystemException)
            {
                //null
            }
            try
            {
                if (File.Exists(UserDataDirectory() + @"\remotecache.vdf"))
                    File.Delete(UserDataDirectory() + @"\remotecache.vdf");


            }
            catch (SystemException)
            {
                //null
            }

            //Clear temp files
            if (File.Exists(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp"))
                File.Delete(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp");

            if (File.Exists(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp.cm"))
                File.Delete(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp.cm");
            //Restart Steam
            MessageBox.Show("Success! Your hotkey name is: " + hkiN + "\n\n" + "[+] STEAM will restart." + "\n" + "[+] SENSEI DE will Close." + "\n\n It's important for applying your new hotkey profile: " + hkiN, "Imported Successfully! Initiating Steam Restart");
            var process = Process.GetProcessesByName("steam");
            var process2 = Process.GetProcessesByName("steamwebhelper");
                    string steamp = null;
                    foreach (var p in process)
                    {
                        p.Kill();
                        var path = p.MainModule.FileName;
                        steamp = path;
                    }
                    if (steamp != null)
                        RunAsDesktopUser(steamp);


                    this.Close();
                }
                else 
                {
                    if (File.Exists(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp"))
                        File.Delete(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp");
                }
                Thread.Sleep(100);
                if (File.Exists(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp"))
                    File.Delete(dePATH + @"\" + _UserSteamID() + @"\profile\Player.tmp");

            }
        }

        private void labelX14_Click(object sender, EventArgs e)
        {

        }

        private void zoomSLIDER_ValueChanged(object sender, EventArgs e)
        {
            zoomPERC.Text = zoomSLIDER.Value.ToString() + "%";
        }

        private void applyEXP_Click(object sender, EventArgs e)
        {
            //Decompress aop
            Decompress(new FileInfo(dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.aop"));
            //Find and replace values
            if (File.Exists(dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.tmp"))
            {
                string text = File.ReadAllText(dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.tmp", Encoding.UTF8);
                
                Match m = Regex.Match(text, zoomPhrase2);
                if (nohealthBARS.Checked)
                {
                    text = text.Replace("Health Bar Visibility:1", "Health Bar Visibility:0");
                    text = text.Replace("Health Bars:1", "Health Bars:0");
                } 
                else
                {
                    text = text.Replace("Health Bar Visibility:0", "Health Bar Visibility:1");
                    text = text.Replace("Health Bars:0", "Health Bars:1");
                }

                if (noclickDRAG.Checked)
                    text = text.Replace("Click Drag Scroll Button:1", "Click Drag Scroll Button:0");
                else
                    text = text.Replace("Click Drag Scroll Button:0", "Click Drag Scroll Button:1");

                if (villagerDOUBLECLICK.Checked)
                    text = text.Replace("VillagerDoubleClick:0", "VillagerDoubleClick:1");
                else
                    text = text.Replace("VillagerDoubleClick:1", "VillagerDoubleClick:0");

                if (noingameZOOM.Checked)
                    text = text.Replace("ZoomToCursor:1", "ZoomToCursor:0");
                else
                    text = text.Replace("ZoomToCursor:0", "ZoomToCursor:1");

                if (confirmDELETION.Checked)
                    text = text.Replace("ings:0", "ings:1");
                else
                    text = text.Replace("ings:1", "ings:0");

                if (shiftAPPEND.Checked)
                    text = text.Replace("ShiftAppendGroups:0", "ShiftAppendGroups:1");
                else
                    text = text.Replace("ShiftAppendGroups:1", "ShiftAppendGroups:0");
                if(m.Success)
                {    
                    text = Regex.Replace(text, zoomPhrase2, "Default Zoom:" + zoomPERC.Text.Replace("%",""));
                    
                }
                //text = Regex.Replace(text, @"\<ref.*?\</ref\>", "");

                File.WriteAllText(dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.tmp.txt", text);
            }

           
            //Compress aop
            Compress(new FileInfo(dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.tmp.txt"));
            //Override aop
            string aopfile = dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.aop";
            //FileAttributes attrs = File.GetAttributes(aopfile);
            //if (attrs.HasFlag(FileAttributes.ReadOnly))
            //    File.SetAttributes(aopfile, attrs & ~FileAttributes.ReadOnly);
            File.Delete(aopfile);
            File.Copy(dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.tmp.txt.cm", aopfile, true);

            try
            {
                File.Delete(dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.tmp.txt.cm");
                File.Delete(dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.tmp.txt");
                File.Delete(dePATH + @"\" + _UserSteamID() + @"\profile\AdditionalOptions.tmp");
            }
            catch (SystemException)
            {

            }
            //Restart Steam & Sensei DE
            if (steamRESTART.Checked)
            {
                //Clear User data cloud files
                try
                {
                    if (Directory.Exists(UserDataDirectory() + @"\remote"))
                        Directory.Delete(UserDataDirectory() + @"\remote", true);

                }
                catch (SystemException)
                {
                    //null
                }
                try
                {
                    if (File.Exists(UserDataDirectory() + @"\remotecache.vdf"))
                        File.Delete(UserDataDirectory() + @"\remotecache.vdf");


                }
                catch (SystemException)
                {
                    //null
                }
                MessageBox.Show("[+] STEAM will restart." + "\n" + "[+] SENSEI DE will Close." + "\n\n It's important for applying your settings. ", "Initiating Steam Restart");
                var process = Process.GetProcessesByName("steam")[0];
                process.Kill();
                var path = process.MainModule.FileName;
                Process.Start(path);
                
                Application.Exit();
            }
            //Recommend Steam restart
            MessageBox.Show("If changes do not take place.. Please check the box Force Steam Restart.", "Changes Applied!");
        }

        private void modsTAB_Click(object sender, EventArgs e)
        {
            RefreshMods();
        }

        private void hotkeyLIST_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private const string ParameterQuery = "\\-(?<key>\\w+)\\s*=\\s*(\"(?<value>[^\"]*)\"|(?<value>[^\\-]*))\\s*";

        private static Dictionary<string, string> ParseString(string value)
        {
            var regex = new Regex(ParameterQuery);
            return regex.Matches(value).Cast<Match>().ToDictionary(m => m.Groups["key"].Value, m => m.Groups["value"].Value);
        }
        public static void procCMD(string mycmd)
        {
            
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c " + mycmd;
            startInfo.Verb = "runas";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            process.Close();
        }
        private void winDEFENDER_ValueChanged(object sender, EventArgs e)
        {
            if(winDEFENDER.ValueObject.ToString()== "Y" && winDEFENDER.Focused)
            {
                procCMD("powershell -inputformat none -outputformat none -NonInteractive -Command " + "\"Add-MpPreference -ExclusionPath \'" + saveDirectoryPath + "\'\"");
                pf.SenseiREG("WinDef", 1, Microsoft.Win32.RegistryValueKind.DWord);
            }
            else if (winDEFENDER.ValueObject.ToString() == "N" && winDEFENDER.Focused)
            {
                procCMD("powershell -Command \"Remove-MpPreference -ExclusionPath \'" + saveDirectoryPath + "\'\"");
                pf.SenseiREG("WinDef", 0, Microsoft.Win32.RegistryValueKind.DWord);
            }
        }
    }
}
